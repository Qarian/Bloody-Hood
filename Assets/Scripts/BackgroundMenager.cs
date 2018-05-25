using UnityEngine;
using UnityEngine.UI;

public class BackgroundMenager : MonoBehaviour
{
    LevelScript level;
    [HideInInspector]
    public float speed = 10;
    [HideInInspector]
    public float speed2 = 20;

    GameManager gameManager;

    GameObject[] background;
    Vector2 screen;
    float scale;
    float length;
    int lastElement = 1;

    bool bossReady = false;
    bool bossMode = false;

    bool newBackground1 = false;
    bool newBackground2 = false;

    #region Singleton
    public static BackgroundMenager singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    void Start()
    {
        gameManager = GameManager.singleton;
        level = gameManager.level;
        speed = level.speed;
        speed2 = level.speed2;
        
        InitializingBackground();
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            background[i].transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (background[lastElement].GetComponent<RectTransform>().anchoredPosition.y < 0 && !bossMode)
            SwapElements(background[lastElement].GetComponent<RectTransform>().anchoredPosition.y);
        if (background[lastElement].GetComponent<RectTransform>().anchoredPosition.y < 1.5f && bossMode){
            speed = 0;
            background[lastElement].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0.01f);
            gameManager.BossBattle();
            enabled = false;
        }
    }

    void SwapElements(float offset)
    {
        lastElement = (lastElement + 1) % 2;
        if (newBackground2)
        {
            background[lastElement].GetComponent<Image>().sprite = level.normalBackground;
            background[lastElement].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, length + offset);
            newBackground2 = false;
            gameManager.NewLevel();
        }
        else if (newBackground1)
        {
            background[lastElement].GetComponent<Image>().sprite = level.normalBackground;
            background[lastElement].GetComponent<RectTransform>().sizeDelta = new Vector2(level.normalBackground.texture.width * scale, level.normalBackground.texture.height * scale);
            background[lastElement].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, length + offset);
            speed = level.speed;
            speed2 = level.speed2;
            newBackground1 = false;
            newBackground2 = true;
        }
        else if (bossReady)
        {
            bossMode = true;
            Vector2 scaleBoss = new Vector2(screen.x / level.bossBackground.texture.width, screen.y / level.bossBackground.texture.height);
            RectTransform rt = background[lastElement].GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(level.bossBackground.texture.width * scaleBoss.x, level.bossBackground.texture.height * scaleBoss.y);
            rt.localScale = new Vector2(1, 1);
            //rt.anchoredPosition = new Vector3(0, offset + rt.sizeDelta.y);
            rt.anchoredPosition = new Vector3(0, offset + length);
            background[lastElement].GetComponent<Image>().sprite = level.bossBackground;
        }
        else
        {
            background[lastElement].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, length + offset);
        }
    }

    void InitializingBackground()
    {
        background = new GameObject[2];
        screen = new Vector2(Screen.width, Screen.height);

        scale = screen.x / level.normalBackground.texture.width;
        for (int i = 0; i < 2; i++)
        {
            GameObject tmp = new GameObject();
            background[i] = tmp;
            background[i].name = "Background" + i.ToString();
            RectTransform rt = background[i].AddComponent<RectTransform>();
            background[i].AddComponent<CanvasRenderer>();
            background[i].AddComponent<Image>();
            rt.SetParent(gameObject.GetComponent<RectTransform>());
            Vector2 vector = new Vector2(0.5f, 0);
            rt.anchorMax = vector;
            rt.anchorMin = vector;
            rt.pivot = vector;
            rt.sizeDelta = new Vector2(level.normalBackground.texture.width * scale, level.normalBackground.texture.height * scale);
            rt.localScale = new Vector3(1, 1);
            background[i].GetComponent<Image>().sprite = level.normalBackground;
        }
        length = background[0].GetComponent<RectTransform>().sizeDelta.y;

        background[0].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        background[1].GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        background[0].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0.01f);
        background[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(0, length);
    }

    public void BossBackgroundReady()
    {
        speed = 0;
        bossReady = true;
    }

    public void BossBackgroundMove()
    {
        speed = speed2;
    }

    public void NewBackground(LevelScript lev)
    {
        bossReady = false;
        bossMode = false;
        newBackground1 = true;
        speed = speed2;
        LastBackground();

        level = lev;
    }

    void LastBackground()
    {
        lastElement = (lastElement + 1) % 2;
        background[lastElement].GetComponent<RectTransform>().anchoredPosition = new Vector3(0, background[(lastElement+1)%2].GetComponent<RectTransform>().sizeDelta.y);
        background[lastElement].GetComponent<Image>().sprite = level.lastBackground;
    }
}