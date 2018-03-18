using UnityEngine;
using UnityEngine.UI;

public class BackgroundMenager : MonoBehaviour
{
    LevelScript level;
    [Tooltip("Background move with that speed")]
    public float speed = 10;
    [Tooltip("Speed of background before a boss")]
    public float speed2 = 20;

    GameMenager gameMenager;

    GameObject[] background;
    Vector2 screen;
    float changePoint;
    float scale;
    float length;
    int lastElement = 1;

    bool bossMode = false;

    void Start()
    {
        gameMenager = GameMenager.singleton;
        level = gameMenager.level;
        InitializingBackground();
    }

    void Update()
    {
        for (int i = 0; i < 2; i++)
        {
            background[i].transform.Translate(new Vector3(0,-speed * Time.deltaTime));
        }
        if (background[lastElement].GetComponent<RectTransform>().localPosition.y < changePoint && !bossMode)
            SwapElements();
        if (background[lastElement].GetComponent<RectTransform>().localPosition.y < 0.1 && bossMode){
            speed = 0;
            background[lastElement].GetComponent<RectTransform>().localPosition = new Vector3(0, 0);
            gameMenager.BossPhase();
            enabled = false;
        }
    }

    void SwapElements()
    {
        lastElement = (lastElement + 1) % 2;
        if (gameMenager.bossmode)
        {
            bossMode = true;
            Vector2 scaleBoss = new Vector2(screen.x / level.bossBackground.texture.width, screen.y / level.bossBackground.texture.height);
            background[lastElement].GetComponent<RectTransform>().sizeDelta = new Vector2(level.bossBackground.texture.width, level.bossBackground.texture.height);
            background[lastElement].GetComponent<RectTransform>().localScale = scaleBoss;
            background[lastElement].GetComponent<RectTransform>().localPosition = new Vector3(0, background[(lastElement+1)%2].GetComponent<RectTransform>().localPosition.y + length/2 + (level.bossBackground.texture.height * scaleBoss.y)/2);
            speed = speed2;
            background[lastElement].GetComponent<Image>().sprite = level.bossBackground;
        }
        else
        {
            background[lastElement].GetComponent<RectTransform>().localPosition = new Vector3(0, background[lastElement].GetComponent<RectTransform>().localPosition.y + 2 * length);
        }
    }

    void InitializingBackground()
    {
        // 9 x 16
        background = new GameObject[2];
        screen = new Vector2(Screen.width, Screen.height);

        for (int i = 0; i < 2; i++)
        {
            GameObject tmp = new GameObject();
            background[i] = tmp;
            background[i].name = "Background";
            RectTransform rt = background[i].AddComponent<RectTransform>();
            background[i].AddComponent<CanvasRenderer>();
            background[i].AddComponent<Image>();
            rt.SetParent(gameObject.GetComponent<RectTransform>());
            rt.sizeDelta = new Vector2(level.normalBackground.texture.width, level.normalBackground.texture.height);
            background[i].GetComponent<Image>().sprite = level.normalBackground;
        }
        FitToScreen();
        changePoint = (length) / 2 - (screen.y / 2);
        background[0].GetComponent<RectTransform>().localPosition = new Vector3(0, changePoint, 0);
        background[1].GetComponent<RectTransform>().localPosition = new Vector3(0, changePoint + length, 0);
    }

    void FitToScreen()
    {
        for (int i = 0; i < 2; i++)
        {
            scale = screen.x / level.normalBackground.texture.width;
            background[i].GetComponent<RectTransform>().localScale = new Vector3(scale, scale);
        }
        length = level.normalBackground.texture.height * scale;
    }
}