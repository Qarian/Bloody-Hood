using UnityEngine;
using System.Collections;

[RequireComponent(typeof(PlayerMovement))]
public class Player : MonoBehaviour {

    public float attackSpeed = 0.2f;
    public float cooldown=0.1f;

    GameObject blade;
    bool canAttack = true;
    
    void Start()
    {
        blade = transform.GetChild(0).gameObject;
        blade.SetActive(false);
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
