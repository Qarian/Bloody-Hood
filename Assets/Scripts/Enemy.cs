using UnityEngine;

public class Enemy : MonoBehaviour {

    public float destroyTime=5f;

	void Start ()
    {
        Destroy(gameObject, destroyTime);
	}
}
