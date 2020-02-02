using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Global {
    public class GridSystem : MonoBehaviour {
        public static GridSystem Instance { get; private set; }

        [SerializeField] private GameObject HumanPrefab;
        [SerializeField] private GameObject SquirrelPrefab;
        [SerializeField] private GameObject TreePrefab;

        private Tilemap ground;
        private Tilemap objects;

        private void Awake() {
            Instance = this;

            var tilemaps = GetComponentsInChildren<Tilemap>();
            foreach (var tilemap in tilemaps) {
                if ("Ground".Equals(tilemap.gameObject.name)) {
                    ground = tilemap;
                } else if ("Objects".Equals(tilemap.gameObject.name)) {
                    objects = tilemap;
                    ReplaceByPrefab(tilemap);
                } else if ("Characters".Equals(tilemap.gameObject.name)) {
                    ReplaceByPrefab(tilemap);
                    tilemap.enabled = false;
                }
            }
        }

        private void ReplaceByPrefab(Tilemap tilemap) {
            foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                var tile = tilemap.GetTile(position);
                if (tile == null) continue;

                if (PlaceCharacterPrefab(tile, position)) tilemap.SetTile(position, null);
            }
        }

        public bool IsOutOfBounds(Vector3 position) {
            var bounds = GroundBounds();

            return position.x < bounds.xMin
                   || position.x >= bounds.xMax
                   || position.y < bounds.yMin
                   || position.y >= bounds.yMax;
        }

        public BoundsInt GroundBounds() {
            ground.CompressBounds();
            return ground.cellBounds;
        }

        public bool AllowsVision(Vector3 position) {
            var tile = objects.GetTile(ToVector3Int(position));
            if (ReferenceEquals(tile, null)) return true;

            var type = GetType(tile.name);
            return type?.AllowsVision() ?? true;
        }

        public bool AllowsMovement(Vector3 position) {
            if (IsOutOfBounds(position)) return false;
            var tile = objects.GetTile(ToVector3Int(position));
            if (ReferenceEquals(tile, null)) return true;

            var type = GetType(tile.name);
            return type?.AllowsMovement() ?? true;
        }

        private ObjectType? GetType([CanBeNull] String name) {
            if (ReferenceEquals(name, null)) return null;
            name = name.ToLower();

            if (name.StartsWith("bldg")) return ObjectType.BUILDING;
            if (name.StartsWith("grass")) return ObjectType.GRASS;
            if (name.StartsWith("water")) return ObjectType.WATER;
            if (name.StartsWith("pvmt")) return ObjectType.PAVEMENT;
            if (name.StartsWith("tree")) return ObjectType.TREE;

            return null;
        }

        private bool PlaceCharacterPrefab(TileBase tile, Vector3Int position) {
            if (ReferenceEquals(tile, null)) return false;
            var name = tile.name.ToLower();

            if (name.StartsWith("human")) return InstantiateTile(HumanPrefab, position);
            if (name.StartsWith("squirrel")) return InstantiateTile(SquirrelPrefab, position);
            if (name.StartsWith("tree")) return InstantiateTile(TreePrefab, position);
            return false;
        }

        private bool InstantiateTile(GameObject prefab, Vector3Int position) {
            Instantiate(prefab, ToVector3(position), Quaternion.identity);
            return true;
        }

        private static Vector3 ToVector3(Vector3Int v) {
            return new Vector3(v.x, v.y, v.z);
        }

        private static Vector3Int ToVector3Int(Vector3 v) {
            return new Vector3Int((int) (v.x), (int) (v.y), (int) v.z);
        }
    }
}