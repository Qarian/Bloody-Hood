using UnityEngine;

public class BloodEffect : MonoBehaviour {

    public float acceleration;
    public Transform target;

    public float friction = 0.03f;

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
        rb.velocity = rb.velocity * (1 - friction);
    }

    void Update()
    {
        if ((target.position - transform.position).magnitude < 1f)
            End();
    }

    public void End(bool playSound = true)
    {
        if (playSound)
            target.GetComponent<AudioSource>().Play();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddAttack(1);
        Destroy(gameObject);
    }

}
