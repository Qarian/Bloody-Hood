using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    [SerializeField]
    Sprite boom;
    [SerializeField]
    Sprite blood;
    [SerializeField]
    float backgroundSpeed;
    [SerializeField]
    float deathTime=0.32f;
    [Space]

    public float destroyTime=5f;
    public int damage = 1;
    [HideInInspector]
    public float speed = -10;
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
            //Destroy(gameObject, 0.55f);
            StartCoroutine(Death());
        }
    }

    IEnumerator Death()
    {
        GetComponent<AudioSource>().Play();
        alive = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddExp(addExp);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = boom;
        yield return new WaitForSeconds(deathTime);
        GetComponent<SpriteRenderer>().sprite = blood;
        //speed = backgroundSpeed;
        alive = true;
    }
}
