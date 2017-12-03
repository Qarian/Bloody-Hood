using UnityEngine;

public class Enemy : MonoBehaviour {

    public float destroyTime=5f;
    public int damage = 1;
    [HideInInspector]
    public bool is2metod = false;
    float time=0;

	void Start ()
    {
        Destroy(gameObject, destroyTime);
	}

    void Update()
    {
        if (is2metod)
        {
            time += Time.deltaTime;
            if (time > 1f)
            {
                time -= 1f;
                transform.Translate(0f, -4f, 0f);
            }
        }
    }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
