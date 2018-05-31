using UnityEngine;

public class Boss : MonoBehaviour {

    public float hp = 100;

    public float bulletSpeed = 10f;
    [SerializeField]
    GameObject bullet;
    Transform[] bulletPoints;

    BossHp bossHp;

    void Start()
    {
        GetBulletsPoints();

        bossHp = GameManager.singleton.bossHp;
        bossHp.gameObject.SetActive(true);
        bossHp.Begin(hp);
    }

    public void Attack(int col)
    {
        GameObject go = Instantiate(bullet, bulletPoints[col].position, Quaternion.identity);
        go.GetComponent<Projectile>().SetVelocity(new Vector2(0,-1), bulletSpeed);
    }

    public void Hit(float dmg)
    {
        hp -= dmg;
        bossHp.NewHp(hp);
        if (hp <= 0)
        {
            DestroyBoss();
        }
    }

    void GetBulletsPoints()
    {
        Transform[] positions;
        positions = FindObjectOfType<PlayerMovement>().positions;

        float[] bulletPosx = new float[3];
        for (int i = 0; i < 3; i++)
        {
            bulletPosx[i] = positions[i].position.x;
        }
        bulletPoints = new Transform[3];
        for (int i = 0; i < 3; i++)
        {
            bulletPoints[i] = transform;
            bulletPoints[i].position = new Vector2(bulletPosx[i], transform.position.y);
        }
    }

    public void DestroyBoss()
    {
        GameManager.singleton.EndGame(true);
        bossHp.gameObject.SetActive(false);
        Destroy(gameObject);
    }

}
