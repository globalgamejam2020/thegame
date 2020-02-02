using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Player {
    public class LinkScene: MonoBehaviour {
        [SerializeField] private String scene;

        private void OnMouseUp() {
            SceneManager.LoadScene(scene);
        }
    }
}