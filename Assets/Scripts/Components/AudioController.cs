using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Component
{
    public class AudioController : MonoBehaviour
    {
        private Dictionary<string, List<AudioClip>> FX_dict;
        public List<AudioClip> moveFX;
        public List<AudioClip> modalFX;
        public List<AudioClip> finalFX;

        private AudioSource audioSource;

        // Start is called before the first frame update
        void Start()
        {
            audioSource = GetComponent<AudioSource>();

            FX_dict.Add("move", moveFX);
            FX_dict.Add("modal", modalFX);
            FX_dict.Add("final", finalFX);

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlayFX(string key, bool forcePlay)
        {
            if (!forcePlay && audioSource.isPlaying) return;

            int rand = Random.Range(0, FX_dict[key].Count);
            audioSource.clip = FX_dict[key][rand];
            audioSource.Play();
        }
    }
}
