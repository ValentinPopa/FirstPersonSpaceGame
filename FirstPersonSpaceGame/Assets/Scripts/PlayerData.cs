using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerData
{
    public float[] playerStats; //[0]-health [1]-food [2]-water
    public float[] playerPositionAndRotation; //pos x,y,z rot x,y,z
    public string[] inventoryContent;
    public int questsCompleted;
    //public string[] inventoryContent;

    public PlayerData(float[] _playerStats, float[] _playerPositionAndRotation, string[] _inventoryContent, int _questsCompleted)
    {
        playerPositionAndRotation = _playerPositionAndRotation;
        playerStats = _playerStats;
        inventoryContent = _inventoryContent;
        questsCompleted = _questsCompleted;
    }
}
