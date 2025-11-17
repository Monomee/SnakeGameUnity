using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    public GameObject snakeHead;
    private GameObject headRight;
    private GameObject headLeft;
    private GameObject headUp;
    private GameObject headDown;

    public SnakeManager snakeManager;
    public GameObject logicManagement;
    //public UIManager uiPlay; 
    public AudioController audioController;

    private Vector2 moveDirection = Vector2.zero; 
    public float speed; // Tốc độ di chuyển
    private const float ORI_SPEED = 3f; //Tốc độ mặc định 

    public ParticleSystem boomEffect;
    // Start is called before the first frame update
    void Start()
    {
        //lấy vị trí đầu rắn
        headRight = snakeHead.transform.Find("HeadRight").gameObject;
        headLeft = snakeHead.transform.Find("HeadLeft").gameObject;
        headUp = snakeHead.transform.Find("HeadUp").gameObject;
        headDown = snakeHead.transform.Find("HeadDown").gameObject;

        this.transform.position = new Vector2(-6, 0); //vị trí ban đầu
 
        snakeManager._segments.Add(this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        headDir(); //vị trí hướng đầu rắn
        moveDir(); //hướng đi 
        
        for (int i = snakeManager._segments.Count - 1; i > 0; i--)
        {
            Vector2 targetPosition = snakeManager._segments[i - 1].position;
            StartCoroutine(MoveSegment(snakeManager._segments[i], targetPosition, 0.14f / (speed/ORI_SPEED))); // Delay, khi thân rắn đủ xa mới dịch chuyển thân tới
        }
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    void headDir()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && (moveDirection != Vector2.left))
        {
            headRight.SetActive(true);
            headDown.SetActive(false);
            headLeft.SetActive(false);
            headUp.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (moveDirection != Vector2.right))
        {
            headRight.SetActive(false);
            headDown.SetActive(false);
            headLeft.SetActive(true);
            headUp.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && (moveDirection != Vector2.down))
        {
            headRight.SetActive(false);
            headDown.SetActive(false);
            headLeft.SetActive(false);
            headUp.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (moveDirection != Vector2.up))
        {
            headRight.SetActive(false);
            headDown.SetActive(true);
            headLeft.SetActive(false);
            headUp.SetActive(false);
        }
    }

    void moveDir()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && ( moveDirection != Vector2.left ))
        {
            // Di chuyển rắn sang phải
            moveDirection = Vector2.right ;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && (moveDirection != Vector2.right))
        {
            moveDirection = Vector2.left ;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && (moveDirection != Vector2.down))
        {
            moveDirection = Vector2.up ;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && (moveDirection != Vector2.up))
        {
            moveDirection = Vector2.down ;
        }
    }

    private IEnumerator MoveSegment(Transform segment, Vector2 targetPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        segment.position = targetPosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Food"))
        {
            audioController.eatingSound.Play();
            snakeManager.Grow();
        }
        else if (other.CompareTag("Wall") || other.CompareTag("Body"))
        {
            audioController.deadSound.Play();
            boomEffect.Play();
            UIManager.Instance.LoadGameOverScreen();
        } else if(other.CompareTag("SuperFood"))
        {
            audioController.eatingSound.Play();
            snakeManager.Grow();
            logicManagement.GetComponent<Logic>().SuperFoodEffect();//+5d
            logicManagement.GetComponent<SpecialItem>().OnEaten();
        } else if( other.CompareTag("BlindBox"))
        {
            audioController.eatingSound.Play();
            logicManagement.GetComponent<Logic>().BlindBoxEffect();//ngẫu nhiên +/- 10d
            logicManagement.GetComponent<SpecialItem>().OnEaten();
        }
        else if(other.CompareTag("SpeedBoost"))
        {
            audioController.eatingSound.Play();
            logicManagement.GetComponent<Logic>().SpeedBoostEffect();//x2 tốc độ
            logicManagement.GetComponent<SpecialItem>().OnEaten();
        }

    }
}
