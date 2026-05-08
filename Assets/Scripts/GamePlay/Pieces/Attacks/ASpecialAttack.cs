using System.Collections.Generic;
using UnityEngine;

public abstract class ASpecialAttack : ISpecialAttack
{
    public abstract bool IsSpecialAttackReady { get; protected set; }
    public abstract List<int> GetSpecialAttackRange(IBoardModel board, IPieceModel piece);
    public abstract bool ExecuteSpecialAttack(IBoardModel board, IPieceModel piece, int targetTileID);
    public void CancelSpecialAttack(IBoardModel board)
    {
        IsSpecialAttackReady = false;
        board.UnHighlightAllTiles();

    }
}
