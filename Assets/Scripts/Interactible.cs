using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    public GameObject game;
    private GameObject highlight;

    private void OnEnable()
    {
        highlight = transform.GetChild(0).gameObject;

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            highlight.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            highlight.SetActive(false);
        }
    }

    public void Play(MoveComponent ms)
    {
        Debug.Log("HEREA");
        game.GetComponent<WireTask>().fix = ms;
        game.SetActive(true);
    }
}
