using UnityEngine;
using Random = UnityEngine.Random;

namespace Player {
    public class CameraShake : MonoBehaviour {
        [SerializeField] private float duration = 0f;
        [SerializeField] private float amount = 0.7f;
        [SerializeField] private float decreaseFactor = 1.0f;

        public void Shake(float duration) {
            this.duration = duration;
        }

        void Update() {
            if (duration > 0) {
                transform.localPosition = Random.insideUnitSphere * amount;
                duration -= Time.deltaTime * decreaseFactor;
            } else {
                duration = 0f;
                transform.localPosition = Vector3.zero;
            }
        }
    }
}