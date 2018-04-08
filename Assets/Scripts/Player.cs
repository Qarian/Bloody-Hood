using UnityEngine;
using System.Collections;
using UnityEngine.UI; //dla debuga

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer), typeof(AudioSource))]
public class Player : MonoBehaviour {

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
    [HideInInspector]
    public bool canAttack = true;

    public Slider damage;
    public Slider experience;
    
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
        if (collision.GetComponent<Enemy>() != null)
        {
            ChangeHp(-collision.GetComponent<Enemy>().damage);
            Destroy(collision.gameObject);
        }
        else if(collision.GetComponent<Boss>() != null)
        {
            return;
        }
        else
        {
            ChangeHp(-1);
            Destroy(collision.gameObject);
        }
    }

    public void AddExp(int amount)
    {
        exp += amount;
        if (exp >= expToLevel)
        {
            exp -= expToLevel;
            blade.GetComponent<Blade>().dmg += 2;
            damage.value = blade.GetComponent<Blade>().dmg;
        }
        experience.value = exp;
        /*
        if (exp >= expToLevel)
            GameMenager.singleton.BossBattleReady();
        */
    }

    public void ChangeHp(int number)
    {
        hp += number;

        if (hp > maxhp)
            hp = maxhp;

        else if (hp <= 0)
        {
            GameManager.singleton.EndGame(false);
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
