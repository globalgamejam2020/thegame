using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Component;
using Player;

public class VisionCone : MonoBehaviour
{
    public LayerMask transparentFX;
    private Movement movement;

    private void Start()
    {
        movement = transform.parent.GetComponent<Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Detected UNICORN!");
            if (LineOfSight(collision.gameObject)) GameOver(collision.gameObject);
        }
    }

    private bool LineOfSight(GameObject collision)
    {
        Vector2 rayDirection = collision.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, rayDirection, 5f, ~transparentFX);
        if (hit.collider.tag == "Player") return true;
        else { Debug.Log("blocked by: " + hit.collider.name); return false; }
    }

    public void GameOver(GameObject unicorn)
    {
        Destroy(transform.parent.GetComponent<HumanController>());
        movement.ForceStop();
        movement.TurnTo(unicorn.transform);
        transform.parent.GetComponent<Animator>().SetTrigger("Idle");

        unicorn.GetComponent<AnimationController>().HORSE();
        unicorn.GetComponent<Movement>().ForceStop();
        //unicorn.GetComponent<Movement>().TurnTo(transform);
        unicorn.GetComponent<Player.Player>().enabled = false;
        unicorn.GetComponent<AudioController>().PlayFX("final");
        Debug.LogWarning("GAME IS OVER!");

        Global.Music.instance.GameOver();

        Destroy(this);
    }
}
