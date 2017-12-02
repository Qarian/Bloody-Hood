using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Blade : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hit();
        }
    }

}
