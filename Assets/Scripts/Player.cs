using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float attackSpeed = 0.2f;

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
            if (canAttack)
            {
                StartCoroutine(Attack());
            }
        }
	}

    IEnumerator Attack()
    {
        blade.SetActive(true);
        canAttack = false;
        yield return new WaitForSeconds(attackSpeed);
        canAttack = true;
        blade.SetActive(false);
    }
}
