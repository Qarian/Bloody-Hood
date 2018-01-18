using UnityEngine;

public class Boss : MonoBehaviour {

    public int hp = 100;

    public float bulletSpeed = 10f;
    [SerializeField]
    GameObject bullet;
    Transform[] bulletPoints;
    

    void Start()
    {
        GetBulletsPoints();
    }

    public void Attack(int col)
    {
        GameObject go = Instantiate(bullet, bulletPoints[col].position, Quaternion.identity);
        go.GetComponent<Enemy>().speed = -bulletSpeed;
    }

    public void Hit(int dmg)
    {
        Debug.Log("Zadano " + dmg + " obrazen");
        hp -= dmg;
        if (hp <= 0)
            Destroy(gameObject);
    }

    void GetBulletsPoints()
    {
        Transform positions = FindObjectOfType<PlayerMovement1>().playerPositions;
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
