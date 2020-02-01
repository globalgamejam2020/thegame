using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global {
    public class Grid : MonoBehaviour {
        public static Grid Instance { get; private set; }

        private void Awake() {
            Instance = this;
        }
    }
}