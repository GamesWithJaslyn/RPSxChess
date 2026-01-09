// using System;
// using UnityEngine;

// public class AEnterableTile : ATile, IEnterAndLeave
// {
//     protected IPieceModel_V2 _piece;
//     protected bool _occupied;
//     public bool _isValidMoveTileForAPiece;
//     private IEnterOperation _operation;


//     public AEnterableTile(int id, TileType type, IPieceModel_V2 piece) : base(id, type)
//     {
//         _id = id;
//         _type = type;
//         _occupied = piece != null;
//         _piece = piece;
//         _isValidMoveTileForAPiece = false;
//     }

//     public void IsValidTile_CanMoveHere(bool isValid)
//     {
//         _isValidMoveTileForAPiece = isValid;
//     }

//     public bool CanPieceMoveHere()
//     {
//         return _isValidMoveTileForAPiece;
//     } 


//     public IPieceModel_V2 GetPiece()
//     {
//         return _piece;
//     }

//     public IEnterOperation GetEnterOperation()
//     {
//         return _operation;
//     }

//     public void Enter(IPieceModel_V2 piece)
//     {
//         _piece = piece;
//     }

//     public bool CanEnter(IPieceModel_V2 piece)
//     {
//         if (_piece == null || piece is AAttackingPiece_V2 attack 
//         && _piece.GetPieceType() == attack.GetTargetType()
//         || !_piece.IsAlive())
//         {
//             return true;
//         }
//         else
//         {
//             return false;
//         }
//     }

//     public void Leave()
//     {
//         if (_piece != null)
//         {
//             _piece = null;
//         }
//     }
    
//       public bool IsOccupied()
//     {
//         return _piece != null;
//     }
// }
