using UnityEngine;
using System.Collections;
using System;

public class EnemySpawner : MonoBehaviour {

    public bool spawnOnStart = true;

    public Transform enemyPositions;

    Transform[] points;
    int pointCount;

    //From Level object
    GameObject[] enemies;
    bool endless;
    int waveCount = 10;
    SpawnChoice spawnChoice;
    LevelEnemies levelEnemies;

    Func<float>[] spawn;
    int currentWaveCount;

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
        if (spawnOnStart)
            StartSpawning();
    }

    public void StartSpawning()
    {
        #region Read Level
        LevelScript level = GameManager.singleton.level;
        enemies = level.enemies;
        endless = level.endless;
        waveCount = level.waveCount;
        spawnChoice = level.spawnChoice;
        levelEnemies = level.levelEnemies;
        #endregion
        StartCoroutine(Spawn());
    }

    void SetupIEnumarators()
    {
        spawn = new Func<float>[Enum.GetValues(typeof(SpawnChoice)).Length];
        spawn[0] = SpawnNormal;
        spawn[1] = SpawnRandom;
        //spawn[2] = ;
        spawn[3] = SpawnFromFile;
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(1f);
        if (!endless)
        {
            currentWaveCount = 0;
            while (currentWaveCount < waveCount)
            {
                currentWaveCount++;
                yield return new WaitForSeconds(spawn[(int)spawnChoice]());
            }
        }
        else
        {
            while (true)
            {
                yield return new WaitForSeconds(spawn[(int)spawnChoice]());
            }
        }
        yield return new WaitForSeconds(2f);
        GameManager.singleton.BossBattleReady();
    }
    
    #region Spawn choice
    float SpawnNormal()
    {
        int rand = UnityEngine.Random.Range(0, pointCount);
        SpawnAtPosition(rand, enemies[0]);
        return enemies[0].GetComponent<Enemy>().timeAfterSpawn;
    }

    float SpawnRandom()
    {
        int randPos = UnityEngine.Random.Range(0, pointCount);
        int randE = UnityEngine.Random.Range(0, enemies.Length);
        SpawnAtPosition(randPos, enemies[randE]);
        return enemies[randE].GetComponent<Enemy>().timeAfterSpawn;
    }

    float SpawnFromFile()
    {
        int pos = levelEnemies.waves[currentWaveCount].row;
        int enemy = levelEnemies.waves[currentWaveCount].enemyId;
        SpawnAtPosition(pos, enemies[enemy]);
        return levelEnemies.waves[currentWaveCount].time;
    }
    #endregion

    #region Spawn Waves
    public void SpawnWaves(GameObject go, int waves, float interval = -1)
    {
        if(interval < 0)
        {
            StartCoroutine(SpawnWave(go, go.GetComponent<Enemy>().timeAfterSpawn, waves));
            return;
        }
        StartCoroutine(SpawnWave(go, interval, waves));
    }

    IEnumerator SpawnWave(GameObject go, float interval, int waves)
    {
        for (int i = 0; i < waves; i++)
        {
            SpawnAtPosition(UnityEngine.Random.Range(0, 3), go);
            yield return new WaitForSeconds(interval);
        }
    }
    #endregion

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
    Random,
    Priority,
    LoadFromFile
}
