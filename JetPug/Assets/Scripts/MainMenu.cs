using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject AchievementsPanel;
    public GameObject Star1;
    public GameObject Star2;
    public GameObject Star3;

    public void ExitButton()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Menu()
    {
        SceneManager.LoadScene("StartScreen");
    }

    public void Upgrade()
    {
        SceneManager.LoadScene("UpgradeStore");
    }

    public void Achievements()
    {
        AchievementsPanel.SetActive(true);
        if (PlayerPrefs.GetInt("SpeedLevel")==3&&PlayerPrefs.GetInt("MagnetLevel")==3&&PlayerPrefs.GetInt("BubbleLevel")==3)
        {
            Star1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("Highscore") >= 150)
        {
            Star2.SetActive(true);
        }
        if (PlayerPrefs.GetString("Star3") == "true")
        {
            Star3.SetActive(true);
        }
    }
    public void AchievementsClose()
    {
        AchievementsPanel.SetActive(false);
    }
}