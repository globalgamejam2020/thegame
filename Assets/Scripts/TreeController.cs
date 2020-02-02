using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component;

public class TreeController : MonoBehaviour {
    [SerializeField] int treeStrength = 1;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlantTree(int strength)
    {
        this.treeStrength = strength;
        Invoke("SparkleSoundFX", 0.2f);
        Invoke("Sprout", 5f / treeStrength);
    }

    private void SparkleSoundFX()
    {
        GetComponent<AudioController>().PlayFX("move");
    }

    public void Sprout()
    {
        animator.SetTrigger("Sprout");
        Invoke("Grow", 5f / treeStrength);
    }

    public void Grow()
    {
        animator.SetTrigger("Grow");
    }
}
