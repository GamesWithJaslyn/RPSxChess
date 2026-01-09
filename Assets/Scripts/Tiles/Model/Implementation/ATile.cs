// using System;
// // public enum TileType { None, Regular, Teleport, Water}

// public class ATile : ITileModel
// {
//     protected int _id;
//     protected TileType _type;

//     public ATile(int id, TileType type)
//     {
//         if (id < 0)
//         {
//             throw new ArgumentException("Tile ID cannot be negative!");
//         }

//         _id = id;
//         _type = type;
//     }

//     public int GetID()
//     {
//         return _id;
//     }


//     public TileType GetTileType()
//     {
//         return _type;
//     }

//     public ITileModel GetTile()
//     {
//         return this;
//     }

//     public void ChangeType(TileType type)
//     {
//         _type = type;
//     }
// }
