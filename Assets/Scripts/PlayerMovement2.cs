using UnityEngine;

public class PlayerMovement2 : MonoBehaviour {

    public Transform playerPositions;
    public RectTransform clockSprite;
    [Tooltip("Margines błędu jaki gracz może popełnić w procentach (w stosunku do całego czasu)")]
    [Range(0f, 100f)]
    public float errorLimit = 100;
    [Tooltip("Czas potrzebny na cały bit (może ulegać zmianie podczas gry)")]
    public float bitTime = 0.8f;


    Player player;
    Transform[] points;
    int currentPoint;
    int pointCount;

    bool done = false;
    float time = 0f;


    void Start()
    {
        GetPoints();
        transform.position = points[currentPoint].position;
        player = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        if (Input.GetKeyDown("a") && !done) Move(-1);
        else if (Input.GetKeyDown("d") && !done) Move(1);
        else if (Input.GetKeyDown(KeyCode.Space) && !done)
        {
            player.Tap();
            if (done) Penalty();
            done = true;
        }
        clockSprite.Rotate(0f, 0f, 360 * Time.deltaTime / bitTime);
        time += Time.deltaTime;
        if (time >= bitTime)
        {
            done = false;
            time -= bitTime;
        }
    }

    bool Move(int number)
    {
        if (done) Penalty();
        done = true;
        currentPoint += number;
        if (currentPoint < 0)
        {
            currentPoint = 0;
            return false;
        }
        else if (currentPoint >= pointCount)
        {
            currentPoint = pointCount - 1;
            return false;
        }
        transform.position = points[currentPoint].position;
        return true;
    }

    void Penalty()
    {
        Debug.Log("Zrobiles 2 akcje naraz - tak nie mozna");
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
