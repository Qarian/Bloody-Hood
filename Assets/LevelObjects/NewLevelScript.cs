using UnityEngine;

[CreateAssetMenu(fileName = "Level ", menuName = "New Level")]
public class NewLevelScript : ScriptableObject {

    public GameObject boss;

    public Sprite normalBackground;
    public Sprite bossBackground;

    public Sprite[] comicStart;
    public Sprite[] comicBoss;
    public Sprite[] comicEnd;

}
