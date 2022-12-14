using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick brickPrefab;
    public int lineCount = 6;
    public Rigidbody Ball;

    public Text scoreText;
    public Text bestScoreText;
    public GameObject gameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    //Instanciate the Bricks in linees and adds a point value to them
    void Start()
    {
        bestScoreText.text = $"Best Score : {DataHandler.Instance.playerPointHighscore} {DataHandler.Instance.playerNameHighscore}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < lineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(brickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        //Starts the Game with space if it didnt start yet
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
        //Reloads the game with space when gameover
        else if (m_GameOver)
        {
            if (m_Points >= DataHandler.Instance.playerPointHighscore)
            {
                DataHandler.Instance.playerPointHighscore = m_Points;
                DataHandler.Instance.playerNameHighscore = DataHandler.Instance.activePlayerName;
            }

            bestScoreText.text = $"Best Score : {DataHandler.Instance.playerPointHighscore} {DataHandler.Instance.playerNameHighscore}";
            //Save the score to Jason
            DataHandler.Instance.SaveScore();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.M))
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    //Adds Points and displays them
    void AddPoint(int point)
    {
        m_Points += point;
        scoreText.text = $"Score : {m_Points}";
    }
    //Displays gameover text
    public void GameOver()
    {
        m_GameOver = true;
        gameOverText.SetActive(true);
    }
}
