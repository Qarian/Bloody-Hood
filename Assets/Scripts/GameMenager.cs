using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour {

    static GameObject player;
    static GameObject spawner;
    static GameObject boss;
    static GameObject comic;
    [HideInInspector]
    public bool bossmode;

    # region Singleton
    public static GameMenager singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        spawner = FindObjectOfType<EnemySpawner>().gameObject;
        comic = Comic.singleton.gameObject;

        boss = GameObject.FindGameObjectWithTag("Boss");
        boss.SetActive(false);

        BossHp bossHp = FindObjectOfType<BossHp>();
        boss.GetComponent<Boss>().bossHp = bossHp;
        bossHp.gameObject.SetActive(false);
    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown("r")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void BossBattleReady()
    {
        bossmode = true;
        spawner.GetComponent<EnemySpawner>().StopSpawn();
    }

    public void BossPhase()
    {
        //muzyczka
        //Time.timeScale = 0;
        comic.GetComponent<Comic>().ShowComic(2);
        player.GetComponent<PlayerMovement1>().enabled = false;
    }

    public int BossBattle()
    {
        boss.SetActive(true);
        player.GetComponent<PlayerMovement1>().ChangeMovement();
        return 0;
    }
}
