using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public LevelScript level;

    GameObject player;
    GameObject spawner;
    GameObject boss;
    GameObject comic;
    AudioSource music;
    BackgroundMenager background;
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    GameObject canvas;
    BossHp bossHp;
    public Transform bossPositions;

    # region Singleton
    public static GameManager singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    void Start()
    {
        Time.timeScale = 1;
        player = FindObjectOfType<Player>().gameObject;
        spawner = FindObjectOfType<EnemySpawner>().gameObject;
        comic = Comic.singleton.gameObject;
        background = BackgroundMenager.singleton;

        boss = level.boss;
        bossHp = FindObjectOfType<BossHp>();
        boss.GetComponent<Boss>().bossHp = bossHp;
        bossHp.gameObject.SetActive(false);

        StartMusic();
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown("r")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    void StartMusic()
    {
        GameObject go = new GameObject("Muzyka");
        music = go.AddComponent<AudioSource>();
        music.clip = level.music;
        music.Play();

        if (level.changeMusic)
        {
            music.loop = false;
            go.AddComponent<MusicScript>().sound2 = level.music2;
        }
        else
        {
            music.loop = true;
        }
    }

    #region Boss
    public void BossBattleReady()
    {
        spawner.GetComponent<EnemySpawner>().StopSpawn();
        DestroyEffects();
        comic.GetComponent<Comic>().ShowComic(2);
        background.BossBackgroundReady();
    }

    public void BossPhase()
    {
        //muzyczka
        if (player.GetComponent<PlayerMovement1>() != null)
            player.GetComponent<PlayerMovement1>().enabled = false;
        else if (player.GetComponent<PlayerMovementTutorial>() != null)
            player.GetComponent<PlayerMovementTutorial>().enabled = false;
        background.BossBackgroundMove();
    }

    public int BossBattle()
    {
        Instantiate(boss, boss.GetComponent<BossMovement>().startPoint.position, Quaternion.identity, null);
        if (player.GetComponent<PlayerMovement1>() != null)
            player.GetComponent<PlayerMovement1>().ChangeMovement();
        else if (player.GetComponent<PlayerMovementTutorial>() != null)
            player.GetComponent<PlayerMovementTutorial>().ChangeMovement();
        return 0;
    }
    #endregion

    public void EndGame(bool win)
    {
        GameObject go = Instantiate(endScreen, canvas.transform);
        go.GetComponent<EndScreenScript>().Begin(win);
        Time.timeScale = 0;
    }

    public void DestroyEffects()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Effect");
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].GetComponent<BloodEffect>().End();
        }
    }
}
