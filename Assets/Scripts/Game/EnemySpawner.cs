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
        waveCount = level.waveCount;
        spawnChoice = level.spawnChoice;
        levelEnemies = level.levelEnemies;
        enemies = level.enemies;
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
        currentWaveCount = 0;
        while (currentWaveCount < waveCount)
        {
            currentWaveCount++;
            yield return new WaitForSeconds(spawn[(int)spawnChoice]());
        }
        yield return new WaitForSeconds(3f);
        GameManager.singleton.BossBattleReady();
    }
    
    #region Spawn choice
    float SpawnNormal()
    {
        SetWave(enemies[0]);
        return enemies[0].GetComponent<Enemy>().timeAfterSpawn;
    }

    float SpawnRandom()
    {
        int randE = UnityEngine.Random.Range(0, enemies.Length);
        SetWave(enemies[randE]);
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
    public void SpawnWaves(GameObject go, int waves, float interval = 0)
    {
        if(interval <= 0)
        {
            StartCoroutine(SpawnWavesSingle(go, go.GetComponent<Element>().timeAfterSpawn, waves));
            return;
        }
        StartCoroutine(SpawnWavesSingle(go, interval, waves));
    }

    IEnumerator SpawnWavesSingle(GameObject go, float interval, int waves)
    {
        for (int i = 0; i < waves; i++)
        {
            SpawnAtPosition(UnityEngine.Random.Range(0, 3), go);
            yield return new WaitForSeconds(interval);
        }
    }
    #endregion


    void SetWave(GameObject enemy)
    {
        GameObject[] gos = new GameObject[3];
        int leftPlaces = 3;
        if (enemy.GetComponent<Enemy>() != null)
        {
            for (int i = 0; i < enemy.GetComponent<Enemy>().SpawnInWave; i++)
            {
                int rand = UnityEngine.Random.Range(0, leftPlaces);
                int j = 0;
                while (gos[j] != null || rand != 0)
                {
                    if (gos[j] == null)
                        rand--;
                    j++;
                }
                gos[j] = enemy;
                leftPlaces--;
            }
        }

        SpawnWave(gos);
    }

    void SpawnWave(GameObject[] gos)
    {
        for (int i = 0; i < gos.Length; i++)
        {
            if (gos[i] != null)
                SpawnAtPosition(i, gos[i]);
        }
    }

    void SpawnAtPosition(int x, GameObject prefab)
    {
        if (GameManager.singleton.endless)
            Endless.singleton.CalculateEnemy(Instantiate(prefab, points[x].position, Quaternion.identity));
        else
            Instantiate(prefab, points[x].position, Quaternion.identity);
    }


    public void StopSpawn()
    {
        Debug.Log("Coroutine stopped");
        StopAllCoroutines();
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
