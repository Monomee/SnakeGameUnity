using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject guideScreen;

    public Logic logicComponent;
    public Text scoreText;
    public Text lengthText;
    public GameObject gameOverScreen;

    /*
     Phần quản lý UI tại Menu scene 
     */
    public void LoadPlayScene()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1; // Khởi động lại game
    }
    public void LoadGuideScene()
    {
        menuScreen.SetActive(false);
        guideScreen.SetActive(true);
    }
    public void BackToMenu()
    {
        guideScreen.SetActive(false);
        menuScreen.SetActive(true);
    }
    public void QuitGame()
    {
        // Thực hiện khi nhấn nút
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    /*
     Phần quản lý Ui tại Play scene
     */
    public void DisplayScore()
    {
        scoreText.text = logicComponent.GetPlayerScore().ToString();
    }

    public void LoadGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0; // Dừng toàn bộ game
        lengthText.text = logicComponent.GetSnakeLength().ToString();
    }
    public void RestartGame()
    {
        Time.timeScale = 1; // Khởi động lại game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void LoadMenuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
