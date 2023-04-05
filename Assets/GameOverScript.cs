using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public Text Score;
    private float score = CoinPicker.coins;

    private void Start()
    {
        Score.text += score.ToString();
    }

    public void Restart()
    {
        SceneManager.LoadScene("LevelOneScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}
