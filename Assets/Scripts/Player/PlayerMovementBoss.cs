using UnityEngine;
using System.Collections;

public class PlayerMovementBoss : MonoBehaviour {


    public float attackMoveSpeed = 0.3f;
    public float distanceToBoss = 8f;
    bool isAttack = false;

    bool canSwipe = true;
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 Swapdelta;

    PlayerMovement movement;
    Player player;
    Transform[] attackPoints;

    int swapDistance;

    Vector2 attackPosition;

    bool moving = true;
    
    public void Start()
    {
        movement = transform.parent.GetComponent<PlayerMovement>();
        swapDistance = movement.swapDistance;
        player = transform.parent.GetComponent<Player>();
    }

    void Update () {
        if (moving)
        {
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
                    StartCoroutine(MoveTime(movement.MoveRight()));

                }
                else if (Swapdelta.x < -swapDistance)
                {
                    StartCoroutine(MoveTime(movement.MoveLeft()));
                }
            }

            #region Button
            if (Input.GetKeyDown("space") && !isAttack) StartCoroutine(Tap());
            if (Input.GetKeyDown("a"))
            {
                if (canSwipe)
                {
                    StartCoroutine(MoveTime(movement.MoveLeft()));
                }
            }
            if (Input.GetKeyDown("d"))
            {
                if (canSwipe)
                {
                    StartCoroutine(MoveTime(movement.MoveRight()));
                }
            }
            #endregion
        }
    }

    IEnumerator MoveTime(float time)
    {
        isDrag = false;
        canSwipe = false;
        yield return new WaitForSeconds(time);
        canSwipe = true;
    }

    IEnumerator Tap()
    {
        canSwipe = false;
        isAttack = true;
        movement.PerformAttack();
        yield return new WaitForSeconds(attackMoveSpeed);
        yield return new WaitForSeconds(player.AttackWithoutCooldown());
        movement.ReturnFromAttack();
        yield return new WaitForSeconds(attackMoveSpeed);
        movement.ContinueMoving();
        canSwipe = true;
        isAttack = false;
    }

    public void StopMoving()
    {
        moving = false;
    }
}
