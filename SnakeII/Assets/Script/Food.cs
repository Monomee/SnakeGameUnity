using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Thêm namespace này để dùng LINQ

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;
    public Logic logic;
    public UIManager uiPlay;
    public Logic logicManager;
    private void Start()
    {
        RandomPos();
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
    }
    private void RandomPos()
    {
        float[] pos = logicManager.RandomPosition(this.gridArea.bounds);
        this.transform.position = new Vector3(Mathf.Round(pos[0]), Mathf.Round(pos[1]), 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RandomPos();
            logic.AddScore(1);
            uiPlay.DisplayScore();
        }
    }
}
