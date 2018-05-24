using UnityEngine;

[SerializeField]
public abstract class EnemyMovement : MonoBehaviour {

    public float speed = 10;

    Enemy enemy;

	void Start () {
        enemy = GetComponent<Enemy>();
	}

	void Update () {
        if (enemy.moving)
            Move();
	}

    protected virtual void Move()
    {
        Debug.Log("Moving");
    }

    public void SnapToBackground()
    {
        speed = FindObjectOfType<BackgroundMenager>().speed;
        enemy.moving = true;
        gameObject.layer = 10;
    }
}
