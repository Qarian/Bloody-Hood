using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour {

    public Text text;

    public void Begin(bool win)
    {
        if (win)
            text.text = "Wygrałeś";
        else
            text.text = "Przegrałeś";
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        string name = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(name);
    }
}
