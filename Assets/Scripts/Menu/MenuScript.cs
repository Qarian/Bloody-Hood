using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Slot1"))
        {
            SetPlayerPrefs();
            SceneManager.LoadScene(1);
        }
        else
        {
            if(PlayerPrefs.GetInt("Slot1") != 1)
            {
                SetPlayerPrefs();
                SceneManager.LoadScene(1);
            }
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

    public static void SetPlayerPrefs()
    {
        #region Settings
        PlayerPrefs.SetFloat("Music", 1f);
        PlayerPrefs.SetFloat("Sound", 1f);
        #endregion

        #region Items
        PlayerPrefs.SetInt("Item1", 10);

        PlayerPrefs.SetInt("Slot1", 1);
        #endregion
        PlayerPrefs.Save();
    }
}
