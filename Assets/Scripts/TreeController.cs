using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component;

public class TreeController : MonoBehaviour {
    private GameObject grass1; 
    private GameObject grass2;
    [SerializeField] int treeStrength = 1;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
        grass1 = Resources.Load<GameObject>("GameObjects/Grass1"); 
        grass2 = Resources.Load<GameObject>("GameObjects/Grass2");
    }

    public void PlantTree(int strength) {
        this.treeStrength = strength;
        Invoke("SparkleSoundFX", 0.2f);
        Invoke("Sprout", 5f / treeStrength);
    }

    private void SparkleSoundFX() {
        GetComponent<AudioController>().PlayFX("move");
    }

    public void Sprout() {
        animator.SetTrigger("Sprout");
        Invoke("Grow", 10f / treeStrength);
    }

    public void Grow() {
        animator.SetTrigger("Grow");
        Invoke("GrowGrass1", 10f);
    }

    public void GrowGrass1() {
        Instantiate(grass1, transform.position, Quaternion.identity);
        Invoke("GrowGrass2", 10f);
    }

    public void GrowGrass2() {
        Instantiate(grass2, transform.position, Quaternion.identity);
    }
}
