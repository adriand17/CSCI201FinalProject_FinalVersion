using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PasswordInputField : MonoBehaviour
{

    public InputField inputfield;
    public GameObject pass;

    public void CheckInput()
    {
        if (inputfield.text == "HH2PN" || inputfield.text == "JK982" || inputfield.text == "LC79K" || inputfield.text == "TB2P1" || inputfield.text == "GQ52P")      // check inputfield contains the string password
        {
            //for register player
            Debug.Log("Password accepted");     // just a debug.Log to show that the password is correct (can be removed)
            pass.GetComponent<passwordInput>().password = inputfield.text;
            SceneManager.LoadScene("MainLevel");  // fill in the name of the scene you want to load
        }
        if (inputfield.text == "MCA82" || inputfield.text == "ASC34" || inputfield.text == "ASD8S" || inputfield.text == "PASD1" || inputfield.text == "ERT65")      // check inputfield contains the string password
        {
            //for guest player
            Debug.Log("Password accepted");     // just a debug.Log to show that the password is correct (can be removed)
            SceneManager.LoadScene("DemoLevel");  // fill in the name of the scene you want to load
        }
    }
}

