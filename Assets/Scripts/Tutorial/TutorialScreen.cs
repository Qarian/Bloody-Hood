using UnityEngine;

public class TutorialScreen : MonoBehaviour {

    [Tooltip("in pixels")]
    public int swapDistance = 35;

    bool canSwipe = true;
    bool isDrag = false;
    Vector2 startPoint;
    Vector2 Swapdelta;

    TutorialManager tm;

    private void Start()
    {
        tm = FindObjectOfType<TutorialManager>();
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
            if (isDrag)
                tm.Run(3);
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
                if (isDrag)
                    tm.Run(3);
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
                    tm.Run(2);
            }
            else if (Swapdelta.x < -swapDistance)
            {
                if (canSwipe)
                    tm.Run(1);
            }
        }

        #region Buttons
        if (Input.GetKeyDown(KeyCode.Space)) tm.Run(3); ;
        if (Input.GetKeyDown("a"))
        {
            if (canSwipe)
                tm.Run(1);
        }
        if (Input.GetKeyDown("d"))
        {
            if (canSwipe)
                tm.Run(2);
        }
        #endregion
    }
}
