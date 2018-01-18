using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour {

    static GameObject player;
    static GameObject spawner;
    static GameObject boss;
    public bool bossmode;

    public static GameMenager singleton;

    void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
        spawner = FindObjectOfType<EnemySpawner>().gameObject;
        boss = GameObject.FindGameObjectWithTag("Boss");
        boss.SetActive(false);
        singleton = this;
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
        if(SceneManager.GetActiveScene().name == "Scene")
        {
            BossBattle();
        }
    }

    public void BossBattle()
    {
        boss.SetActive(true);
        player.GetComponent<PlayerMovement1>().ChangeMovement();
    }
}
