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
    public Logic logicManager;
    private void Start()
    {
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
        float[] pos = logicManager.RandomPosition(gridArea.bounds);
        
        Debug.Log("Create Item");
        currentItem = Instantiate(RandomItem(), new Vector3(Mathf.Round(pos[0]), Mathf.Round(pos[1]), 0), Quaternion.identity);
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

    public GameObject RandomItem()
    {
        int randNum = Random.Range(0, itemPrefabs.Count());
        return itemPrefabs[randNum];
    }
    private void DestroyItem()
    {
        Destroy(currentItem);
        currentItem = null;
        isActive = false;
        Debug.Log("Destroyed prefab");
    }
}
