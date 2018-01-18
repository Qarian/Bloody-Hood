using UnityEngine;

public class Enemy : MonoBehaviour {

    public float destroyTime=5f;
    public int damage = 1;
    [HideInInspector]
    public float speed;

	void Start ()
    {
        Destroy(gameObject, destroyTime);
	}

    void Update()
    {
        transform.Translate(0, speed * Time.deltaTime, 0);
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
