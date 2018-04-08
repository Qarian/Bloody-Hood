using UnityEngine;

public class ComicPaper : MonoBehaviour {

	public void Clicked()
    {
        Comic c = GetComponentInParent<Comic>();
        c.NextPage();
        //d�wi�k zmiany strony
        //animacje
    }

    void Update()
    {
        if (Input.anyKeyDown)
            Clicked();
    }
}
