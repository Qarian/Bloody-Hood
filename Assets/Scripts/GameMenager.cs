using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour {

    static GameObject player;
    static GameObject spawner;
    static GameObject boss;
    public bool bossmode;

    void Awake()
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

    public void BossBattle()
    {
        if (bossmode)
            return;
        StartCoroutine(BossBattleBeggining(2));
    }

    IEnumerator BossBattleBeggining(float time)
    {
        bossmode = true;
        player.GetComponent<PlayerMovement1>().ChangeMovement();
        spawner.GetComponent<EnemySpawner>().StopSpawn();
        yield return new WaitForSeconds(time);
        boss.SetActive(true);
    }
}
