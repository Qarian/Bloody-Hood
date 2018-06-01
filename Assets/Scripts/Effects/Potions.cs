using UnityEngine;

public class Potions : MonoBehaviour {

    private void Update()
    {
        if (Input.GetKeyDown("k"))
            DestroyAllEnemies();
    }

    public void DestroyAllEnemies()
    {
        Enemy[] objects = FindObjectsOfType<Enemy>();
        foreach (Enemy obj in objects)
        {
            if(obj.alive == true)
            {
                obj.Death();
            }
        }
    }

}
