using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(AudioSource))]
public class Player : MonoBehaviour {

    [SerializeField]
    float minAttack = 10;
    [SerializeField]
    float maxAttack = 20;
    public float attackSpeed = 0.2f;
    public float cooldown=0.1f;
    public float hp=5;
    float maxhp;
    [HideInInspector]
    public int exp=0;
    public int expToLevel = 3;

    AudioSource audiosource;
    SpriteRenderer sprite;
    GameObject blade;

    public Slider healthBar;
    public GameObject maxHp;
    
    void Start()
    {
        SetBlade();
        SetHealth();
        
        sprite = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
    }

    void SetHealth()
    {
        maxhp = hp;
        healthBar.minValue = 0;
        healthBar.maxValue = maxhp;
        healthBar.value = maxhp;
    }

    void SetBlade()
    {
        blade = transform.GetChild(0).gameObject;
        blade.SetActive(false);
        blade.GetComponent<Blade>().dmg = minAttack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Enemy":
                ReceivedDamage(collision.GetComponent<Enemy>().damage);
                collision.GetComponent<Enemy>().Collide();
                break;
            case "Obstacle":
                ReceivedDamage(collision.GetComponent<Obstacle>().damage);
                collision.GetComponent<Obstacle>().Collide();
                break;
            case "Boss":
                break;
            case  "Projectile":
                ReceivedDamage(collision.GetComponent<Projectile>().Damage());
                break;
            default:
                Debug.Log("Collided with object of type: " + collision.tag);
                break;
        }
    }

    public void AddAttack(int amount)
    {
        if (maxHp.activeSelf)
            return;

        float damage = blade.GetComponent<Blade>().dmg;
        if (damage + amount >= maxAttack)
        {
            maxHp.SetActive(true);
            amount = (int)(maxAttack - damage);
        }
        blade.GetComponent<Blade>().dmg += amount;

        Color percent = sprite.color;
        percent.g = (maxAttack - damage) / (maxAttack - minAttack);
        percent.b = (maxAttack - damage) / (maxAttack - minAttack);
        sprite.color = percent;
    }

    public void ReceivedDamage(float number)
    {
        hp -= number;
        if (hp <= 0)
        {
            GameManager.singleton.EndGame(false);
            Destroy(gameObject);
        }
        UpdateLife();
    }

    public void AddHp(float number)
    {
        hp += number;
        if (hp > maxhp)
            hp = maxhp;
        UpdateLife();
    }

    void UpdateLife()
    {
        healthBar.value = hp;
    }


    public float Attack()
    {
        StartCoroutine(AttackIENumerator());
        return attackSpeed + cooldown;
    }

    public float AttackWithoutCooldown()
    {
        StartCoroutine(AttackIENumerator());
        return attackSpeed;
    }

    IEnumerator AttackIENumerator()
    {
        blade.SetActive(true);
        if (PlayerPrefs.GetFloat("Sound") == 1)
            audiosource.Play();
        yield return new WaitForSeconds(attackSpeed);
        blade.SetActive(false);
    }

    public IEnumerator ComboAttack(float attackSpeed)
    {
        blade.SetActive(true);
        if (PlayerPrefs.GetFloat("Sound") == 1)
            audiosource.Play();
        yield return new WaitForSeconds(attackSpeed);
        blade.SetActive(false);
    }
}
