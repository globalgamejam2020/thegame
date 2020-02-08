using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campaign : MonoBehaviour {
  private AudioSource audioSource;
  public List<AudioClip> clips;
  public List<GameObject> scenes;
  public GameObject currentScene;
  private int currIndex = 0;
  public float timeUntilNextAudio;
    void Start() {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[0];
        audioSource.Play();
        scenes[0].SetActive(true);
        for (int i = 1; i < scenes.Count; i++) {
          scenes[i].SetActive(false);
        }
    }

    void Update() {
      if (Input.GetKeyDown(KeyCode.Space)){
        Next();
      }
    }

    public void Next() {
      scenes[currIndex].SetActive(false);
      scenes[currIndex].SetActive(true);
      currIndex++;
      audioSource.clip = clips[currIndex];
      audioSource.Play();
    }
}
