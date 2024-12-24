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

    public GameObject logicManagement;

    private Vector2 moveDirection = Vector2.zero; 
    public float speed; // Tốc độ di chuyển
    private const float ORI_SPEED = 3f; //Tốc độ mặc định 

    private List<Transform> _segments;
    public Transform body;

    public AudioSource eatingSound;
    public AudioSource deadSound;
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
 
        _segments = new List<Transform>();
        _segments.Add(this.transform);

    }

    // Update is called once per frame
    void Update()
    {
        headDir(); //vị trí hướng đầu rắn
        moveDir(); //hướng đi 

        for (int i = _segments.Count - 1; i > 0; i--)
        {
            Vector2 targetPosition = _segments[i - 1].position;
            StartCoroutine(MoveSegment(_segments[i], targetPosition, 0.14f / (speed/ORI_SPEED))); // Delay, khi thân rắn đủ xa mới dịch chuyển thân tới
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
    public List<Transform> GetSegments()
    {
        return _segments;
    }
    private void Grow()
    {
        Transform segment = Instantiate(this.body);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
    }
    private IEnumerator MoveSegment(Transform segment, Vector2 targetPosition, float delay)
    {
        yield return new WaitForSeconds(delay);
        segment.position = targetPosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            eatingSound.Play();
            Grow();
        } else if (other.tag == "Wall" )
        {
            deadSound.Play();
            boomEffect.Play();
            logicManagement.GetComponent<Logic>().gameOver();
        } else if(other.tag == "SuperFood")
        {
            eatingSound.Play();
            Grow();
            logicManagement.GetComponent<Logic>().superFoodEffect();//+5d
            logicManagement.GetComponent<SpecialItem>().OnEaten();
        } else if( other.tag == "BlindBox")
        {
            eatingSound.Play();
            logicManagement.GetComponent<Logic>().blindBoxEffect();//ngẫu nhiên +/- 10d
            logicManagement.GetComponent<SpecialItem>().OnEaten();
        }
        else if(other.tag == "SpeedBoost")
        {
            eatingSound.Play();
            logicManagement.GetComponent<Logic>().speedBoostEffect();//x2 tốc độ
            logicManagement.GetComponent<SpecialItem>().OnEaten();
        }

    }

    

}
