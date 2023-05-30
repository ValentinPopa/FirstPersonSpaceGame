using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button LoadGameBTN;

    public void NewGame()
    {
        SceneManager.LoadScene("Planet1");
    }
    public void ExitGame()
    {
        Debug.Log("Quiting Game");
        Application.Quit();
    }
}
