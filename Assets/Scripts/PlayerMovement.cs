using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Transform playerPositions;

    Transform[] points;
    int currentPoint;
    int pointCount;

	void Start ()
    {
        GetPoints();
        transform.position = points[currentPoint].position;
	}

    void Update()
    {
        if (Input.GetKeyDown("w")) Move(-1);
        if (Input.GetKeyDown("s")) Move(1);
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
        transform.position = points[currentPoint].position;
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
