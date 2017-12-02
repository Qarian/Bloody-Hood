using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenager : MonoBehaviour {
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("Menu");
        if (Input.GetKeyDown("r")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
