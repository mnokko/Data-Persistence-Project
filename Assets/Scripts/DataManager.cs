using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance; //datan säilyttämiseen scenejen välillä.
    public string playerName; //muuttuja nykyisen pelaajan nimen säilyttämiseen
    public string bestPlayer; //muuttuja parhaan pelaajan nimen säilyttämiseen
    public int hiScore;



    //datan säilyttämiseen scenejen välillä.
    private void Awake()
    {
 
        //Only use single instance of Instance variable
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        
    }

   //Luokka tallennettavalle tiedolle
   [System.Serializable]
    class SaveData
    {
        public string playerName;
        public int bestScore;
    }
    //Tietojen tallennus tiedostoon, kun uusi hiscore syntyy.
    public void SaveScore(string playerName, int hiScore)
    {
        SaveData data = new SaveData();
        data.playerName = playerName;
        data.bestScore = hiScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    //Tietojen lataus tiedostosta.
    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestPlayer = data.playerName;
            hiScore = data.bestScore;
        }
    }
}
