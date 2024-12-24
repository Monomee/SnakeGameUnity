using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Logic : MonoBehaviour
{
    private int playerScore = 0;
    private int snakeLength = 0;
    public int scoreToAdd = 1;
    public Text scoreText;
    public Text lengthText;
    public GameObject gameOverScreen;
    public GameObject movement;

    public void addScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        snakeLength++;
        scoreText.text = playerScore.ToString();
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
        Time.timeScale = 0; // Dừng toàn bộ game
        lengthText.text += snakeLength.ToString();
    }
    public void restartGame()
    {
        Time.timeScale = 1; // Khởi động lại game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void menuScene()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void superFoodEffect()
    {
        Debug.Log("superfood effect");
        addScore(5);
    }
    private IEnumerator SpeedBoostCoroutine()
    {
        float originalSpeed = movement.GetComponent<Movement>().speed;
        movement.GetComponent<Movement>().speed *= 2; // Tăng tốc độ
        yield return new WaitForSeconds(10f); // Tăng tốc trong 10 giây
        movement.GetComponent<Movement>().speed = originalSpeed; // Trả lại tốc độ ban đầu
    }
    public void speedBoostEffect()
    {
        Debug.Log("speedboost effect");
        StartCoroutine(SpeedBoostCoroutine());
    }
    public void blindBoxEffect()
    {
        Debug.Log("blindbox effect");
        float rand = Random.Range(1f, 10f);
        addScore(((int)rand % 2 == 0) ? 10 : -10);
    }
}
