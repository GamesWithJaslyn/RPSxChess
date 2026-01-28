using UnityEngine;
using System.Collections.Generic;


//Creates the basic three piece types, on both teams.
public class CreatePieces
{
    private List<IPieceModel> _allPieces;
    private IBoardModel _boardModel;

    public CreatePieces(IBoardModel model)
    {
        _allPieces = new List<IPieceModel>();
        _boardModel = model;

        _allPieces.AddRange(CreateBluePieces());
        _allPieces.AddRange(CreateRedPieces());
    }

    public List<IPieceModel> GetPieces()
    {
        _allPieces = new List<IPieceModel>();
        _allPieces.AddRange(CreateBluePieces());
        _allPieces.AddRange(CreateRedPieces());
        return _allPieces;
    }

    private List<IPieceModel> CreateBluePieces()
    {
        List<IPieceModel> pieces = new List<IPieceModel>();

        for (int i = 0; i < 22; i++)
        {
            if ((i < 3) || (i >= 11 && i < 14))
            {
                pieces.Add(new AAttackingPiece(i, PieceType.Bow, Team.Blue,
                 PieceType.Pegasus, _boardModel));
            }
            else if ((i >= 4 && i < 7) || (i >= 15 && i < 18))
            {
                pieces.Add(new AAttackingPiece(i, PieceType.Sword, Team.Blue,
                PieceType.Bow, _boardModel));
            }
            else if ((i >= 8 && i < 11) || (i >= 19 && i < 22))
            {
                pieces.Add(new AAttackingPiece(i, PieceType.Pegasus, Team.Blue,
                PieceType.Sword, _boardModel));
            }
        }

        return pieces;
    }

    private List<IPieceModel> CreateRedPieces()
    {
        List<IPieceModel> pieces = new List<IPieceModel>();

        for (int i = 99; i < 121; i++)
        {
            if ((i < 102) || (i >= 110 && i < 113))
            {
                pieces.Add(new AAttackingPiece(i, PieceType.Bow, Team.Red,
                PieceType.Pegasus, _boardModel));
            }
            else if ((i >= 103 && i < 106) || (i >= 114 && i < 117))
            {
                pieces.Add(new AAttackingPiece(i, PieceType.Sword, Team.Red,
                PieceType.Bow, _boardModel));
            }
            else if ((i >= 107 && i < 110) || (i >= 118 && i < 121))
            {
                pieces.Add(new AAttackingPiece(i, PieceType.Pegasus, Team.Red,
                PieceType.Sword, _boardModel));
            }
        }

        return pieces;
    }

}
