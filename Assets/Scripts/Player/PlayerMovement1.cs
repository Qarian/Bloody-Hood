using UnityEngine;
using System.Collections;

public class PlayerMovement1: MonoBehaviour {

    PlayerMovement movement;
    int swapDistance = 100;

    [HideInInspector]
    public float distance = 4f;
    Player player;
    [HideInInspector]
    public int currentPoint;
    int pointCount;

    bool canSwipe = true;
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 Swapdelta;


	void Start ()
    {
        movement = transform.parent.GetComponent<PlayerMovement>();
        swapDistance = movement.swapDistance;
        player = transform.parent.GetComponent<Player>();
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
            if (isDrag)
                StartCoroutine(CooldownTime(player.Attack()));
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
                if (isDrag)
                    StartCoroutine(CooldownTime(player.Attack()));
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
                    StartCoroutine(CooldownTime(movement.MoveRight()));
                }
            }
            else if (Swapdelta.x < -swapDistance)
            {
                if (canSwipe)
                {
                    StartCoroutine(CooldownTime(movement.MoveLeft()));
                }
            }
        }

        #region Buttons
        if (Input.GetKeyDown(KeyCode.Space)) StartCoroutine(CooldownTime(player.Attack()));
        if (Input.GetKeyDown("a"))
        {
            if (canSwipe)
            {
                StartCoroutine(CooldownTime(movement.MoveLeft()));
            }
        }
        if (Input.GetKeyDown("d"))
        {
            if (canSwipe)
            {
                StartCoroutine(CooldownTime(movement.MoveRight()));
            }
        }
        #endregion
    }
    
    IEnumerator CooldownTime(float time)
    {
        isDrag = false;
        canSwipe = false;
        yield return new WaitForSeconds(time);
        canSwipe = true;
    }
}
