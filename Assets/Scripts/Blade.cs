using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blade : MonoBehaviour {

    public int dmg;

    Player player;

    private void Start()
    {
        player = transform.GetComponentInParent<Player>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                collision.gameObject.GetComponent<Enemy>().Hit();
                player.AddExp(1);
            }
                
        }
        if (collision.tag == "Boss")
        {
            collision.gameObject.GetComponent<Boss>().Hit(dmg);
        }
    }

}
