using UnityEngine;

public class MusicScript : MonoBehaviour {

    public bool change = true;
    public AudioClip sound2;

    AudioSource aus;

    #region singleton
    public static MusicScript singleton;

    void Awake()
    {
        singleton = this;
    }

    #endregion


    private void Start()
    {
        aus = GetComponent<AudioSource>();
        if (change)
            aus.loop = false;
    }

    void Update()
    {
        if (change)
        {
            if (!aus.isPlaying)
            {
                aus.clip = sound2;
                aus.Play();
                aus.loop = true;
                change = false;
            }
        }
    }

    public void ChangeMusic(AudioClip clip, bool loop = false)
    {
        aus.clip = clip;
        aus.loop = loop;
        change = false;
        aus.Play();
    }

}
