using UnityEngine;

public class Enemy : MonoBehaviour {

    public float destroyTime=5f;
    public int damage = 1;

	void Start ()
    {
        Destroy(gameObject, destroyTime);
	}

    public void Hit()
    {
        Destroy(gameObject);
    }
}
