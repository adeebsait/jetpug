using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitButton() {
        Application.Quit();
    }

    public void StartGame() {
        SceneManager.LoadScene("Main");
    }

    public void Menu() {
    SceneManager.LoadScene("StartScreen");
    }

    public void Upgrade() {
    SceneManager.LoadScene("UpgradeStore");
    }

}