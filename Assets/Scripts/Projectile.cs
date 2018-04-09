using UnityEngine;

public class Projectile : MonoBehaviour {

    public bool reflective;

    [SerializeField]
    float damage = 1f;

    Vector2 dir = Vector2.zero;
    float speed;

    void Start()
    {
        Destroy(gameObject, 6f);
    }

    public void SetVelocity(Vector2 dir, float speed)
    {
        this.dir = dir;
        this.speed = speed;
    }

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

    public void Hit()
    {
        if (reflective)
        {
            speed = -speed;
            return;
        }
        Des();
    }

    public float Damage()
    {
        Des();
        return damage;
    }

    private void Des()
    {
        Destroy(gameObject, 0.001f);
    }
}
