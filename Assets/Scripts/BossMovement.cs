using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BossMovement : MonoBehaviour {

    public Transform bossPositions;
    public float speed = 1f;

    Transform[] points;
    int pointCount;
    int currentPoint;
    int currentDirection = 1;

    Boss boss;

	void Start () {
        GetPoints();
        transform.position = points[currentPoint].position;
        boss = GetComponent<Boss>();
	}
	
	void Update () {
        if (Mathf.Abs(Vector2.Distance(transform.position, points[currentPoint + currentDirection].position)) < 0.1f)
            NextPoint();
        transform.position = Vector2.MoveTowards(transform.position, points[currentPoint + currentDirection].position, speed * Time.deltaTime);
    }

    void NextPoint()
    {
        currentPoint += currentDirection;
        if (currentPoint == 0 || currentPoint == pointCount - 1)
            currentDirection *= -1;
        boss.Attack(currentPoint);
    }

    void GetPoints()
    {
        pointCount = bossPositions.childCount;
        points = new Transform[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            points[i] = bossPositions.GetChild(i);
        }
        currentPoint = pointCount / 2;
    }
}
