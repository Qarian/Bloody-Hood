using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {

    public float endScreenWaitTime = 2f;

    [SerializeField]
    ListOfLevels list;
    [HideInInspector]
    public LevelScript level;

    GameObject player;
    EnemySpawner spawner;
    GameObject boss;
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
    [HideInInspector]
    public GameObject maxHp;

    GameObject endGO;
    GameObject pauseGO;

    public bool pause = false;
    [HideInInspector]
    public int money;
    public bool endless;

    # region Singleton
    public static GameManager singleton;

    void Awake()
    {
        level = list.list[PlayerPrefs.GetInt("Level")];
        singleton = this;
    }
    #endregion

    void Start()
    {
        Time.timeScale = 1;
        player = FindObjectOfType<Player>().gameObject;
        spawner = EnemySpawner.singleton;
        background = BackgroundMenager.singleton;
        background.gameObject.SetActive(false);
        pause = true;

        Comic.singleton.ShowComic(0);
    }

    public void StartGame()
    {
        pause = false;
        
        money = 0;
        boss = level.boss;
        bossHp = FindObjectOfType<BossHp>();
        bossHp.gameObject.SetActive(false);
        background.gameObject.SetActive(true);
        StartMusic();
        spawner.StartSpawning();
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
        Comic.singleton.ShowComic(1);
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
        if (endless)
        {
            DestroyProjectiles();
            if (win)
            {
                FindObjectOfType<PlayerMovementBoss>().StopMoving();
                ContinueGame();
            }
            else
            {
                pause = true;
                endGO = Instantiate(endScreen, canvas.transform);
                endGO.GetComponent<EndScreenScript>().Begin(win);
                Time.timeScale = 0;
            }
            return;
        }

        pause = true;
        DestroyProjectiles();
        if (win)
        {
             StartCoroutine(FindObjectOfType<PlayerMovementBoss>().EndAttack());
            //PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
        }
        StartCoroutine(ShowEndscript(win));
    }
    
    IEnumerator ShowEndscript(bool win)
    {
        if (win)
            yield return new WaitForSeconds(endScreenWaitTime);
        endGO = Instantiate(endScreen, canvas.transform);
        endGO.GetComponent<EndScreenScript>().Begin(win);
        if(!win)
            Time.timeScale = 0;
    }

    
    public void ContinueGame()
    {
        pause = false;
        Time.timeScale = 1;
        if (endless)
        {
            if(Endless.singleton.NextLevel())
                level = level.nextLevel;
        }
        else
        {
            level = level.nextLevel;
        }  

        background.enabled = true;
        background.NewBackground(level);

        Destroy(endGO);
    }

    public void NewLevel()
    {
        spawner.StartSpawning();
        player.GetComponent<PlayerMovement>().ChangeMovementNormal();
        StartMusic();
    }


    public void DestroyEffects()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Effect");
        for (int i = 0; i < gos.Length; i++)
        {
            gos[i].GetComponent<BloodEffect>().End(false);
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

    public void ChangeScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
