using System.Collections;
using UnityEngine;

public class PlayerMovement3 : MonoBehaviour {

    public Transform playerPositions;
    [Tooltip("in seconds")]
    public float swapTime = 0.25f;

    float distance;
    Player player;
    Transform[] points;
    int currentPoint;
    int pointCount;

    bool canMove = true;

    void Start()
    {
        GetPoints();
        transform.position = points[currentPoint].position;
        player = gameObject.GetComponent<Player>();
        distance = (points[currentPoint].position - points[currentPoint-1].position).magnitude;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, (distance / swapTime) * Time.deltaTime);
    }

    public void Move(int number)
    {
        if (canMove)
        {
            currentPoint = number;
            StartCoroutine(MoveTime());
        }
        if (currentPoint < 0)
        {
            currentPoint = 0;
        }
        else if (currentPoint >= pointCount)
        {
            currentPoint = pointCount - 1;
        }
    }

    void GetPoints()
    {
        pointCount = playerPositions.childCount;
        points = new Transform[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            points[i] = playerPositions.GetChild(i);
        }
        currentPoint = pointCount / 2;
    }

    IEnumerator MoveTime()
    {
        canMove = false;
        yield return new WaitForSeconds(swapTime);
        canMove = true;
        player.Tap();
    }
}
