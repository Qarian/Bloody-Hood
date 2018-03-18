using UnityEngine;
using UnityEngine.UI;

public class Comic : MonoBehaviour {

    //public Sprite[] comicStart;
    //public Sprite[] comicBoss;
    //public Sprite[] comicEnd;

    LevelScript level;

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
        gm = GameMenager.singleton;
        #region Generate 'comics'
        level = gm.level;
        comics = new Sprite[3][];
        comics[0] = new Sprite[level.comicStart.Length];
        for (int i = 0; i < level.comicStart.Length; i++)
        {
            comics[0][i] = level.comicStart[i];
        }
        comics[1] = new Sprite[level.comicBoss.Length];
        for (int i = 0; i < level.comicBoss.Length; i++)
        {
            comics[1][i] = level.comicBoss[i];
        }
        comics[2] = new Sprite[level.comicEnd.Length];
        for (int i = 0; i < level.comicEnd.Length; i++)
        {
            comics[3][i] = level.comicEnd[i];
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
            but.onClick.AddListener(() => { cp.Clicked(); Debug.Log("klik"); });
            paper[i].SetActive(false);
        }
        #endregion
    }

    public void ShowComic(int num)
    {
        num--;
        page = 0;
        pages = comics[num].Length-1;
        paper[0].SetActive(true);
        paper[0].GetComponent<Image>().sprite = comics[num][0];
        moment = num;
    }

    public void NextPage()
    {
        paper[page % 2].SetActive(false);
        page++;
        if (page > pages)
        {
            gm.BossBattle();
            return;
        }
        paper[page % 2].SetActive(true);
        paper[page % 2].GetComponent<Image>().sprite = comics[moment][page];
    }
}
