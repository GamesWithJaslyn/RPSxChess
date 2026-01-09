using System.Collections.Generic;
using System;
using UnityEngine;

public enum PieceType { None, Bow, Sword, Pegasus }
public enum Team { Blue, Red }

public class PieceModelImpl : IPieceModel_V2
{
    public int Position { get; private set; }
    public Team Team { get; }
    public PieceType PieceType { get; set; }


    public bool IsSelected { get; set; }
    public bool IsAlive { get; private set; }
    public bool IsPromoted { get; set; }

    private IMoveStrategy _moveStrategy;
    private IBoardModel_V2 _boardModel;
    private int _originalPos;
    private PieceType _originalType;


    public event Action OnDeath;
    public event Action<int> OnMoved;
    public event Action OnChangeInto;

    public PieceModelImpl(int pos, PieceType pieceType, Team team, IBoardModel_V2 model)
    {
        Position = pos;
        Team = team;
        _boardModel = model;
        PieceType = pieceType;

        _originalPos = Position;
        _originalType = PieceType;

        IsAlive = true;
        IsSelected = false;
        IsPromoted = false;

        SetMoveStrategy();
    }


    public List<Move> GetValidMoves() { return _moveStrategy.GetValidMoves(_boardModel, this); }

    public bool IsSameTeam(Team team) { return Team == team; }

    public bool IsItPossibleToChange()
    {
        bool location;

        if (Team.Equals(Team.Blue) && Position >= 109 && Position < 121) { location = true; }
        else if (Team.Equals(Team.Red) && Position >= 0 && Position < 11) { location = true; }
        else { location = false; }

        return IsPromoted && location;
    }

    public void SetPos(int tile)
    {
        Debug.Log("[ABasicPiece] - SetPos() -> move sucessful");
        Position = tile;
        OnMoved?.Invoke(tile);
        IsSelected = false;
    }

    public Move MakeMove(int tile)
    {
        foreach (Move move in GetValidMoves())
        {
            if (move._to == tile)
            {
                SetPos(tile);
                return move;
            }
        }

        return new Move();
    }

    public virtual void SetDead()
    {
        IsAlive = false;
        OnDeath?.Invoke();
    }

    public void SetPieceType(PieceType type)
    {
        PieceType = type;

        if (this is AAttackingPiece_V2 aAttacking)
        {
            aAttacking.ChangeTarget(PieceType);
        }

        OnChangeInto?.Invoke();
        SetMoveStrategy();
    }

    private void SetMoveStrategy()
    {
        switch (PieceType)
        {
            case PieceType.Bow:
                _moveStrategy = new Bow_Moves();
                break;
            case PieceType.Sword:
                _moveStrategy = new Sword_Moves();
                break;
            case PieceType.Pegasus:
                _moveStrategy = new Pegasus_Moves();
                break;
        }
    }

    public void ChangeInto(PieceType changingInto)
    {
        if (IsItPossibleToChange())
        {
            SetPieceType(changingInto);
            IsPromoted = true;

            if (this is AAttackingPiece_V2 attack)
            {
                attack.ChangeTarget(changingInto);
            }
            // if (PiecesLeft() <= 0) 
            // {
            //     GameState.OnGameWon?.Invoke(_pieceType.Equals(Team.Blue) ? Team.Blue : Team.Red);
            // }

        }
        else { return; }
    }

    public void Reset()
    {
        SetPos(_originalPos);
        SetPieceType(_originalType);
        IsPromoted = false;
        IsAlive = true;
    }

    // public override bool Equals(object obj)
    // {
    //     if (obj is not IPieceModel_V2 piece) return false;
    //     if (obj is null) return false;
    //     if (ReferenceEquals(this, obj)) return true;

    //     return //Position == piece.Position
    //          Team == piece.Team
    //         && PieceType == piece.PieceType
    //         //&& IsSelected == piece.IsSelected
    //         && IsPromoted == piece.IsPromoted
    //         && IsAlive == piece.IsAlive;
    // }

    // public override int GetHashCode()
    // {
    //     return HashCode.Combine(//Position, 
    //     Team, PieceType, IsAlive, IsSelected, IsPromoted);
    // }

}
