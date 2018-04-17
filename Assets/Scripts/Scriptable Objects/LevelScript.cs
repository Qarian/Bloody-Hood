using UnityEngine;

[CreateAssetMenu(fileName = "Level ", menuName = "New/Level")]
public class LevelScript : ScriptableObject {

    public GameObject boss;

    [Space]
    public Sprite normalBackground;
    public Sprite bossBackground;

    [Space]
    public Sprite[] comicStart;
    public Sprite[] comicBoss;
    public Sprite[] comicEnd;

    [Space]
    public bool endless;
    public int waveCount;
    public SpawnChoice spawnChoice;
    public GameObject[] enemies;
}
