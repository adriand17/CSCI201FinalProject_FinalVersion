using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passwordInput : MonoBehaviour
{
    public string password;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform);
    }
}
