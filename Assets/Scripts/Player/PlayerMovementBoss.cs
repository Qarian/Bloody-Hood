using UnityEngine;
using System.Collections;

public class PlayerMovementBoss : MonoBehaviour {


    float attackMoveTime = 0.3f;

    int movLevel = 2; //2-can move, 1-can attack, 0 - can't do anything

    //bool canAttack = true;

    //bool canSwipe = true;
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 Swapdelta;

    bool isAttack = false;
    bool combo = false;
    float timer; 
    float nextAttackTime; // Time to tap again or combo ends
    float timeToNextAttack; // Time needed to tap again
    public float comboTime = 3.0f; // Time for whole combo
    public float comboAttackTime = 0.4f; // Time to next attack, depend on weapon, ToDo - make method that takes varaible from weapon
    public float comboWaitTime = 1f;

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

        attackMoveTime = movement.attackTime;
    }

    void Update () {
        if (moving)
        {
            //#if UNITY_STANDALONE
            if (Input.GetMouseButtonDown(0))
            {
                isDrag = true;
                startPoint = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                if (isDrag && movLevel > 0)
                    Tap();
                isDrag = false;
            }
            //#endif

            //#if UNITY_ANDROID
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    isDrag = true;
                    startPoint = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Canceled || Input.touches[0].phase == TouchPhase.Ended)
                {
                    if (isDrag && movLevel > 0)
                        Tap();
                    isDrag = false;
                }
            }
            //#endif

            if (isDrag && movLevel == 2)
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
            if (Input.GetKeyDown("space") && movLevel > 0)
                Tap();

            if (Input.GetKeyDown("a"))
            {
                if (movLevel == 2)
                {
                    StartCoroutine(MoveTime(movement.MoveLeft()));
                }
            }
            if (Input.GetKeyDown("d"))
            {
                if (movLevel == 2)
                {
                    StartCoroutine(MoveTime(movement.MoveRight()));
                }
            }
            #endregion
        }

        if (combo)
        {
            timer += Time.deltaTime;
            if (timer > comboTime || timer > nextAttackTime)
                EndCombo();
        }
    }

    IEnumerator MoveTime(float time)
    {
        isDrag = false;
        movLevel = 0;
        yield return new WaitForSeconds(time);
        movLevel = 2;
    }

    void Tap()
    {
        if (!isAttack)
        {
            StartCoroutine(Attack());
        }
        else
        {
            if (!combo)
            {
                movement.moving = false;
                combo = true;
                timer = 0;
                nextAttackTime = comboWaitTime;
                StartCoroutine(player.ComboAttack(comboAttackTime));
                timeToNextAttack = comboAttackTime;
            }
            else
            {
                if(timer < comboTime && timer > timeToNextAttack)
                {
                    nextAttackTime = timer + comboWaitTime;
                    StartCoroutine(player.ComboAttack(comboAttackTime));
                    timeToNextAttack = timer + comboAttackTime;
                    if(timer + comboAttackTime > comboTime)
                    {
                        timer = comboTime - comboAttackTime;
                    }
                }
            }
        }
    }

    void EndCombo()
    {
        isAttack = false;
        combo = false;
        StartCoroutine(EndAttack());
    }

    IEnumerator Attack()
    {
        movLevel = 1;
        isAttack = true;
        movement.PerformAttack();
        yield return new WaitForSeconds(attackMoveTime);
        if (!combo)
        {
            yield return new WaitForSeconds(player.AttackWithoutCooldown());
            StartCoroutine(EndAttack());
        }
    }

    IEnumerator EndAttack()
    {
        movLevel = 0;
        movement.moving = true;
        movement.ReturnFromAttack();
        yield return new WaitForSeconds(attackMoveTime);
        movement.ContinueMoving();
        isAttack = false;
        movLevel = 2;
    }

    public void StopMoving()
    {
        moving = false;
    }
}
