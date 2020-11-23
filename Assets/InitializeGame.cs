using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGame : MonoBehaviour
{
    public GameObject a1;
    public GameObject a2;
    public GameObject a3;
    public GameObject a4;
    public GameObject a5;
    public Canvas w1;
    public Canvas w2;
    public Canvas w3;
    public string password;

    Vector3 start = Vector3.zero;
    public void Start()
    {
        password = GameObject.FindGameObjectsWithTag("pass")[0].GetComponent<passwordInput>().password;
        if (password == "HH2PN")
        {
            a1.transform.position = start;
            a1.transform.GetChild(1).transform.gameObject.SetActive(true);
            w1.worldCamera = a1.transform.GetChild(1).GetComponent<Camera>();
            a1.GetComponent<MoveComponent>().enabled = true;
            a1.GetComponent<MoveComponent>().myPlayer = true;
        }
        else if (password == "JK982")
        {
            a2.transform.position = new Vector3(-14.8f, -1.24f, 0.0f);
            a2.transform.GetChild(1).transform.gameObject.SetActive(true);
            w1.worldCamera = a2.transform.GetChild(1).GetComponent<Camera>();
            a2.GetComponent<MoveComponent>().enabled = true;
            a2.GetComponent<MoveComponent>().myPlayer = true;
        }
        else if (password == "LC79K")
        {
            a3.transform.position = new Vector3(15.27f, 0f, 0f);
            a3.transform.GetChild(1).transform.gameObject.SetActive(true);
            w1.worldCamera = a3.transform.GetChild(1).GetComponent<Camera>();
            a3.GetComponent<MoveComponent>().enabled = true;
            a3.GetComponent<MoveComponent>().myPlayer = true;
        }
        else if (password == "TB2P1")
        {
            a4.transform.position = new Vector3(0f, -13.76f, 0f);
            a4.transform.GetChild(1).transform.gameObject.SetActive(true);
            w1.worldCamera = a4.transform.GetChild(1).GetComponent<Camera>();
            a4.GetComponent<MoveComponent>().enabled = true;
            a4.GetComponent<MoveComponent>().myPlayer = true;
        }
        else if (password == "GQ52P")
        {
            a5.transform.position = new Vector3(0f, -28.31f, 0f);
            a5.transform.GetChild(1).transform.gameObject.SetActive(true);
            w1.worldCamera = a5.transform.GetChild(1).GetComponent<Camera>();
            a5.GetComponent<MoveComponent>().enabled = true;
            a5.GetComponent<MoveComponent>().myPlayer = true;
        }
    }






}
