using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Transform scoreContainer;
    [SerializeField] GameObject scoreRowTextPrefab;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void EndGame()
    {
        Debug.Log("end game");
        Application.Quit();
    }
    private void Start()
    {
        GetPlayerList();
    }
    public void GetPlayerList()
    {
        PlayerList myPlayerList = ScoreManager.Instance.LoadJsonData();  
        foreach (var item in myPlayerList.players)
        {
            GameObject ScoreRowObj = Instantiate(scoreRowTextPrefab, scoreContainer);
            TextMeshProUGUI text = ScoreRowObj.GetComponent<TextMeshProUGUI>();
            text.text = item.playerName + " ..... " + item.playerScore;
        }
    }
}
