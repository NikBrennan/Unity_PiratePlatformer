using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public GameObject gameOverUI;
    public Text totalScore;
    private float score = CoinPicker.coins;

    private void Start()
    {
        score = score / 2;
        totalScore.text += score.ToString();
    }

    public void Restart()
    {
        CoinPicker.coins = 0;
        SceneManager.LoadScene("LevelOneScene");
    }

    public void MainMenu()
    {
        CoinPicker.coins = 0;
        SceneManager.LoadScene("MenuScene");
    }

    public void QuitGame()
    {
        CoinPicker.coins = 0;
        Debug.Log("Quitting Game...");
        Application.Quit();
    }


}
