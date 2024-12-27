using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeManager : MonoBehaviour
{
    private bool isFirstBody = true;

    public List<Transform> _segments;
    public Transform body;

    // Start is called before the first frame update
    void Awake()
    {
        _segments = new List<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public List<Transform> GetSegments()
    {
        return _segments;
    }
    public void Grow()
    {
        Transform segment = Instantiate(this.body);
        segment.position = _segments[_segments.Count - 1].position;
        if (isFirstBody)
        {
            segment.GetComponent<BoxCollider2D>().enabled = false;
            isFirstBody = false;
        }
        _segments.Add(segment);
    }
}
