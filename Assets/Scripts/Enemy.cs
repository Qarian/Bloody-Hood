using UnityEngine;

public class Enemy : MonoBehaviour {

    public float destroyTime=5f;
    public int damage = 1;
    [HideInInspector]
    public float speed;
    public int hitToDestroy = 1;

    [SerializeField]
    int addExp = 1;
    bool alive = true;

	void Start ()
    {
        Destroy(gameObject, destroyTime);
	}

    void Update()
    {
        if(alive)
            transform.Translate(0, speed * Time.deltaTime, 0);
    }

    public void Hit()
    {
        hitToDestroy--;
        if (hitToDestroy <= 0)
        {
            GetComponent<AudioSource>().Play();
            alive = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddExp(addExp);
            Destroy(gameObject, 0.55f);
        }
    }
}
