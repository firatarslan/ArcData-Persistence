using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine.EventSystems;
using System.Linq;
using System.Xml.Linq;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public PlayerList myPlayerList = new PlayerList();
    private int maxPlayerCount = 5;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public PlayerList LoadJsonData()
    {
        string filePath = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            myPlayerList = JsonUtility.FromJson<PlayerList>(jsonData);

            //sort list
            myPlayerList.players.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));

            //remove other players
            while (myPlayerList.players.Count > maxPlayerCount)
            {
                myPlayerList.players.RemoveAt(maxPlayerCount);
            }
            return myPlayerList;
        }
        else
        {
            Debug.LogError("not file have : " + filePath);
            return null;
        }
    }
    public void SaveJsonData(List<PlayerData> newPlayers)
    {
        string filePath = Application.persistentDataPath + "/savefile.json";

        //TODO:make method sort and remove maxPlayerCount
        newPlayers.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));
        while (newPlayers.Count > maxPlayerCount)
        {
            newPlayers.RemoveAt(maxPlayerCount);
        }

        string jsonData = JsonHandler.ToJson<PlayerData>(newPlayers.ToArray());
        File.WriteAllText(filePath, jsonData);

    }
}



