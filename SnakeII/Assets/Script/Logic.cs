using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Logic : MonoBehaviour
{
    private int playerScore = 0;
    private int snakeLength = 0;
    public GameObject movement;
    //public UIManager uiManager;
    public SnakeManager snakeManager;

    public int GetPlayerScore() { return playerScore; }
    public void SetPlayerScore(int playerScore) { this.playerScore = playerScore; }
    public int GetSnakeLength() { return snakeLength; }
    public void SetSnakeLength(int snakeLength) {  this.snakeLength = snakeLength;}
    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        snakeLength++;
    }
    public float[] RandomPosition(Bounds bounds)
    {
        float x, y;
        // Nếu không có segment nào, cho phép tạo item bất kỳ
        if (snakeManager.GetSegments() == null)
        {
            x = Random.Range(bounds.min.x, bounds.max.x);
            y = Random.Range(bounds.min.y, bounds.max.y);
        }
        else
        {
            do
            {
                x = Random.Range(bounds.min.x, bounds.max.x);
                y = Random.Range(bounds.min.y, bounds.max.y);
            }
            while (snakeManager.GetSegments().Any(segment => segment.position.x == x && segment.position.y == y));
        }
        return new float[] { x, y };
    }
    public void SuperFoodEffect()
    {
        Debug.Log("superfood effect");
        AddScore(5);
        UIManager.Instance.DisplayScore();
    }
    private IEnumerator SpeedBoostCoroutine()
    {
        float originalSpeed = movement.GetComponent<Movement>().speed;
        movement.GetComponent<Movement>().speed *= 2; // Tăng tốc độ
        yield return new WaitForSeconds(10f); // Tăng tốc trong 10 giây
        movement.GetComponent<Movement>().speed = originalSpeed; // Trả lại tốc độ ban đầu
    }
    public void SpeedBoostEffect()
    {
        Debug.Log("speedboost effect");
        StartCoroutine(SpeedBoostCoroutine());
    }
    public void BlindBoxEffect()
    {
        Debug.Log("blindbox effect");
        float rand = Random.Range(1f, 10f);
        AddScore(((int)rand % 2 == 0) ? 10 : -10);
        UIManager.Instance.DisplayScore();
    }
}
