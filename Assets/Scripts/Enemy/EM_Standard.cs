using UnityEngine;

public class EM_Standard : EnemyMovement{

    protected override void Move()
    {
        //base.Move();
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }

}
