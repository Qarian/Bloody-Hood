using UnityEngine;

public class ComicPaper : MonoBehaviour {

	public void Clicked()
    {
        Comic c = GetComponentInParent<Comic>();
        c.NextPage();
        //d�wi�k zmiany strony
        //animacje
        Debug.Log("Click");
    }
}
