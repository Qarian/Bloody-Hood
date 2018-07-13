using UnityEngine;

public class Enemy : Element {

    [Space]
    public string title = "Enemy";
    public AudioClip deathSound;

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
    [SerializeField]
    int moneyWorth = 1;

    [Space]
    public float hitToDestroy = 1;

	void Start ()
    {
        MakeAudioSource();
        movement = GetComponent<EnemyMovement>();
        if (changeSprite)
        {
            int spriteNumber = Random.Range(0, sprites.Length);
            GetComponent<SpriteRenderer>().sprite = sprites[spriteNumber];
        }
	}

    void Update()
    {
        if (!moving)
        {
            if (!audios.isPlaying)
            {
                if (blood == null)
                    Destroy(gameObject);
                else
                    Death2();
            }
        }
    }

    public void Collide()
    {
        Destroy(gameObject);
    }

    public void Hit(float dmg)
    {
        hitToDestroy -= dmg;
        if (hitToDestroy <= 0)
        {
            Death();
            return;
        }  
    }

    public void Death()
    {
        alive = false;
        audios.Play();
        audios.volume = PlayerPrefs.GetFloat("Sound");
        moving = false;
        GetComponent<BoxCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = death;
        UIEffects.singleton.GenerateBloodEffect(Camera.main.WorldToScreenPoint(transform.position), addExp);
        GameManager.singleton.money += moneyWorth;
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
        audios.volume = 0.7f;
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
