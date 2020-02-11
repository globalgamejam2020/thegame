using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component;

public class TreeController : MonoBehaviour, IsTree {
    private GameObject grass1; 
    private GameObject grass2;
    private Animator animator;
    private System.DateTime age;

    private void Start() {
        animator = GetComponent<Animator>();
        grass1 = Resources.Load<GameObject>("GameObjects/Grass1"); 
        grass2 = Resources.Load<GameObject>("GameObjects/Grass2");
        age = System.DateTime.Now;
    }

    public void PlantTree(int strength) {
        Invoke("SparkleSoundFX", 0.2f);
        Invoke("Sprout", 2f);
    }

    private void SparkleSoundFX() {
        GetComponent<AudioController>().PlayFX("move");
    }

    public void Sprout() {
        animator.SetTrigger("Sprout");
        Invoke("Grow", 2f);
    }

    public void Grow() {
        animator.SetTrigger("Grow");
        Invoke("GrowGrass1", 2f);
    }

    public void GrowGrass1() {
        Instantiate(grass1, transform.position, Quaternion.identity);
        Invoke("GrowGrass2", 2f);
    }

    public void GrowGrass2() {
        Instantiate(grass2, transform.position, Quaternion.identity);
    }

    public System.DateTime getAge() {
        return age;
    }
}
