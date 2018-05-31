using UnityEngine;

[SerializeField]
public abstract class EnemyMovement : MonoBehaviour {

    public float speed = 10;

    Element enemy;

	void Start () {
        enemy = GetComponent<Element>();
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
