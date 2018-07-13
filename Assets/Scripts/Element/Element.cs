using UnityEngine;

public class Element : MonoBehaviour {

    public float damage = 1;

    [Space]
    public float timeAfterSpawn = 1f;

    [Space]
    [HideInInspector]
    public bool moving = true;
    public int addExp = 1;
    [Range(1, 3)]
    public int SpawnInWave = 1;

    public bool alive = false;

    private void OnBecameVisible()
    {
        alive = true;
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
