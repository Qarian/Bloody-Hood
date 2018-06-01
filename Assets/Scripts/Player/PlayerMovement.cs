using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float height = -6.5f;
    [HideInInspector]
    public Transform[] positions = new Transform[3];
    int currentPoint = 1;
    float distance;

    [Tooltip("in pixels")]
    public int swapDistance = 35;
    public float swapTime = 0.2f;
    float changingSpeed;

    [Space]
    public float attackTime;
    public float attackHeight = 5f;
    bool swaping = true;
    float attackSpeed;
    Vector3 attackPosition;

    [SerializeField]
    GameObject movementObject;

    void Start () {
        GeneratePoints();
        transform.position = positions[currentPoint].position;
    }
	
	void Update () {
        if (swaping)
            transform.position = Vector2.MoveTowards(transform.position, positions[currentPoint].position, changingSpeed * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, attackPosition, attackSpeed * Time.deltaTime);
    }

    public float MoveLeft()
    {
        if (currentPoint == 0)
            return 0;
        currentPoint--;
        return swapTime;
    }

    public float MoveRight()
    {
        if (currentPoint == 2)
            return 0;
        currentPoint++;
        return swapTime;
    }

    public void ChangeMovementNormal()
    {
        Destroy(movementObject);
        movementObject = new GameObject("Movement Normal");
        movementObject.transform.parent = transform;
        movementObject.AddComponent<PlayerMovement1>();
    }

    public void ChangeMovementBoss()
    {
        Destroy(movementObject);
        movementObject = new GameObject("Movement Boss");
        movementObject.transform.parent = transform;
        movementObject.AddComponent<PlayerMovementBoss>();
    }

    #region Attack
    public void PerformAttack()
    {
        swaping = false;
        attackPosition = positions[currentPoint].position + new Vector3(0, attackHeight - height, 0);
        attackSpeed = (attackHeight - height) / attackTime;
    }

    public void ReturnFromAttack()
    {
        attackPosition = positions[currentPoint].position;
    }

    public void ContinueMoving()
    {
        swaping = true;
    }
    #endregion

    void GeneratePoints()
    {
        height = transform.position.y;

        distance = Screen.width / (float)Screen.height * 5.1f;
        changingSpeed = distance / swapTime;

        Transform positionsHolder = new GameObject("Player Positions").transform;
        positionsHolder.transform.position = new Vector3(0, height, -0.01f);

        for (int i = 0; i < 3; i++)
        {
            positions[i] = new GameObject("Position " + i).transform;
            positions[i].position = new Vector3(distance * (i - 1), height, -0.01f);
            positions[i].parent = positionsHolder;
        }
    }

}
