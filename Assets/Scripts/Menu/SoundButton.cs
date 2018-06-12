using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {

    public Sprite on;
    public Sprite off;
    public AudioSource audioSource;
    Image image;

    void Start()
    {
        image = GetComponent<Image>();
        if (audioSource == null)
            audioSource = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>();

        if (PlayerPrefs.GetFloat("Music") == 0)
        {
            image.sprite = off;
            audioSource.volume = 0;
        }
        else
        {
            image.sprite = on;
            audioSource.volume = PlayerPrefs.GetFloat("Music");
        }
    }

	public void Click()
    {
        if (PlayerPrefs.GetFloat("Music") == 0)
        {
            PlayerPrefs.SetFloat("Music", 1);
            PlayerPrefs.SetFloat("Sound", 1);
            image.sprite = on;
            audioSource.volume = 1;
        }
        else
        {
            PlayerPrefs.SetFloat("Music", 0);
            PlayerPrefs.SetFloat("Sound", 0);
            image.sprite = off;
            audioSource.volume = 0;
        }
        PlayerPrefs.Save();
    }
}
