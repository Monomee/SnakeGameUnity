using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuLogic : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject guideScreen;
    public AudioSource menuTheme;
    public AudioSource clickSound;


    public void playScene()
    {
        SceneManager.LoadScene("SampleScene");
        Time.timeScale = 1; // Khởi động lại game
    }
    public void guide()
    {
        menuScreen.SetActive(false);
        guideScreen.SetActive(true);
    }
    public void backToMenu()
    {
        guideScreen.SetActive(false);
        menuScreen.SetActive(true);
    }
    public void Quit()
    {
        // Thực hiện khi nhấn nút
        Debug.Log("Game is quitting...");
        Application.Quit();

    }
}
