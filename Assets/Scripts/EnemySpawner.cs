using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public Transform enemyPositions;
    public GameObject enemyPrefab;
    public float speed = 10f;
    [Tooltip("number of enemies per second")]
    public float frequency = 1f;
    [Tooltip("How many waves before boss")]
    public int waveCount = 5;

    Transform[] points;
    int pointCount;

    void Start ()
    {
        GetPoints();
        StartCoroutine(Spawn());
	}
	
    public IEnumerator Spawn()
    {
        int currentWaveCount = 0;
        while (currentWaveCount < waveCount)
        {
            int rand = Random.Range(0, pointCount);
            SpawnAtPosition(rand);
            yield return new WaitForSeconds(1f / frequency);
            currentWaveCount++;
        }
        GameMenager.singleton.BossBattleReady();
    }

    public void StopSpawn()
    {
        Debug.Log("Coroutine stopped");
        StopAllCoroutines();
    }

    void SpawnAtPosition(int x, Vector2 offset=new Vector2())
    {
        GameObject go = Instantiate(enemyPrefab, points[x].position, Quaternion.identity);
        go.GetComponent<Enemy>().speed = -speed;
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
