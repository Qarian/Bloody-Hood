using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour {

    public LevelScript level;

    GameObject player;
    GameObject spawner;
    GameObject boss;
    GameObject comic;
    [SerializeField]
    GameObject endScreen;
    [SerializeField]
    GameObject canvas;
    BossHp bossHp;
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
        Time.timeScale = 1;
        player = FindObjectOfType<Player>().gameObject;
        spawner = FindObjectOfType<EnemySpawner>().gameObject;
        comic = Comic.singleton.gameObject;

        boss = GameObject.FindGameObjectWithTag("Boss");
        boss.SetActive(false);

        bossHp = FindObjectOfType<BossHp>();
        boss.GetComponent<Boss>().bossHp = bossHp;
        bossHp.gameObject.SetActive(false);


    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown("r")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    #region Boss
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
    #endregion

    public void EndGame(bool win)
    {
        Debug.Log("Boss pokonany");
        GameObject go = Instantiate(endScreen, canvas.transform);
        go.GetComponent<EndScreenScript>().Begin(win);
        Time.timeScale = 0;
    }
}
