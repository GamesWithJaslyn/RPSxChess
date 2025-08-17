using UnityEngine;
using System.Collections.Generic;


//Creates the basic three piece types, on both teams.
public class CreatePieces
{
    private List<AAttackingPiece> _allPieces;

    public CreatePieces()
    {
        _allPieces = new List<AAttackingPiece>();
        _allPieces.Clear();

        _allPieces.AddRange(CreateBlueBows());
                Debug.Log("------------------------");

        _allPieces.AddRange(CreateRedBows());
                Debug.Log("------------------------");

        _allPieces.AddRange(CreateBlueSwords());
                Debug.Log("------------------------");

        _allPieces.AddRange(CreateRedSwords());
                Debug.Log("------------------------");

        _allPieces.AddRange(CreateBluePegasus());
                Debug.Log("------------------------");

        _allPieces.AddRange(CreateRedPegasus());
                Debug.Log("------------------------");

        Debug.Log($"[CreatePieces] Total pieces created: {_allPieces.Count}");
    
    }

    public List<AAttackingPiece> GetPieces()
    {
        return _allPieces;
    }

    /// <summary>
    /// Instaniates Blue Bows.
    /// </summary>
    /// <returns> A List containing the blue bows instantiated. </returns>
    private List<AAttackingPiece> CreateBlueBows()
    {
        List<AAttackingPiece> blueBows = new List<AAttackingPiece>();

        for (int i = 0; i < 3; i++)
        {
            AAttackingPiece blueBow = new Bow(i, 1, -3);
            blueBows.Add(blueBow);
        }

        for (int i = 11; i < 14; i++)
        {
            AAttackingPiece blueBow = new Bow(i, 1, -3);
            blueBows.Add(blueBow);
        }
        return blueBows;
    }

    /// <summary>
    /// Instaniates Blue Swords.
    /// </summary>
    /// <returns> A List containing the blue swords instantiated. </returns>
    private List<AAttackingPiece> CreateBlueSwords()
    {
        List<AAttackingPiece> blueSwords = new List<AAttackingPiece>();

        for (int i = 4; i < 7; i++)
        {
            AAttackingPiece blueSword = new Sword(i, 2, -1);
            blueSwords.Add(blueSword);
        }

        for (int i = 15; i < 18; i++)
        {
            AAttackingPiece blueSword = new Sword(i, 2, -1);
            blueSwords.Add(blueSword);
        }

        return blueSwords;
    }

    /// <summary>
    /// Instaniates Blue Pegasi.
    /// </summary>
    /// <returns> A List containing the blue pegasi instantiated. </returns>
    private List<AAttackingPiece> CreateBluePegasus()
    {
        List<AAttackingPiece> bluePegasi = new List<AAttackingPiece>();

        for (int i = 8; i < 11; i++)
        {
            AAttackingPiece bluePegasus = new Pegasus(i, 3, -2);
            bluePegasi.Add(bluePegasus);
        }

        for (int i = 19; i < 22; i++)
        {
            AAttackingPiece bluePegasus = new Pegasus(i, 3, -2);
            bluePegasi.Add(bluePegasus);
        }

        return bluePegasi;
    }

    /// <summary>
    /// Instaniates Red Bows.
    /// </summary>
    /// <returns> A List containing the red bows instantiated. </returns>
    private List<AAttackingPiece> CreateRedBows()
    {
        List<AAttackingPiece> redBows = new List<AAttackingPiece>();

        for (int i = 99; i < 102; i++)
        {
            AAttackingPiece redBow = new Bow(i, -1, 3);
            redBows.Add(redBow);
        }

        for (int i = 110; i < 113; i++)
        {
            AAttackingPiece redBow = new Bow(i, -1, 3);
            redBows.Add(redBow);
        }

        return redBows;
    }

    /// <summary>
    /// Instaniates Red Swords.
    /// </summary>
    /// <returns> A List containing the red swords instantiated. </returns>
    private List<AAttackingPiece> CreateRedSwords()
    {
        List<AAttackingPiece> redSwords = new List<AAttackingPiece>();

        for (int i = 103; i < 106; i++)
        {
            AAttackingPiece redSword = new Sword(i, -2, 1);
            redSwords.Add(redSword);
        }

        for (int i = 114; i < 117; i++)
        {
            AAttackingPiece redSword = new Sword(i, -2, 1);
            redSwords.Add(redSword);
        }

        return redSwords;
    }

    /// <summary>
    /// Instaniates red Pegasi.
    /// </summary>
    /// <returns> A List containing the red pegasi instantiated. </returns>
    private List<AAttackingPiece> CreateRedPegasus()
    {
        List<AAttackingPiece> redPegasi = new List<AAttackingPiece>();

        for (int i = 107; i < 110; i++)
        {
            AAttackingPiece redPegasus = new Pegasus(i, -3, 2);
            redPegasi.Add(redPegasus);
        }

        for (int i = 118; i < 121; i++)
        {
            AAttackingPiece redPegasus = new Pegasus(i, -3, 2);
            redPegasi.Add(redPegasus);
        }

        return redPegasi;
    }
}
