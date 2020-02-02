using UnityEngine;

namespace Player {
    public class UnicornWalkingBehaviour : StateMachineBehaviour {
        private CameraShake shake;

        override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            shake = Camera.main.GetComponent<CameraShake>();
        }

        override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            shake.Shake(0.1f);
        }
    }
}