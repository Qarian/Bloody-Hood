using UnityEngine;

public class BloodEffect : MonoBehaviour {

    public float acceleration;
    public Transform target;

    Rigidbody2D rb;

    void Start()
    {
        rb = gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        gameObject.tag = "Effect";
    }

    void FixedUpdate()
    {
        rb.AddForce((target.position - transform.position) * acceleration * Time.fixedDeltaTime);
        rb.velocity = rb.velocity * 0.95f;
    }

    void Update()
    {
        if ((target.position - transform.position).magnitude < 6f)
            End();
    }

    public void End()
    {
        target.GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddExp(1);
        Destroy(gameObject);
    }

}
