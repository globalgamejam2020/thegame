using System.Collections;
using System.Collections.Generic;
using Data;
using UnityEngine;

namespace Component {
    public class Poop : MonoBehaviour {
        [SerializeField] private GameObject poop;
        [SerializeField] private int effectRadius;

        private Sprite[] poopSprites = new Sprite[3];
        private SpriteRenderer spriteRenderer;

        private Vector3 finalPosition;
        private bool moving = true;
        private int animationFrame = 0;
        private bool shouldUpdate = true;

        public Poop(Vector3 initial, Vector3 position) {
            position.z = -2f;
            initial.z = -2f;
            LoadSprites();

            this.finalPosition = position;
            int maxPoopSize = Random.Range(0, 3);
            effectRadius = maxPoopSize;

            poop = Resources.Load<GameObject>("GameObjects/poop");
            GameObject poopInstance = Instantiate(poop, initial, Quaternion.identity);
            Poop poopComponent = poopInstance.AddComponent<Poop>();
            SetPoopComponent(poopComponent);
        }

        private void SetPoopComponent(Poop poo) {
            poo.effectRadius = this.effectRadius;
            poo.spriteRenderer = poo.GetComponent<SpriteRenderer>();
            poo.poopSprites = this.poopSprites;
            poo.finalPosition = finalPosition;
            poo.moving = true;
            poo.shouldUpdate = true;
            // Destroy(poo.GetComponent<GameObject>(), 2f);
            poo.InvokeRepeating("AnimatePoop", 0f, 0.15f);
        }

        private void LoadSprites() {
            for (int i = 0; i < 3; i++) {
                poopSprites[i] = Resources.Load<Sprite>("Sprites/poop" + i);
            }
        }

        private void AnimatePoop() {
            if (animationFrame > effectRadius) { CancelInvoke(); return; }

            spriteRenderer.sprite = poopSprites[animationFrame];
            animationFrame++;
        }

        void Update() {
            Debug.Log($"IsMoving: {IsMoving()}, final: {finalPosition}, position: {transform.position}");
            if (IsMoving()) Move();
            if(shouldUpdate) {
                Destroy(this, 2.0f);
                GameObject tree = Resources.Load<GameObject>("GameObjects/Tree");
                Debug.Log(tree);
                Instantiate(tree, this.finalPosition, Quaternion.identity);
                shouldUpdate = false;
            }
        }

        private void Move() {
            transform.position = Vector3.Lerp(transform.position, finalPosition, 10 * Time.deltaTime);
            var distanceRemaining = (finalPosition - transform.position).sqrMagnitude;
            if (distanceRemaining < 0.01f) {
                moving = false;
                transform.position = finalPosition;
            }
        }

        private bool IsMoving() {
            return moving;
        }
    }
}
