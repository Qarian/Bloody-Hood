using UnityEngine;
using System.Collections;

public class EnemySpawner2 : MonoBehaviour {

    public Transform enemyPositions;
    public GameObject enemyPrefab;
    [Tooltip("number of enemies per second")]
    public float frequency = 1f;

    Transform[] points;
    int pointCount;

    void Start()
    {
        GetPoints();
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            //int rand = Random.Range(0, pointCount);
            //GameObject go = Instantiate(enemyPrefab, points[rand].position, Quaternion.identity);
            //go.GetComponent<Enemy>().is2metod = true;
            yield return new WaitForSeconds(1f / frequency);
        }
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
