using UnityEngine;

public class Enemy : MonoBehaviour {

    [HideInInspector]
    public EnemyMovement movement;
    AudioSource audioSource;

    public bool changeSprite = false;
    public Sprite[] sprites;

    [Space]
    [SerializeField]
    Sprite death;
    [SerializeField]
    Sprite blood;

    [Space]
    public float timetoSpawn;
    public float timeAfterSpawn;

    [Space]
    public float destroyTime=5f;
    public int damage = 1;
    public float hitToDestroy = 1;

    [SerializeField]
    int addExp = 1;
    public bool moving = true;

	void Start ()
    {
        Destroy(gameObject, destroyTime);
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<EnemyMovement>();
        if (changeSprite)
        {
            int spriteNumber = Random.Range(0, sprites.Length);
            GetComponent<SpriteRenderer>().sprite = sprites[spriteNumber];
        }
	}

    public void Hit(float dmg)
    {
        hitToDestroy -= dmg;
        if (hitToDestroy <= 0)
            Death();
    }

    void Update()
    {
        if (!moving)
        {
            if (!audioSource.isPlaying)
                Death2();
        }
    }

    void Death()
    {
        GetComponent<AudioSource>().Play();
        moving = false;
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().AddExp(addExp);
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = death;
    }

    void Death2()
    {
        GetComponent<EnemyMovement>().SnapToBackground();
        GetComponent<SpriteRenderer>().sprite = blood;
    }
}
