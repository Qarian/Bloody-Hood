using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour {

    static GameObject player;
    static GameObject spawner;
    static GameObject boss;
    public static bool bossmode;

    void Start()
    {
        player = FindObjectOfType<Player>().gameObject;
        spawner = FindObjectOfType<EnemySpawner>().gameObject;
        boss = GameObject.FindGameObjectWithTag("Boss");
        boss.SetActive(false);
    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown("r")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public static void BossBattle()
    {
        bossmode = true;
        player.GetComponent<PlayerMovementBoss>().enabled = true;
        player.GetComponent<PlayerMovement1>().enabled = false;
        spawner.GetComponent<EnemySpawner>().enabled = false;
        boss.SetActive(true);
    }
}
