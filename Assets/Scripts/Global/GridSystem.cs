using System;
using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Global {
    public class GridSystem : MonoBehaviour {
        public static GridSystem Instance { get; private set; }

        private GridObject[] Ground;
        private GridObject[] Objects;

        private void Awake() {
            Instance = this;

            var tilemaps = GetComponentsInChildren<Tilemap>();
            foreach (var tilemap in tilemaps) {
                var bounds = tilemap.cellBounds;
                var tiles = tilemap.GetTilesBlock(bounds);

                Debug.Log($"Tilemap: {tilemap.name}, Bounds: ({bounds.xMin}, {bounds.yMin}, {bounds.zMin}), ({bounds.size.x}, {bounds.size.y}, {bounds.size.z})");

                var objects = new GridObject[bounds.size.x * bounds.size.y * bounds.size.z];
                
                for (int x = bounds.xMin; x < bounds.xMax; x++) {
                    for (int y = bounds.yMin; y < bounds.yMax; y++) {
                        for (int z = bounds.zMin; z < bounds.zMax; z++) {
                            bool hasTile = tilemap.HasTile(new Vector3Int(x, y, z));

                            // if (hasTile)
                                // objects[]
                        }
                    }
                }
            }
        }
    }
}