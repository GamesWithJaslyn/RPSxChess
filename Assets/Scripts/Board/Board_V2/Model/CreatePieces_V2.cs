using UnityEngine;
using System.Collections.Generic;


//Creates the basic three piece types, on both teams.
public class CreatePieces_V2
{
    private List<IPieceModel_V2> _allPieces;
    private IBoardModel_V2 _boardModel;

    public CreatePieces_V2(IBoardModel_V2 model)
    {
        _allPieces = new List<IPieceModel_V2>();
        _allPieces.Clear();
        _boardModel = model;

        _allPieces.AddRange(CreateBluePieces());
        _allPieces.AddRange(CreateRedPieces());
    }

    public List<IPieceModel_V2> GetPieces()
    {
        return _allPieces;
    }

    private List<IPieceModel_V2> CreateBluePieces()
    {
        List<IPieceModel_V2> pieces = new List<IPieceModel_V2>();

        for (int i = 0; i < 22; i++)
        {
            if ((i < 3) || (i >= 11 && i < 14))
            {
                pieces.Add(new AAttackingPiece_V2(i, PieceType.Bow, Team.Blue,
                 PieceType.Pegasus, _boardModel));
            }
            else if ((i >= 4 && i < 7) || (i >= 15 && i < 18))
            {
                pieces.Add(new AAttackingPiece_V2(i, PieceType.Sword, Team.Blue,
                PieceType.Bow, _boardModel));
            }
            else if ((i >= 8 && i < 11) || (i >= 19 && i < 22))
            {
                pieces.Add(new AAttackingPiece_V2(i, PieceType.Pegasus, Team.Blue,
                PieceType.Sword, _boardModel));
            }
        }

        return pieces;
    }

    private List<IPieceModel_V2> CreateRedPieces()
    {
        List<IPieceModel_V2> pieces = new List<IPieceModel_V2>();

        for (int i = 99; i < 121; i++)
        {
            if ((i < 102) || (i >= 110 && i < 113))
            {
                pieces.Add(new AAttackingPiece_V2(i, PieceType.Bow, Team.Red,
                PieceType.Pegasus, _boardModel));
            }
            else if ((i >= 103 && i < 106) || (i >= 114 && i < 117))
            {
                pieces.Add(new AAttackingPiece_V2(i, PieceType.Sword, Team.Red,
                PieceType.Bow, _boardModel));
            }
            else if ((i >= 107 && i < 110) || (i >= 118 && i < 121))
            {
                pieces.Add(new AAttackingPiece_V2(i, PieceType.Pegasus, Team.Red,
                PieceType.Sword, _boardModel));
            }
        }

        return pieces;
    }

}
