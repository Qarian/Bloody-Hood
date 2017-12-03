using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D),typeof(SpriteRenderer))]
public class Player : MonoBehaviour {

    public float attackSpeed = 0.2f;
    public float cooldown=0.1f;
    public float hp=5;
    float maxhp;

    SpriteRenderer sprite;
    GameObject blade;
    bool canAttack = true;
    
    void Start()
    {
        blade = transform.GetChild(0).gameObject;
        blade.SetActive(false);
        maxhp = hp;
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update () {
        if (Input.GetKeyDown("space"))
        {
            Tap();
        }
	}

    public void Tap()
    {
        if (canAttack)
        {
            StartCoroutine(Attack());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            ChangeHp(-collision.GetComponent<Enemy>().damage);
            Destroy(collision.gameObject);
        }
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

    IEnumerator Attack()
    {
        blade.SetActive(true);
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        blade.SetActive(false);
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
