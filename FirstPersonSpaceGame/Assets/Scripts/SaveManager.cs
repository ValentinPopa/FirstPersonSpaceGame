using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor.Experimental.RestService;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    string jsonPathProject;
    string jsonPathPersistant;
    string binaryPath;
    string fileName = "SaveGame";

    public bool isSavingToJson;
    public bool isLoading;
    public Canvas loadingScreen;
    private void Start()
    {
        jsonPathProject = Application.dataPath + Path.AltDirectorySeparatorChar;
        jsonPathPersistant = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
        binaryPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar;
    }
    #region General Section
    #region Saving
    public void SaveGame(int slotNumber)
    {
        AllGameData data=new AllGameData();
        data.playerData = GetPlayerData();
        data.enviromentData = GetEnviromentData();
        SavingTypeSwitch(data, slotNumber);
    }

    private EnviromentData GetEnviromentData()
    {
        List<string> itemsPickedUp = PlayerInventory.Instance.itemsPickedUp;
        return new EnviromentData(itemsPickedUp);
    }

    private PlayerData GetPlayerData()
    {
        float[] playerStats = new float[3];
        playerStats[0] = PlayerState.Instance.currentHealth;
        playerStats[1] = PlayerState.Instance.currentFood;
        playerStats[2] = PlayerState.Instance.currentWater;

        float[] playerPosAndRot = new float[6];
        playerPosAndRot[0] = PlayerState.Instance.player.transform.position.x;
        playerPosAndRot[1] = PlayerState.Instance.player.transform.position.y;
        playerPosAndRot[2] = PlayerState.Instance.player.transform.position.z;

        playerPosAndRot[3] = PlayerState.Instance.player.transform.rotation.x;
        playerPosAndRot[4] = PlayerState.Instance.player.transform.rotation.y;
        playerPosAndRot[5] = PlayerState.Instance.player.transform.rotation.z;

        string[] inventory=PlayerInventory.Instance.itemList.ToArray();
        return new PlayerData(playerStats, playerPosAndRot, inventory);
    }
    public void SavingTypeSwitch(AllGameData gameData,int slotNumber)
    {
        if(isSavingToJson)
        {
            SaveGameDataToJsonFile(gameData, slotNumber);
        }
        else
        {
            SaveGameDataToBinaryFile(gameData, slotNumber);
        }
    }
    #endregion
    #region Loading
    public AllGameData LoadingTypeSwitch(int slotNumber)
    {
        if(isSavingToJson)
        {
            AllGameData gameData = LoadGameDataFromJsonFile(slotNumber);
            return gameData;
        }
        else
        {
            AllGameData gameData = LoadGameDataFromBinaryFile(slotNumber);
            return gameData;
        }
    }
    public void LoadGame(int slotNumber)
    {
        SetPlayerData(LoadingTypeSwitch(slotNumber).playerData);

        //enviroment
        SetEnviromentData(LoadingTypeSwitch(slotNumber).enviromentData);
        isLoading = false;
        DisableLoadingScreen();
    }

    private void SetEnviromentData(EnviromentData enviromentData)
    {
        foreach(Transform itemType in EnviromentManager.Instance.allItems.transform)
        {
            foreach(Transform item in itemType.transform)
            {
                if(enviromentData.pickedUpItems.Contains(item.name))
                {
                    Destroy(item.gameObject);
                }
            }
        }
        PlayerInventory.Instance.itemsPickedUp = enviromentData.pickedUpItems;
    }

    private void SetPlayerData(PlayerData playerData)
    {
        PlayerState.Instance.currentHealth = playerData.playerStats[0];
        PlayerState.Instance.currentFood = playerData.playerStats[1];
        PlayerState.Instance.currentWater = playerData.playerStats[2];

        Vector3 loadedPosition;
        loadedPosition.x = playerData.playerPositionAndRotation[0];
        loadedPosition.y = playerData.playerPositionAndRotation[1];
        loadedPosition.z = playerData.playerPositionAndRotation[2];

        PlayerState.Instance.player.transform.position = loadedPosition;

        Vector3 loadedRotation;
        loadedRotation.x = playerData.playerPositionAndRotation[3];
        loadedRotation.y = playerData.playerPositionAndRotation[4];
        loadedRotation.z = playerData.playerPositionAndRotation[5];

        PlayerState.Instance.player.transform.rotation = Quaternion.Euler(loadedRotation);


        //setting the inventory content

        foreach(string item in playerData.inventoryContent)
        {
            PlayerInventory.Instance.AddItemToInventory(item);
        }

    }
    public void StartLoadedGame(int slotNumber)
    {
        ActivateLoadingScreen();
        isLoading = true;
        SceneManager.LoadScene("Planet1");
        StartCoroutine(DelayedLoading(slotNumber));
    }
    private IEnumerator DelayedLoading(int slotNumber)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("Planet1");

        // Wait until the scene finishes loading
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        LoadGame(slotNumber);
        Debug.Log("Game loaded");
    }
    #endregion
    #endregion
    #region To binary section
    public void SaveGameDataToBinaryFile(AllGameData gameData, int slotNumber)
    {
        BinaryFormatter formatter= new BinaryFormatter();
        FileStream stream=new FileStream(binaryPath + fileName + slotNumber + ".bin", FileMode.Create);
        formatter.Serialize(stream, gameData);
        stream.Close();
        print("Data saved to" + binaryPath + fileName + slotNumber + ".bin");
    }
    public AllGameData LoadGameDataFromBinaryFile(int slotNumber)
    {
        if(File.Exists(binaryPath + fileName + slotNumber + ".bin"))
        {
            BinaryFormatter formatter= new BinaryFormatter();
            FileStream stream=new FileStream(binaryPath + fileName + slotNumber + ".bin", FileMode.Open);
            AllGameData data = formatter.Deserialize(stream) as AllGameData;
            stream.Close();
            print("Data loaded from" + binaryPath + fileName + slotNumber + ".bin");
            return data;
        }
        else
        {
            return null;
        }
    }
    #endregion
    #region To json section
    public void SaveGameDataToJsonFile(AllGameData gameData, int slotNumber)
    {
        string json=JsonUtility.ToJson(gameData);
        //saving the data encrypted
        //string encrypted = EncryptionDecryption(json);
        using (StreamWriter writer=new StreamWriter(jsonPathProject+ fileName + slotNumber + ".json"))
        {
            writer.Write(json);//encrypted
            print("Saved Game to json file at:" + jsonPathProject + fileName + slotNumber + ".json");
        };
    }
    public AllGameData LoadGameDataFromJsonFile(int slotNumber)
    {
        using(StreamReader reader=new StreamReader(jsonPathProject + fileName + slotNumber + ".json"))
        {
            string json=reader.ReadToEnd();
            //loading the data while decrypting it
            //string decrypted=EncryptionDecryption(json);
            AllGameData data=JsonUtility.FromJson<AllGameData>(json);//decrypted
            return data;
        };
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

    #region Encryption
    public string EncryptionDecryption(string jsonString)
    {
        string keyword = "1234567"; //used to encrypt and decrypt
        string result = "";
        for(int i=0;i< jsonString.Length;i++) 
        {
            //takes every character from the saved file and make XOR bitwise with the keyword
            result += (char)(jsonString[i] ^ keyword[i % keyword.Length]);
        }
        return result;
    }
    #endregion

    #region Utility
    public bool DoesFileExists(int slotNumber)
    {
        if(isSavingToJson) 
        {
            if(System.IO.File.Exists(jsonPathProject + fileName + slotNumber + ".json"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            if(System.IO.File.Exists(binaryPath + fileName + slotNumber + ".bin"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public bool IsSlotEmpty(int slotNumber)
    {
        if (DoesFileExists(slotNumber))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void DeselectButton()
    {
        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
    }
    #endregion

    #region Loading Screen Section
    public void ActivateLoadingScreen()
    {
        loadingScreen.gameObject.SetActive(true);
        Cursor.visible = false;
    }
    public void DisableLoadingScreen()
    {
        loadingScreen.gameObject.SetActive(false);
        Cursor.visible = true;
    }
    #endregion
}
