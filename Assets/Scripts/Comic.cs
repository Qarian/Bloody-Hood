using UnityEngine;
using UnityEngine.UI;

public class Comic : MonoBehaviour {

    [SerializeField]
    GameObject comicPage;

    public Sprite[] comicStart;
    public Sprite[] comicBoss;
    public Sprite[] comicEnd;

    GameMenager gm;

    Sprite[][] comics;
    GameObject[] paper;
    int page; //current
    int pages; //total
    int moment;

    #region Singleton
    public static Comic singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    void Start()
    {
        #region Generate 'comics'
        comics = new Sprite[3][];
        comics[0] = new Sprite[comicStart.Length];
        for (int i = 0; i < comicStart.Length; i++)
        {
            comics[0][i] = comicStart[i];
        }
        comics[1] = new Sprite[comicBoss.Length];
        for (int i = 0; i < comicBoss.Length; i++)
        {
            comics[1][i] = comicBoss[i];
        }
        comics[2] = new Sprite[comicEnd.Length];
        for (int i = 0; i < comicEnd.Length; i++)
        {
            comics[3][i] = comicEnd[i];
        }

        paper = new GameObject[2];
        #endregion
        #region Generate 'paper'
        paper = new GameObject[2];
        for(int i = 0; i < 2; i++)
        {
            GameObject tmp = new GameObject();
            paper[i] = tmp;
            paper[i].name = "Paper" + i.ToString();
            RectTransform rt = paper[i].AddComponent<RectTransform>();
            paper[i].AddComponent<CanvasRenderer>();
            paper[i].AddComponent<Image>();
            rt.SetParent(gameObject.GetComponent<RectTransform>());

            rt.sizeDelta = new Vector2(comics[0][0].texture.width, comics[0][0].texture.height);
            rt.localScale = new Vector2(Screen.width/rt.sizeDelta.x, Screen.height/rt.sizeDelta.y);
            rt.anchorMin = new Vector2(0, 0);
            rt.anchorMax = new Vector2(0, 0);
            rt.localPosition = new Vector2(0, 0);

            ComicPaper cp = paper[i].AddComponent<ComicPaper>();
            Button but = paper[i].AddComponent<Button>();
            but.transition = Selectable.Transition.None;
            but.onClick.AddListener(cp.Clicked);
            paper[i].SetActive(false);
        }
        #endregion
        gm = FindObjectOfType<GameMenager>();
    }

    void Update()
    {
        Debug.Log(gm);
    }

    public void ShowComic(int num)
    {
        num--;
        page = 1;
        pages = comics[num].Length;
        paper[1].SetActive(true);
        paper[1].GetComponent<Image>().sprite = comics[num][0];
        moment = num;
    }

    public void NextPage()
    {
        page++;
        if (page > pages)
        {
            paper[pages-1].SetActive(false);
            gm.BossBattle();
            return;
        }
        paper[page % 2].GetComponent<Image>().sprite = comics[moment][page-1];
    }
}
