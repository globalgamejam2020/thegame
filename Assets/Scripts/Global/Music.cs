using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
    public class Music : MonoBehaviour
    {
        public AudioClip[] music;
        public AudioClip uhWhat;
        public AudioClip deathMusic;

        public static Music instance;

        private AudioSource audioSource;

        // Start is called before the first frame update
        void Start()
        {
            instance = this;

            int rand = Random.Range(0, music.Length);
            float pitch = Random.Range(0.9f, 1.1f);

            audioSource = GetComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.volume = 0.4f;
            audioSource.pitch = pitch;
            audioSource.clip = music[rand];
            audioSource.Play();
        }

        public void GameOver()
        {
            audioSource.Stop();
            audioSource.loop = false;
            Invoke("UhWhat", 2f);
            Invoke("DeathMusic", 6f);
        }

        private void UhWhat()
        {
            audioSource.volume = 1;
            audioSource.clip = uhWhat;
            audioSource.Play();
        }

        private void DeathMusic()
        {
            audioSource.volume = 0.5f;
            audioSource.clip = deathMusic;
            audioSource.Play();
        }
    }
}
