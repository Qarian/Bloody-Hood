using UnityEngine;
using System.Collections;

public class PlayerMovementBoss : MonoBehaviour {

    public float AttackBossWaitTime = 0.05f;
    public float attackMoveSpeed = 0.3f;
    public float distanceToBoss = 12;
    bool isAttack = false;

    bool canSwipe = true;
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 Swapdelta;

    float distance = 4f;
    Player player;
    Transform[] points;
    Transform[] attackPoints;
    int currentPoint;
    int pointCount;

    Transform playerPositions;
    float swapTime;
    int swapDistance;

    PlayerMovement1 move;
    Vector2 attackPosition;

    public void Begin(PlayerMovement1 move)
    {
        playerPositions = move.playerPositions;
        swapTime = move.swapTime;
        swapDistance = move.swapDistance;
        GetPoints();
        player = gameObject.GetComponent<Player>();
        currentPoint = move.currentPoint;
    }

    void Update () {
        #region StandAlone
        if (Input.GetMouseButtonDown(0) && canSwipe)
        {
            isDrag = true;
            startPoint = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (isDrag) StartCoroutine(Tap());
            isDrag = false;
        }
        #endregion
        #region Mobile
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began && canSwipe)
            {
                isDrag = true;
                startPoint = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
            {
                if (isDrag) Tap();
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
                Move(1);
                StartCoroutine(MoveTime());
                
            }
            else if (Swapdelta.x < -swapDistance)
            {
                Move(-1);
                StartCoroutine(MoveTime());
            }
        }
        if (Input.GetKeyDown("space") && !isAttack) StartCoroutine(Tap());
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
        if (isAttack)
            transform.position = Vector2.MoveTowards(transform.position, attackPosition, (distanceToBoss / attackMoveSpeed) * Time.deltaTime);
        else
            transform.position = Vector2.MoveTowards(transform.position, points[currentPoint].position, (distance / swapTime) * Time.deltaTime);
    }

    bool Move(int number)
    {
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
    }

    IEnumerator MoveTime()
    {
        isDrag = false;
        canSwipe = false;
        yield return new WaitForSeconds(swapTime);
        canSwipe = true;
    }

    IEnumerator Tap()
    {
        canSwipe = false;
        isAttack = true;
        attackPosition = new Vector2(transform.position.x, transform.position.x + distanceToBoss);
        yield return new WaitForSeconds(attackMoveSpeed);
        attackPosition = transform.position;
        StartCoroutine(player.Attack());
        yield return new WaitForSeconds(AttackBossWaitTime);
        attackPosition = new Vector2(transform.position.x, transform.position.x - distanceToBoss);
        yield return new WaitForSeconds(attackMoveSpeed);
        canSwipe = true;
        isAttack = false;
    }
}
