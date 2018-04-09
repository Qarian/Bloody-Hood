using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blade : MonoBehaviour {

    public float dmg;

    void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                collision.gameObject.GetComponent<Enemy>().Hit(dmg);
                break;
            case "Boss":
                collision.gameObject.GetComponent<Boss>().Hit(dmg);
                break;
            case "Projectile":
                collision.GetComponent<Projectile>().Hit();
                break;
            default:
                Debug.Log("Attacked with object of type: " + collision.tag);
                break;
        }
    }
}
