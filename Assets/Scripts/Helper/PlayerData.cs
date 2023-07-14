using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// player data info
[Serializable]
public class PlayerData
{
    public string playerName;
    public int playerScore;
    public PlayerData(string _playerName, int _playerScore)
    {
        playerName = _playerName;
        playerScore = _playerScore;
    }
}
[Serializable]
public class PlayerList
{
    public List<PlayerData> players = new List<PlayerData>();
}


