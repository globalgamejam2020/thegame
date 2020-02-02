using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data;

namespace Component
{
    public class Squirrel : MonoBehaviour
    {
        private bool rabid = false;
        private float overlapRadius;

        private Movement movement;
        private AnimationController animationController;
        private AudioController audioController;

        // Start is called before the first frame update
        void Start()
        {
            movement = GetComponent<Movement>();
            animationController = GetComponent<AnimationController>();
            audioController = GetComponent<AudioController>();

            float rabidInvoke = Random.Range(10f, 20f);
            Invoke("MakeRabid", rabidInvoke);
            Invoke("MoveRandom", 2f);
        }

        // Update is called once per frame
        void Update()
        {
            if (rabid && !IsInvoking("Attack")) Invoke("Attack", 0f);

            
        }

        private void MoveRandom()
        {
            movement.Move(GetRandomMoveDirection());
            if (!rabid) audioController.PlayFX("move");
            else audioController.PlayFX("move2");
        }

        private MovementDirection GetRandomMoveDirection()
        {
            int randv = Random.Range(0, 4);
            int randh = Random.Range(0, 4);
            MovementDirection vertical = (MovementDirection) (1 << randv);
            MovementDirection horizontal = (MovementDirection) (1 << randh);

            float nextInvoke = Random.Range(1f, 5f);
            Invoke("MoveRandom", nextInvoke);

            return vertical | horizontal;
        }

        private void Attack()
        {
            animationController.Attack();

            float nextInvoke = Random.Range(0.5f, 3f);
            Invoke("Attack", nextInvoke);
        }

        private void MakeRabid()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, overlapRadius);

            rabid = true;
            animationController.Rabid();
        }

        public bool IsRabid()
        {
            return rabid;
        }
    }
}
