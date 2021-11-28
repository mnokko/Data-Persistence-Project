using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

// Sets the script to be executed later than all default scripts
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public InputField playerNameField;
    public string pname;

    public void NewNameEntered(string pname)
    {
        pname = playerNameField.text;
        //Debug.Log(pname);
        //Debug.Log("Instanssi:");
        //Debug.Log(DataManager.Instance.playerName);
        DataManager.Instance.playerName = pname;
        //Debug.Log(DataManager.Instance.playerName);

    }
    //Pelin aloittaminen.
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {

      
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
