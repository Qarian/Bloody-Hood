using UnityEngine;

public class Comic : MonoBehaviour {

    public float comicElementTime = 2.5f;

    public GameObject background; // background to show
    public GameObject button;
    GameObject container; // Empty container for images

    GameObject[] images;
    GameManager gm;

    int currentImage;
    bool clear = false;
    int moment; //  0 - start, 1- boss, 2- end

    GameObject[] gos; // thing with tag "Bar" to hide

    bool active = false;
    float time;

    #region Singleton
    public static Comic singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    void Start()
    {
        gm = GameManager.singleton;
        background.SetActive(false);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            NextPage();
        if (active && !clear)
        {
            if (time < Time.time)
                NextPage();
        }
    }


    public void ShowComic(int num)
    {
        active = true;
        moment = num;
        gos = GameObject.FindGameObjectsWithTag("Bar");
        foreach (var go in gos)
        {
            go.SetActive(false);
        }
        currentImage = 0;
        background.SetActive(true);
        button.SetActive(true);

        switch (num)
        {
            case 0:
                images = gm.level.comics.start;
                break;
            case 1:
                images = gm.level.comics.boss;
                break;
            case 2:
                images = gm.level.comics.end;
                break;
        }

        container = SetContainer();
        clear = Instantiate(images[currentImage], container.transform).GetComponent<ComicElement>().lastOnPage;
        time = Time.time + comicElementTime;
    }

    public void NextPage()
    {
        if(currentImage == images.Length - 1)
        {
            switch (moment)
            {
                case 0:
                    ContinueToGame();
                    break;
                case 1:
                    ContinueToBoss();
                    break;
                case 2:
                    break;
                default:
                    break;
            }
            return;
        }
        if (clear)
        {
            Destroy(container);
            container = SetContainer();
        }
        currentImage++;
        clear = Instantiate(images[currentImage], container.transform).GetComponent<ComicElement>().lastOnPage;
        time = Time.time + comicElementTime;
    }

    GameObject SetContainer()
    {
        GameObject ret = new GameObject("Container");
        ret.transform.SetParent(transform);
        RectTransform rect = ret.AddComponent<RectTransform>();
        rect.anchorMax = Vector2.one;
        rect.anchorMin = Vector2.zero;
        rect.localScale = Vector3.one;
        rect.sizeDelta = Vector3.zero;

        button.transform.SetParent(null);
        button.transform.SetParent(transform);
        return ret;
    }

    void CloseComic()
    {
        Destroy(container);
        background.SetActive(false);
        button.SetActive(false);
        clear = false;
        active = false;
    }

    void ContinueToBoss()
    {
        CloseComic();
        foreach (var go in gos)
        {
            go.SetActive(true);
        }

        gm.BossPhase();
    }

    void ContinueToGame()
    {
        CloseComic();
        foreach (var go in gos)
        {
            go.SetActive(true);
        }

        gm.StartGame();
    }
}
