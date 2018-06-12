using UnityEngine;
using UnityEngine.SceneManagement;

public class Shop : MonoBehaviour {

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GoToMenu();
    }


    public void GoToMenu()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

}
