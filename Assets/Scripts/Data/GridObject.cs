using Global;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Data {
    public class GridObject {
        public readonly Tilemap Tilemap;
        public readonly BoundsInt TilemapBounds;
        public readonly Tile Tile;
        public readonly ObjectType Type;
        public readonly Vector3Int Position;

        public int Index() {
            return ToIndex(Position, TilemapBounds);
        }

        public static int ToIndex(Vector3Int position, BoundsInt bounds) {
            var size = bounds.size;
            return position.x + (position.y * size.x);
        }

        public static Vector3Int FromIndex(int index, BoundsInt bounds) {
            var size = bounds.size;
            var x = index % size.x;
            var y = (index - x) / size.x;

            return new Vector3Int(x, y, 0);
        }
    }
}