using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public LevelScript level;

    GameObject player;
    EnemySpawner spawner;
    GameObject boss;
    GameObject comic;
    AudioSource music;
    BackgroundMenager background;
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    GameObject canvas;
    [HideInInspector]
    public BossHp bossHp;

    GameObject end;

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
        spawner = EnemySpawner.singleton;
        comic = Comic.singleton.gameObject;
        background = BackgroundMenager.singleton;

        boss = level.boss;
        bossHp = FindObjectOfType<BossHp>();
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
        GameObject go;
        if (music == null)
        {
            go = new GameObject("Muzyka");
            music = go.AddComponent<AudioSource>();
        }
        else
        {
            go = music.gameObject;
        }
        
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
            player.GetComponent<PlayerMovement1>().ChangeMovementBoss();
        else if (player.GetComponent<PlayerMovementTutorial>() != null)
            player.GetComponent<PlayerMovementTutorial>().ChangeMovement();
        return 0;
    }
    #endregion

    public void DestroyEffects()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Effect");
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].GetComponent<BloodEffect>().End();
        }
    }

    public void EndGame(bool win)
    {
        end = Instantiate(endScreen, canvas.transform);
        end.GetComponent<EndScreenScript>().Begin(win);
        Time.timeScale = 0;
    }

    public void ContinueGame()
    {
        level = level.nextLevel;

        background.enabled = true;
        background.NewBackground(level);

        Destroy(end);

        player.GetComponent<PlayerMovement1>().ChangeMovementNormal();

        Time.timeScale = 1;
    }

    public void NewLevel()
    {
        spawner.StartSpawning();

        StartMusic();
    }
}
