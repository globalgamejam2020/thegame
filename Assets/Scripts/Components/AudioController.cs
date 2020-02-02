using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Component
{
    public class AudioController : MonoBehaviour
    {
        private Dictionary<string, List<AudioClip>> FX_dict = new Dictionary<string, List<AudioClip>>();
        public List<AudioClip> moveFX;
        public List<AudioClip> modalFX;
        public List<AudioClip> finalFX;

        private AudioSource audioSource;

        private HashSet<int> moveFXIndexSet = new HashSet<int>();

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            FX_dict.Add("move", moveFX);
            FX_dict.Add("modal", modalFX);
            FX_dict.Add("final", finalFX);

            InitHashSet();

        }

        private void InitHashSet()
        {
            moveFXIndexSet.Clear();
            for (int i = 0; i < moveFX.Count; i++)
            {
                moveFXIndexSet.Add(i);
            }
        }

        private int PickFromHashSet(int index)
        {
            if (moveFXIndexSet.Contains(index)) 
            moveFXIndexSet.Remove(index);
            return index;
        }

        public void PlayFX(string key)
        {
            if (moveFXIndexSet.Count == 0) InitHashSet();
            //if (!forcePlay && audioSource.isPlaying) return;

            int rand = Random.Range(0, moveFX.Count);
            int index = PickFromHashSet(rand);
            audioSource.clip = FX_dict[key][index];
            audioSource.Play();
        }
    }
}
