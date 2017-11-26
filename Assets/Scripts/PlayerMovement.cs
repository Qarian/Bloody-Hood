using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour {

    public Transform playerPositions;
    public float swapSpeed = 5f;
    [Tooltip("in pixels")]
    public int swapDistance = 100;

    Player player;
    Transform[] points;
    int currentPoint;
    int pointCount;

    bool isDrag = false;
    Vector2 startPoint;
    Vector2 Swapdelta;


	void Start ()
    {
        GetPoints();
        transform.position = points[currentPoint].position;
        player = gameObject.GetComponent<Player>();
	}

    void Update()
    {
        #region StandAlone
        if (Input.GetMouseButtonDown(0))
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
            if(Input.touches[0].phase == TouchPhase.Began)
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

            if (Swapdelta.y > swapDistance)
            {
                Move(-1);
                isDrag = false;
            }
            else if (Swapdelta.y < -swapDistance)
            {
                Move(1);
                isDrag = false;
            }
        }

        if (Input.GetKeyDown("w")) Move(-1);
        if (Input.GetKeyDown("s")) Move(1);
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, swapSpeed * Time.deltaTime);
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
        //transform.position = points[currentPoint].position;
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
}
