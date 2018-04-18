using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenScript : MonoBehaviour {

    public Text text;
    public AudioClip winSound;
    public AudioClip lossSound;

    public void Begin(bool win)
    {
        if (win)
        {
            text.text = "Wygrałeś";
            MusicScript.singleton.ChangeMusic(winSound);
        }
        else
        {
            text.text = "Przegrałeś";
            MusicScript.singleton.ChangeMusic(lossSound);
        }
    }

    public void GoToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void Restart(string name)
    {
        Time.timeScale = 1;
        if(name == string.Empty)
            SceneManager.LoadScene("Game");
        else
            SceneManager.LoadScene(name);
    }
}
