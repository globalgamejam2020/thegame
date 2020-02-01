using Global;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Data {
    public class GridObject {
        public readonly Tilemap Tilemap;
        public readonly Tile Tile;
        public readonly ObjectType Type;
        public readonly Vector3Int Position;

        public int Index() {
            return 0;
        }

        public static int ToIndex(Vector3Int position) {
            return 0;
        }

        public static Vector3Int FromIndex(int index) {
            return Vector3Int.zero;
        }
    }
}