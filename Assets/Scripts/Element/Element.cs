using UnityEngine;

public class Element : MonoBehaviour {

    public float destroyTime = 5f;
    public int damage = 1;

    [Space]
    public float timeAfterSpawn = 1f;

    [Space]
    [HideInInspector]
    public bool moving = true;
    public int addExp = 1;
    [Range(1, 3)]
    public int SpawnInWave = 1;
}
