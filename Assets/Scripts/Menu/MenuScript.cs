using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Music"))
        {
            PlayerPrefs.SetFloat("Music", 1f);
            PlayerPrefs.SetFloat("Sound", 1f);
            SceneManager.LoadScene(1);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) Exit();
    }

    public void LoadScene(int num)
    {
        SceneManager.LoadScene(num);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
