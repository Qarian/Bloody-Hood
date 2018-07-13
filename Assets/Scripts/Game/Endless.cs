using UnityEngine;

public class Endless : MonoBehaviour {

    [SerializeField]
    float speedMultiplier = 1.25f;
    [SerializeField]
    float spawnTimeMultiplier = 0.8f;
    [SerializeField]
    float damageMultiplier = 1.25f;
    [SerializeField]
    float damageMultiplierBoss = 1.25f;
    [SerializeField]
    float healthMultiplierBoss = 1.25f;

    float speedM = 1f;
    float spawnTimeM = 1f;
    float damageM = 1f;
    float damageMB = 1f;
    float healthMB = 1f;

    int lev;

    #region singleton
    public static Endless singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    void Start()
    {
        lev = 1;
    }

    public bool NextLevel()
    {
        lev++;
        if (lev > PlayerPrefs.GetInt("Level"))
        {
            Multiply();
            return false;
        }
        return true;
    }

    void Multiply()
    {
        Debug.Log("Mniozeine");
        speedM *= speedMultiplier;
        spawnTimeM *= spawnTimeMultiplier;
        damageM *= damageMultiplier;
        damageMB *= damageMultiplierBoss;
        healthMB *= healthMultiplierBoss;
    }

    public void CalculateEnemy(GameObject enemy)
    {
        Enemy com = enemy.GetComponent<Enemy>();
        com.timeAfterSpawn *= spawnTimeM;
        com.damage *= damageM;
        com.addExp = 1;
        enemy.GetComponent<EnemyMovement>().speed *= speedM;
    }

    public void CalculateBoss(Boss boss)
    {
        boss.hp *= healthMB;
        boss.damage *= damageMB;
    }
}
