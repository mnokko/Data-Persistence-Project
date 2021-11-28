using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    public Text BestScoreText;
    
    private bool m_Started = false;
    private int m_Points; //T‰m‰nhetkisen pelin pisteet.
    private string currentName; //T‰m‰nhetkisen pelaajan nimi.

    private bool m_GameOver = false;

    public string bestScorePlayer; // Muuttuja, johon haetaan parhaan tuloksen pelanneen nimi tiedostosta.
    public int bestScore; // Muuttuja, johon haetaan paras tulos tiedostosta.



    // Start is called before the first frame update
    void Start()
    {
        //Ladataan hiscore ja parhaan pelaajan nimi tiedostosta.
        DataManager.Instance.LoadScore();

        //Haetaan hiscore ja parhaan pelaajan nimi muuttujiin.
        bestScore = DataManager.Instance.hiScore;
        bestScorePlayer = DataManager.Instance.bestPlayer;

        //Haetaan nykyisen pelaajan nimi muuttujaan.
        currentName = DataManager.Instance.playerName;
        //Debug.Log("Bestscore-kohta: " + bestScorePlayer + " " + bestScore);

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }

        ShowBestScore();
    }

    private void Update()
    {
        if (!m_Started)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
        }

        //Jos nykyisen pelaajan pistem‰‰r‰ on suurempi kuin hiscore, tallennetaan uusi hiscore ja pelaajan nimi.
        if (m_Points > bestScore)
        {
            bestScore = m_Points;
            bestScorePlayer = currentName;

            SaveHighScoreAndName();

            ShowBestScore();
        }
    }

    //Pistelasku
    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }
    //N‰ytet‰‰n hiscore ruudun yl‰laidassa.
    void ShowBestScore()
    {
        BestScoreText.text = "Best Score: " + bestScorePlayer + ": " + bestScore;
    }

    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);
        
    }

    //Tallennetaan uusi hiscore ja pelaajan nimi tiedostoon.
    void SaveHighScoreAndName()
    {
        DataManager.Instance.SaveScore(bestScorePlayer, bestScore);
        Debug.Log("paras tulos " + bestScore);
        Debug.Log("pelaaja " + bestScorePlayer);
    }

}
