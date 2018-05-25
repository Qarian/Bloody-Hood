using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Player))]
public class PlayerMovement1: MonoBehaviour {

    public Transform playerPositions;
    [Tooltip("in seconds")]
    public float swapTime = 0.25f;
    [Tooltip("in pixels")]
    public int swapDistance = 100;

    [HideInInspector]
    public float distance = 4f;
    Player player;
    Transform[] points;
    [HideInInspector]
    public int currentPoint;
    int pointCount;

    bool canSwipe = true;
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 Swapdelta;


	void Start ()
    {
        if(points == null)
        {
            GetPoints();
            transform.position = points[currentPoint].position;
            player = gameObject.GetComponent<Player>();
            distance = (points[currentPoint].position - points[currentPoint - 1].position).magnitude;
        }
    }

    void Update()
    {
        #region StandAlone
        if (Input.GetMouseButtonDown(0) && canSwipe)
        {
            isDrag = true;
            startPoint = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isDrag) player.Tap();
            isDrag = false;
        }
        #endregion
        #region Mobile
        if(Input.touches.Length > 0)
        {
            if(Input.touches[0].phase == TouchPhase.Began && canSwipe)
            {
                isDrag = true;
                startPoint = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
            {
                if (isDrag) player.Tap();
                isDrag = false;
            }
        }
        #endregion

        if (isDrag)
        {
            if (Input.touches.Length > 0)
            {
                Swapdelta = Input.touches[0].position - startPoint;
            }
            else if (Input.GetMouseButton(0))
            {
                Swapdelta = (Vector2)Input.mousePosition - startPoint;
            }

            if (Swapdelta.x > swapDistance)
            {
                if (canSwipe)
                {
                    Move(1);
                    StartCoroutine(MoveTime());
                }
            }
            else if (Swapdelta.x < -swapDistance)
            {
                if (canSwipe)
                {
                    Move(-1);
                    StartCoroutine(MoveTime());
                }
            }
        }

        #region Buttons
        if (Input.GetKeyDown(KeyCode.Space)) player.Tap();
        if (Input.GetKeyDown("a"))
        {
            if (canSwipe)
            {
                Move(-1);
                StartCoroutine(MoveTime());
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (canSwipe)
            {
                Move(1);
                StartCoroutine(MoveTime());
            }
        }
        #endregion

        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, (distance/swapTime) * Time.deltaTime);
    }

    bool Move(int number)
    {
        currentPoint += number;
        if (currentPoint < 0)
        {
            currentPoint = 0;
            return false;
        }
        else if(currentPoint >= pointCount)
        {
            currentPoint = pointCount - 1;
            return false;
        }
        return true;
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
        isDrag = false;
        canSwipe = false;
        yield return new WaitForSeconds(swapTime);
        canSwipe = true;
    }

    public void ChangeMovementBoss()
    {
        GetComponent<PlayerMovementBoss>().enabled = true;
        GetComponent<PlayerMovementBoss>().Begin(this);
        enabled = false;
    }

    public void ChangeMovementNormal()
    {
        GetComponent<PlayerMovementBoss>().enabled = false;
        enabled = true;
    }
}
