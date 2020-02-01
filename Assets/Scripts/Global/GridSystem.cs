using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {
    public class GridSystem : MonoBehaviour {
        public static GridSystem Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }
    }
}