using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour {
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
        if (Input.GetKeyDown("r")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
