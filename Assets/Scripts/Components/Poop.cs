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

        public Poop(Vector3 position)
        {
            this.position = position;
            int poopSize = Random.Range(1, 4);
            effectRadius = poopSize;

            poop = Resources.Load<GameObject>("GameObjects/poop"+poopSize);
            GameObject poopInstance = Instantiate(poop, position, Quaternion.identity);
            poopInstance.transform.position = new Vector3(poopInstance.transform.position.x, poopInstance.transform.position.y, -2f);
            Poop poopComponent = poopInstance.AddComponent<Poop>();
            poopComponent = this;
        }

        // Update is called once per frame
        void Update()
        {
            //tree radius growing code
        }
    }
}
