using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Thêm namespace này để dùng LINQ

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Logic logic;
    public GameObject snake;
    private Movement snakeBody;
    private List<Transform> segments;

    private void Start()
    {
        randomPos();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
        snakeBody = snake.GetComponent<Movement>();
        segments = snakeBody.GetSegments();
    }
    private void randomPos()
    {
        Bounds bounds = this.gridArea.bounds;

        float x, y;
        // Nếu không có segment nào, cho phép tạo item bất kỳ
        if (segments == null)
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
            while (segments.Any(segment => segment.position.x == x && segment.position.y == y));
        }
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            randomPos();
            logic.addScore(1);
        }
    }
}
