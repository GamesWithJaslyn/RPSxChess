using System.Collections.Generic;
using System;
using UnityEngine;

public enum PieceType { None, Bow, Sword, Pegasus }
public enum Team { Blue, Red }

public class PieceModelImpl : IPieceModel
{
    public int Position { get; private set; }
    public Team Team { get; }
    public PieceType PieceType { get; set; }


    public bool IsSelected { get; set; }
    public bool IsAlive { get; private set; }
    public bool IsPromoted { get; set; }

    private IMoveStrategy _moveStrategy;
    private ISpecialAttack _specialAttack;
    private IBoardModel _boardModel;
    private int _originalPos;
    private PieceType _originalType;


    public event Action OnDeath;
    public event Action<int> OnMoved;
    public event Action OnChangeInto;
    public event Action OnReset;

    public PieceModelImpl(int pos, PieceType pieceType, Team team, IBoardModel model)
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
        SetSpecialAttack();
    }


    public List<Move> GetValidMoves() { return _moveStrategy.GetValidMoves(_boardModel, this); }
    public ISpecialAttack GetSpecialAttack() { return _specialAttack; }

    public bool IsSameTeam(Team team) { return Team == team; }

    public bool IsItPossibleToChange(int pos)
    {
        bool location;

        if (Team.Equals(Team.Blue) && pos > 109 && pos < 121)
        {
            location = true;
        }
        else if (Team.Equals(Team.Red) && pos >= 0 && pos < 11)
        {
            location = true;
        }
        else
        {
            location = false;
        }

        return IsPromoted == false && location;
    }

    public void SetPos(int tile)
    {
        Position = tile;
        OnMoved?.Invoke(tile);
        IsSelected = false;
    }

    public Move MakeMove(int tile)
    {
        foreach (Move move in GetValidMoves())
        {
            if (move.To == tile)
            {
                SetPos(tile);
                return move;
            }
        }

        return new MoveBuilder(new Move(-1000, -1000)).AddFlags(new List<MoveFlags>()).BuildMove();
    }

    public virtual void SetDead()
    {
        IsAlive = false;
        Debug.Log($"[ABAsicPiece] - {Team}_{PieceType} Died!");
        OnDeath?.Invoke();
    }

    public void SetPieceType(PieceType type)
    {
        PieceType = type;

        if (this is AAttackingPiece aAttacking)
        {
            aAttacking.ChangeTarget(PieceType);
        }

        SetMoveStrategy();
        SetSpecialAttack();
        OnChangeInto?.Invoke();
    }

    private void SetMoveStrategy()
    {
        _moveStrategy = null;

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

    private void SetSpecialAttack()
    {
        _specialAttack = null;

        switch (PieceType)
        {
            case PieceType.Bow:
                _specialAttack = new Bow_SpecialAttack();
                break;
        }
    }

    public void ChangeInto(PieceType changingInto)
    {
        SetPieceType(changingInto);
        IsPromoted = true;
    }

    public void Reset()
    {
        SetPos(_originalPos);
        SetPieceType(_originalType);
        IsPromoted = false;
        IsAlive = true;
        OnReset?.Invoke();
    }
}
