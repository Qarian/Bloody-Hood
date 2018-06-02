using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

	public void Continue()
    {
        GameManager.singleton.Continue();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
