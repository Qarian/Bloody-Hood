using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour {

    public Text version;

    void Awake()
    {
        if (!PlayerPrefs.HasKey("Slot1"))
        {
            SetPlayerPrefs();
            SceneManager.LoadScene(1);
        }
        else
        {
            if(PlayerPrefs.GetInt("Slot1") != 0)
            {
                SetPlayerPrefs();
                SceneManager.LoadScene(1);
            }
        }

        version.text = "Version " + Application.version;
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
        for (int i = 1; i < 5; i++)
        {
            PlayerPrefs.SetInt("Item" + i, 0);
        }
        PlayerPrefs.SetInt("Item0", 10);

        PlayerPrefs.SetInt("Slot1", 0);
        #endregion
        #region Weapons
        for (int i = 1; i < 4; i++)
        {
            PlayerPrefs.SetInt("Weapon" + i, 0);
        }
        PlayerPrefs.SetInt("Weapon0", 1);

        PlayerPrefs.SetInt("Hand", 0);
        #endregion
        #region Armors
        for (int i = 1; i < 4; i++)
        {
            PlayerPrefs.SetInt("Armor" + i, 0);
        }
        PlayerPrefs.SetInt("Armor0", 1);

        PlayerPrefs.SetInt("Body", 0);
        #endregion

        PlayerPrefs.SetInt("Level", 0);
        PlayerPrefs.SetInt("Money", 1000);
        PlayerPrefs.Save();
    }
}
