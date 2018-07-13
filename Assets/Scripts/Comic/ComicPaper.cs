using UnityEngine;

public class ComicPaper : MonoBehaviour {

	public void Clicked()
    {
        Comic c = GetComponentInParent<Comic>();
        c.NextPage();
    }

#if UNITY_STANDALONE
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Clicked();
    }
#endif
}
