using UnityEngine;

public class Boss : MonoBehaviour {

    public int hp = 100;
	
	public void Hit(int dmg)
    {
        Debug.Log("Zadano " + dmg + " obrazen");
        hp -= dmg;
        if (hp <= 0)
            Destroy(gameObject);
    }
}
