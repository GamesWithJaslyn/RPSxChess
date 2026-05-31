using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class ASpecialAttack : ISpecialAttack
{
    public abstract bool IsSpecialAttackReady { get; protected set; }
    public abstract List<int> GetSpecialAttackRange(IBoardModel board, IPieceModel piece);
    public abstract Task<bool> ExecuteSpecialAttack(IBoardModel board, IPieceModel piece, int targetTileID);
    public void CancelSpecialAttack(IBoardModel board)
    {
        IsSpecialAttackReady = false;
        ((AAttackingPiece)board.GetSelectedPiece())?.CallHideArrow();
        board.UnHighlightAllTiles();
    }
}
