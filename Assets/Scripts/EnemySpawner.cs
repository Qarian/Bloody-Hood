using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour {

    public bool spawnOnStart = true;

    public Transform enemyPositions;

    Transform[] points;
    int pointCount;

    float timeToSpawn = 0f;
    float timePostSpawn = 1f;

    //From Level object
    GameObject[] enemy;
    bool endless;
    int waveCount = 10;
    SpawnChoice spawnChoice;

    Action[] spawn;// = new Action[2];

    # region Singleton
    public static EnemySpawner singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    void Start ()
    {
        GetPoints();
        SetupIEnumarators();
        if(spawnOnStart)
            StartCoroutine(Spawn());
        #region Read Level
        LevelScript level = GameManager.singleton.level;
        enemy = level.enemies;
        endless = level.endless;
        waveCount = level.waveCount;
        spawnChoice = level.spawnChoice;
        #endregion
    }
	
    void SetupIEnumarators()
    {
        spawn = new Action[Enum.GetValues(typeof(SpawnChoice)).Length];
        spawn[0] = SpawnNormal;
        spawn[1] = SpawnRandom;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        if (!endless)
        {
            int currentWaveCount = 0;
            while (currentWaveCount < waveCount)
            {
                yield return new WaitForSeconds(timeToSpawn);
                spawn[(int)spawnChoice]();
                currentWaveCount++;
                yield return new WaitForSeconds(timePostSpawn);
            }
        }
        else
        {
            while (true)
            {
                yield return new WaitForSeconds(timeToSpawn);
                spawn[(int)spawnChoice]();
                yield return new WaitForSeconds(timePostSpawn);
            }
        }
        yield return new WaitForSeconds(2f);
        GameManager.singleton.BossBattleReady();
    }

    void SpawnNormal()
    {
        int rand = UnityEngine.Random.Range(0, pointCount);
        SpawnAtPosition(rand, enemy[0]);
    }

    void SpawnRandom()
    {
        int randPos = UnityEngine.Random.Range(0, pointCount);
        int randE = UnityEngine.Random.Range(0, enemy.Length);
        SpawnAtPosition(randPos, enemy[randE]);
    }


    public void StopSpawn()
    {
        Debug.Log("Coroutine stopped");
        StopAllCoroutines();
    }

    void SpawnAtPosition(int x, GameObject prefab, Vector2 offset=new Vector2())
    {
        Instantiate(prefab, points[x].position, Quaternion.identity);
    }

    void GetPoints()
    {
        pointCount = enemyPositions.childCount;
        points = new Transform[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            points[i] = enemyPositions.GetChild(i);
        }
    }
}

public enum SpawnChoice
{
    OnlyOne = 0,
    Random = 1,
    Priority = 2,
}
