using UnityEngine;

public class MusicScript : MonoBehaviour {

    public AudioClip sound2;

    AudioSource aus;

    private void Start()
    {
        aus = GetComponent<AudioSource>();
        aus.loop = false;
    }

    void Update()
    {
        if (!aus.isPlaying)
        {
            aus.clip = sound2;
            aus.Play();
            aus.loop = true;
            enabled = false;
        }
    }

}
