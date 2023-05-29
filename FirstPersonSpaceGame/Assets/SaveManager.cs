using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Experimental.RestService;
using UnityEditor.Rendering;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    public bool isSavingToJson;
    #region General Section
    public void SaveGame()
    {
        AllGameData data=new AllGameData();
        data.playeData = GetPlayerData();
        SaveAllGameData(data);
    }
    private PlayerData GetPlayerData()
    {
        float[] playerStats = new float[3];
        playerStats[0] = PlayerState.Instance.currentHealth;
        playerStats[1] = PlayerState.Instance.currentFood;
        playerStats[2] = PlayerState.Instance.currentWater;

        float[] playerPosAndRot = new float[6];
        playerPosAndRot[0] = PlayerMotor.Instance.transform.position.x;
        playerPosAndRot[1] = PlayerMotor.Instance.transform.position.y;
        playerPosAndRot[2] = PlayerMotor.Instance.transform.position.z;

        playerPosAndRot[3] = PlayerMotor.Instance.transform.rotation.x;
        playerPosAndRot[4] = PlayerMotor.Instance.transform.rotation.y;
        playerPosAndRot[5] = PlayerMotor.Instance.transform.rotation.z;

        return new PlayerData(playerStats, playerPosAndRot);
    }
    public void SaveAllGameData(AllGameData gameData)
    {
        if(isSavingToJson)
        {
            //SaveGameDataToJsonFile(gameData);
        }
        else
        {
            SaveGameDataToBinaryFile(gameData);
        }
    }
    #endregion
    #region To binary section
    public void SaveGameDataToBinaryFile(AllGameData gameData)
    {
        BinaryFormatter formatter= new BinaryFormatter();
        string path = Application.persistentDataPath + "/save_game.bin";
        FileStream stream=new FileStream(path,FileMode.Create);
        formatter.Serialize(stream, gameData);
        stream.Close();
        print("Data saved to" + Application.persistentDataPath + "/save_game.bin");
    }
    public AllGameData LoadGameDataFromBinaryFile()
    {
        string path = Application.persistentDataPath + "/save_game.bin";
        if(File.Exists(path))
        {
            BinaryFormatter formatter= new BinaryFormatter();
            FileStream stream=new FileStream(path,FileMode.Open);
            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();
            return data;
        }
        else
        {
            return null;
        }
    }
    #endregion
    #region Settings Section
    #region Volume Settings
    [System.Serializable]
    public class VolumeSettings
    {
        public float music;
        public float effects;
        public float master;
    }
    public void SaveVolumeSettings(float _music, float _effects, float _master)
    {
       VolumeSettings volumeSettings = new VolumeSettings()
        {
            music = _music,
            effects = _effects,
            master = _master
        };
        //save an entire class as an json string
        PlayerPrefs.SetString("Volume", JsonUtility.ToJson(volumeSettings));
        PlayerPrefs.Save();

        Debug.Log("Saved to Player Prefs");
    }
    public VolumeSettings LoadVolumeSettings()
    {
        return JsonUtility.FromJson<VolumeSettings>(PlayerPrefs.GetString("Volume"));
    }
    #endregion
    #endregion
}
