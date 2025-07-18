// using System.Collections.Generic;

// /// <summary>
// /// Represents a Piece in an RPSxChess game.
// /// The implementation of the IPieceModel.
// /// </summary>
// public class PieceModelImpl : IPieceModel
// {
//     private int _pos;
//     private List<int> _targets;
//     private List<int> _targetTiles;
//     private List<int> _moveTiles;
//     private List<int> _validMoveTiles;
//     private int _pieceType;
//     private bool _isSelected;
//     private bool _isAlive;

//     public static List<IPieceModel> _allPieces;

//     public PieceModelImpl(int pos, int targetType, int pieceType)
//     {
//         _allPieces = new List<IPieceModel>();

//         _pos = pos;
//         _targets = new List<int>();
//         _targetTiles = new List<int>();
//         _moveTiles = new List<int>();
//         _validMoveTiles = new List<int>();
//         _pieceType = pieceType;
//         _isSelected = false;

//         _allPieces.Add(this);
//     }

//     public void SetPos(int tile) 
//     {
//         _pos = tile;
//     }
//     public int GetPos()
//     {
//         return _pos;
//     }

//     public int GetPieceType() {
//         return _pieceType;
//     }

//     public bool IsSelected() {
//         return _isSelected;
//     }

//     public void SetSelected() {
//         _isSelected = true;
//         foreach (IPieceModel piece in _allPieces)
//         {
//             if (piece != this)
//             {
//                 piece.TurnSelectedFalse();
//             }
//         }
//     }

//     public void TurnSelectedFalse() {
//         this._isSelected = false;
//     }

//     public bool IsAlive() {
//         return _isAlive;
//     }

//     public void SetAliveOrDead(bool alive) {
//         _isAlive = alive;
//     }

// }
