using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public InputField playerNameField;
    public string pname;
    
    // Start is called before the first frame update
    void Start()
    {
       //playerNameField.onValueChanged.AddListener(NewNameEntered);


    }

    // Update is called once per frame
    void Update()
    {
     
    }

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

    public void NewNameEntered(string pname)
    {
        pname = playerNameField.text;
        Debug.Log(pname);
        
       //MainManager.Instance.playerName = pname;
        //Debug.Log(MainManager.Instance.playerName);
    }
}
