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
    GameObject pauseScreen;
    [SerializeField]
    GameObject canvas;
    [HideInInspector]
    public BossHp bossHp;
    public GameObject maxHp;

    GameObject endGO;
    GameObject pauseGO;

    bool pause = false;

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
        if (Input.GetKeyDown(KeyCode.Escape)) Pause();
        if (Input.GetKeyDown("r")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    void StartMusic()
    {
        GameObject go;
        if (music == null)
        {
            go = new GameObject("Muzyka");
            go.tag = "Audio";
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
        music.volume = PlayerPrefs.GetFloat("Music");
    }

    #region Boss
    public void BossBattleReady()
    {
        //spawner.GetComponent<EnemySpawner>().StopSpawn();
        DestroyEffects();
        comic.GetComponent<Comic>().ShowComic(2);
        background.BossBackgroundReady();
        maxHp.SetActive(false);
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
        player.GetComponent<PlayerMovement>().ChangeMovementBoss();
        return 0;
    }
    #endregion
    

    public void EndGame(bool win)
    {
        pause = true;
        endGO = Instantiate(endScreen, canvas.transform);
        endGO.GetComponent<EndScreenScript>().Begin(win);
        DestroyProjectiles();
        if (win)
            FindObjectOfType<PlayerMovementBoss>().StopMoving();
        else
            Time.timeScale = 0;
    }

    
    public void ContinueGame()
    {
        pause = false;
        Time.timeScale = 1;

        level = level.nextLevel;

        background.enabled = true;
        background.NewBackground(level);

        Destroy(endGO);

        player.GetComponent<PlayerMovement>().ChangeMovementNormal();
    }

    public void NewLevel()
    {
        spawner.StartSpawning();

        StartMusic();
    }


    public void DestroyEffects()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Effect");
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].GetComponent<BloodEffect>().End();
        }
    }

    void DestroyProjectiles()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (var go in gos)
        {
            Destroy(go);
        }
    }


    void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Pause();
        }
    }

    public void Pause()
    {
        if (pause)
            return;
        pause = true;
        pauseGO = Instantiate(pauseScreen, canvas.transform);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        pause = false;
        Destroy(pauseGO);
        Time.timeScale = 1;
    }

}
