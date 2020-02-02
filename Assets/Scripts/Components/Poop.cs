using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Component
{
    public class Poop : MonoBehaviour
    {
        [SerializeField] private GameObject poop;
        private Vector3 position;
        [SerializeField] private int effectRadius;

        private Sprite[] poopSprites = new Sprite[3];
        private SpriteRenderer spriteRenderer;

        private int animationFrame = 0;

        public Poop(Vector3 position)
        {
            LoadSprites();

            this.position = position;
            int maxPoopSize = Random.Range(0, 3);
            effectRadius = maxPoopSize;

            poop = Resources.Load<GameObject>("GameObjects/poop");
            GameObject poopInstance = Instantiate(poop, position, Quaternion.identity);
            poopInstance.transform.position = new Vector3(poopInstance.transform.position.x, poopInstance.transform.position.y, -2f); //for camera z-depth issues
            Poop poopComponent = poopInstance.AddComponent<Poop>();
            SetPoopComponent(poopComponent);
        }

        private void SetPoopComponent(Poop poo)
        {
            poo.effectRadius = this.effectRadius;
            poo.spriteRenderer = poo.GetComponent<SpriteRenderer>();
            poo.poopSprites = this.poopSprites;
            poo.position = this.position;
            poo.InvokeRepeating("AnimatePoop", 0f, 0.15f);
        }

        private void LoadSprites()
        {
            for (int i = 0; i < 3; i++)
            {
                poopSprites[i] = Resources.Load<Sprite>("Sprites/poop" + i);
            }
        }

        private void AnimatePoop()
        {
            if (animationFrame > effectRadius) { CancelInvoke(); return; }

            spriteRenderer.sprite = poopSprites[animationFrame];
            animationFrame++;
        }

        // Update is called once per frame
        void Update()
        {
            //tree radius growing code
        }
    }
}
