using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(AudioSource))]
public class Player : MonoBehaviour {

    public float attackSpeed = 0.2f;
    public float cooldown=0.1f;
    public float hp=5;
    float maxhp;
    [HideInInspector]
    public int exp=0;
    public int expToBoss = 5;

    AudioSource audiosource;
    SpriteRenderer sprite;
    GameObject blade;
    [HideInInspector]
    public bool canAttack = true;
    
    void Start()
    {
        blade = transform.GetChild(0).gameObject;
        blade.SetActive(false);
        maxhp = hp;
        sprite = GetComponent<SpriteRenderer>();
        audiosource = GetComponent<AudioSource>();
    }

    public void Tap()
    {
        if (canAttack)
            StartCoroutine(Attack());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.tag == "Enemy")
        {
            if (collision.GetComponent<Enemy>() != null)
            {
                ChangeHp(-collision.GetComponent<Enemy>().damage);
                collision.GetComponent<Enemy>().Hit();
            }
            else
            {
                ChangeHp(-1);
                Destroy(collision.gameObject);
            }
        }
    }

    public void AddExp(int amount)
    {
        exp += amount;
        if (exp >= expToBoss)
            FindObjectOfType<GameMenager>().BossBattle();
    }

    public void ChangeHp(int number)
    {
        hp += number;
        if (hp > maxhp) hp = maxhp;
        else if (hp <= 0)
        {
            Destroy(gameObject);
        }
        Color percent = sprite.color;
        percent.a = hp/maxhp;
        sprite.color = percent;

    }

    public IEnumerator Attack()
    {
        blade.SetActive(true);
        canAttack = false;
        audiosource.Play();
        yield return new WaitForSeconds(attackSpeed);
        blade.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
