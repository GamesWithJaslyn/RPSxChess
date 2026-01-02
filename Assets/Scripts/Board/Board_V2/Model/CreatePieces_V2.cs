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

        // _allPieces.AddRange(CreateBlueBows());
        // _allPieces.AddRange(CreateRedBows());
        // _allPieces.AddRange(CreateBlueSwords());
        // _allPieces.AddRange(CreateRedSwords());
        // _allPieces.AddRange(CreateBluePegasus());
        // _allPieces.AddRange(CreateRedPegasus());
    }

    public List<IPieceModel_V2> GetPieces()
    {
        return _allPieces;
    }

    // /// <summary>
    // /// Instaniates Blue Bows.
    // /// </summary>
    // /// <returns> A List containing the blue bows instantiated. </returns>
    // private List<AAttackingPiece_V2> CreateBlueBows()
    // {
    //     List<AAttackingPiece_V2> blueBows = new List<AAttackingPiece_V2>();

    //     for (int i = 0; i < 3; i++)
    //     {
    //         AAttackingPiece blueBow = new Bow(i, 1, -3, _boardModel);
    //         blueBows.Add(blueBow);
    //     }

    //     for (int i = 11; i < 14; i++)
    //     {
    //         AAttackingPiece blueBow = new Bow(i, 1, -3, _boardModel);
    //         blueBows.Add(blueBow);
    //     }
    //     return blueBows;
    // }

    // /// <summary>
    // /// Instaniates Blue Swords.
    // /// </summary>
    // /// <returns> A List containing the blue swords instantiated. </returns>
    // private List<AAttackingPiece> CreateBlueSwords()
    // {
    //     List<AAttackingPiece> blueSwords = new List<AAttackingPiece>();

    //     for (int i = 4; i < 7; i++)
    //     {
    //         AAttackingPiece blueSword = new Sword(i, 2, -1, _boardModel);
    //         blueSwords.Add(blueSword);
    //     }

    //     for (int i = 15; i < 18; i++)
    //     {
    //         AAttackingPiece blueSword = new Sword(i, 2, -1, _boardModel);
    //         blueSwords.Add(blueSword);
    //     }

    //     return blueSwords;
    // }

    // /// <summary>
    // /// Instaniates Blue Pegasi.
    // /// </summary>
    // /// <returns> A List containing the blue pegasi instantiated. </returns>
    // private List<AAttackingPiece> CreateBluePegasus()
    // {
    //     List<AAttackingPiece> bluePegasi = new List<AAttackingPiece>();

    //     for (int i = 8; i < 11; i++)
    //     {
    //         AAttackingPiece bluePegasus = new Pegasus(i, 3, -2, _boardModel);
    //         bluePegasi.Add(bluePegasus);
    //     }

    //     for (int i = 19; i < 22; i++)
    //     {
    //         AAttackingPiece bluePegasus = new Pegasus(i, 3, -2, _boardModel);
    //         bluePegasi.Add(bluePegasus);
    //     }

    //     return bluePegasi;
    // }

    // /// <summary>
    // /// Instaniates Red Bows.
    // /// </summary>
    // /// <returns> A List containing the red bows instantiated. </returns>
    // private List<AAttackingPiece> CreateRedBows()
    // {
    //     List<AAttackingPiece> redBows = new List<AAttackingPiece>();

    //     for (int i = 99; i < 102; i++)
    //     {
    //         AAttackingPiece redBow = new Bow(i, -1, 3, _boardModel);
    //         redBows.Add(redBow);
    //     }

    //     for (int i = 110; i < 113; i++)
    //     {
    //         AAttackingPiece redBow = new Bow(i, -1, 3, _boardModel);
    //         redBows.Add(redBow);
    //     }

    //     return redBows;
    // }

    // /// <summary>
    // /// Instaniates Red Swords.
    // /// </summary>
    // /// <returns> A List containing the red swords instantiated. </returns>
    // private List<AAttackingPiece> CreateRedSwords()
    // {
    //     List<AAttackingPiece> redSwords = new List<AAttackingPiece>();

    //     for (int i = 103; i < 106; i++)
    //     {
    //         AAttackingPiece redSword = new Sword(i, -2, 1, _boardModel);
    //         redSwords.Add(redSword);
    //     }

    //     for (int i = 114; i < 117; i++)
    //     {
    //         AAttackingPiece redSword = new Sword(i, -2, 1, _boardModel);
    //         redSwords.Add(redSword);
    //     }

    //     return redSwords;
    // }

    // /// <summary>
    // /// Instaniates red Pegasi.
    // /// </summary>
    // /// <returns> A List containing the red pegasi instantiated. </returns>
    // private List<AAttackingPiece> CreateRedPegasus()
    // {
    //     List<AAttackingPiece> redPegasi = new List<AAttackingPiece>();

    //     for (int i = 107; i < 110; i++)
    //     {
    //         AAttackingPiece redPegasus = new Pegasus(i, -3, 2, _boardModel);
    //         redPegasi.Add(redPegasus);
    //     }

    //     for (int i = 118; i < 121; i++)
    //     {
    //         AAttackingPiece redPegasus = new Pegasus(i, -3, 2, _boardModel);
    //         redPegasi.Add(redPegasus);
    //     }

    //     return redPegasi;
    // }
}
