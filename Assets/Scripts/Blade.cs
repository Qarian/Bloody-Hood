using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blade : MonoBehaviour {

    public int dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hit();
        }
        if (collision.tag == "Boss")
        {
            Debug.Log("Hit");
            collision.gameObject.GetComponent<Boss>().Hit(dmg);
        }
    }

}
