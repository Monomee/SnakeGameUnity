using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; 

public class SpecialItem : MonoBehaviour
{
    public GameObject superFood;
    public GameObject speedBoost;
    public GameObject blindBox;
    public BoxCollider2D gridArea;     
    private GameObject currentItem; // Đối tượng item hiện tại
    private bool isActive = false; // Trạng thái item
    public GameObject[] itemPrefabs;
    public GameObject snakeBody;
    private List<Transform> segments;
    private void Start()
    {
        segments = snakeBody.GetComponent<Movement>().GetSegments();
        StartCoroutine(HandleItem());
    }

    private IEnumerator HandleItem()
    {
        while (true) // Lặp vô hạn
        {
            if (!isActive && currentItem == null) // Nếu chưa có item
            {
                float randomDelay = Random.Range(15f, 30f); // Thời gian ngẫu nhiên (từ 15 đến 30 giây)
                yield return new WaitForSeconds(randomDelay); // Chờ

                SpawnRandomPosition(); // Xuất hiện item
                isActive = true; // Đánh dấu là đang hoạt động

                yield return new WaitForSeconds(10f);
                DestroyItem();//xóa item sau 10s không ăn item đó
            }

            yield return null; // Chờ 1 frame
        }
    }

    private void SpawnRandomPosition()
    {
        Bounds bounds = gridArea.bounds;

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
        Debug.Log("Create Item");
        currentItem = Instantiate(randomItem(), new Vector3(Mathf.Round(x), Mathf.Round(y), 0), Quaternion.identity);
    }

    public void OnEaten()
    {
        if (currentItem != null)
        {
            Destroy(currentItem); // Xóa item
            currentItem = null;   // Đặt về null
            isActive = false; // Đánh dấu là không hoạt động
        }
    }

    public GameObject randomItem()
    {
        float randNum = Mathf.Round(Random.Range(1f, 10f));
        int num = (int)randNum % 3;
        if (num == 0)
        {
            return speedBoost;
        }
        else if (num == 1)
        {
            return superFood;
        }
        else
        {
            return blindBox;
        }
    }
    private void DestroyItem()
    {
        Destroy(currentItem);
        currentItem = null;
        isActive = false;
        Debug.Log("Destroyed prefab");
    }
}
