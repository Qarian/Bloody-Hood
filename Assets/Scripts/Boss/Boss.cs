using UnityEngine;

public class Boss : MonoBehaviour {

    public float hp = 100;

    public float bulletSpeed = 10f;
    [SerializeField]
    GameObject bullet;
    Transform[] bulletPoints;

    [HideInInspector]
    public BossHp bossHp;

    void Start()
    {
        GetBulletsPoints();

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
            GameManager.singleton.EndGame(true);
            Destroy(bossHp.gameObject);
            Destroy(gameObject);
        }
    }

    void GetBulletsPoints()
    {
        Transform positions;
        if(FindObjectOfType<PlayerMovement1>()!= null)
            positions = FindObjectOfType<PlayerMovement1>().playerPositions;
        else
            positions = FindObjectOfType<PlayerMovementTutorial>().playerPositions;

        int pointCount = positions.childCount;
        float[] bulletPosx = new float[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            bulletPosx[i] = positions.GetChild(i).position.x;
        }
        bulletPoints = new Transform[pointCount];
        for (int i = 0; i < pointCount; i++)
        {
            bulletPoints[i] = transform;
            bulletPoints[i].position = new Vector2(bulletPosx[i], transform.position.y);
        }
    }
}
