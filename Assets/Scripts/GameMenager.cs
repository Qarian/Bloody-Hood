using UnityEngine;

public class GameMenager : MonoBehaviour {
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
	}
}
