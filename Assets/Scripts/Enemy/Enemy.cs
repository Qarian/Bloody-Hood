using UnityEngine;

public class Enemy : MonoBehaviour {

    public string title = "Enemy";
    public AudioClip deathSound;
    public EnemyMovement move;

    [HideInInspector]
    public EnemyMovement movement;
    AudioSource audios;

    [Space]
    public bool changeSprite = false;
    public Sprite[] sprites;

    [Space]
    [SerializeField]
    Sprite death;
    [SerializeField]
    Sprite blood;

    [Space]
    public float timeToSpawn;
    public float timeAfterSpawn = 1f;

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
        MakeAudioSource();
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
            if (!audios.isPlaying)
                Death2();
        }
    }

    void Death()
    {
        audios.Play();
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

    void MakeAudioSource()
    {
        audios = gameObject.AddComponent<AudioSource>();
        audios.playOnAwake = false;
        audios.loop = false;
        audios.clip = deathSound;
    }

    public static GameObject Create(Vector2 position, Enemy component)
    {
        GameObject go = new GameObject(component.title);
        go.tag = "Enemy";
        go.layer = 8;
        go.transform.position = position;
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>().size = new Vector2(1.15f, 2.3f);
        go.AddComponent<Rigidbody2D>().gravityScale = 0;
        go.AddComponent<Enemy>();
        CopyComponent(go.GetComponent<Enemy>(), component);


        return go;
    }

    static void CopyComponent(Component copy, Component original)
    {
        System.Type type = original.GetType();
        System.Reflection.FieldInfo[] fields = type.GetFields();
        foreach (System.Reflection.FieldInfo field in fields)
        {
            field.SetValue(copy, field.GetValue(original));
        }
    }

}
