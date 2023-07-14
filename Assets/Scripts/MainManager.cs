using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text GameOverText;
    public Text bestScoreText;
    PlayerData bestPlayer;


    private bool m_Started = false;
    private int m_Points;
    private bool m_GameOver = false; 


    //Get inputs from user
    [SerializeField] InputField juniorPlayerName;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject NewScorePanel;

    void Start()
    {
        BestPlayerText();
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 3, 3, 5, 5 };
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

        void BestPlayerText()
        {
            bestPlayer = ScoreManager.Instance.myPlayerList.players.OrderByDescending(player => player.playerScore).FirstOrDefault();
            if (bestPlayer != null)
            {
                string firstFiveChar = bestPlayer.playerName.Substring(0, System.Math.Min(bestPlayer.playerName.Length, 5)) + "...";
                bestScoreText.text = firstFiveChar + " : " + bestPlayer.playerScore.ToString();
            }
            else
            {
                bestScoreText.text = "no players";
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                GameOverPanel.SetActive(false);
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
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
    }

    public void RestartGame()
    {
        m_Started = true;
        GameOverPanel.SetActive(false);
        NewScorePanel.SetActive(false);
        PlayerData newPlayer = new PlayerData(juniorPlayerName.text, m_Points);
        FoundHighScore(newPlayer);
        SceneManager.LoadScene(0);
    }
    void FoundHighScore(PlayerData newPlayer)
    {
        ScoreManager.Instance.myPlayerList.players.Add(newPlayer);
        List<PlayerData> yeniListem = ScoreManager.Instance.myPlayerList.players;
        ScoreManager.Instance.SaveJsonData(yeniListem);
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        int minSkor = ScoreManager.Instance.myPlayerList.players.Min(player => player.playerScore);
        if (m_Points > minSkor)
        {
            NewScorePanel.SetActive(true);
        }
        else
        {
            GameOverPanel.SetActive(true);
        }
    }

   
}
