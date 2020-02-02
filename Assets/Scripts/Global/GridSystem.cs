using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Global {
    public class GridSystem : MonoBehaviour {
        public static GridSystem Instance { get; private set; }

        [SerializeField] private GameObject HumanPrefab;
        [SerializeField] private GameObject SquirrelPrefab;

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
                } else if ("Characters".Equals(tilemap.gameObject.name)) {
                    foreach (var position in tilemap.cellBounds.allPositionsWithin) {
                        var tile = tilemap.GetTile(position);
                        if (tile == null) continue;

                        PlaceCharacterPrefab(tile, position);
                        tilemap.SetTile(position, null);
                    }

                    tilemap.enabled = false;
                }
            }
        }
        
        public bool IsOutOfBounds(Vector3 position) {
            var bounds = GroundBounds();

            return position.x < bounds.xMin
                   || position.x > bounds.xMax
                   || position.y < bounds.yMin
                   || position.y > bounds.yMax;
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
            switch (name) {
                case "Tree":
                    return ObjectType.TREE;
                case "Grass":
                    return ObjectType.GRASS;
                case "Water":
                    return ObjectType.WATER;
                case "Building":
                    return ObjectType.BUILDING;
                case "Pavement":
                    return ObjectType.PAVEMENT;
                default:
                    return null;
            }
        }

        private void PlaceCharacterPrefab(TileBase tile, Vector3Int position) {
            switch (tile.name) {
                case "Human":
                    Instantiate(HumanPrefab, ToVector3(position), Quaternion.identity);
                    break;
                case "Squirrel":
                    Instantiate(SquirrelPrefab, ToVector3(position), Quaternion.identity);
                    break;
                default:
                    return;
            }
        }

        private static Vector3 ToVector3(Vector3Int v) {
            return new Vector3(v.x, v.y, v.z);
        }

        private static Vector3Int ToVector3Int(Vector3 v) {
            return new Vector3Int((int) (v.x), (int) (v.y), (int) v.z);
        }
    }
}