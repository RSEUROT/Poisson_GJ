using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void OnPlayButtonPressed()
    {
        SceneManager.LoadScene("MainLevelScene");
    }
    public void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
