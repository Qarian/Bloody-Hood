using UnityEngine;

[CreateAssetMenu(fileName = "Level ", menuName = "New/Level")]
public class LevelScript : ScriptableObject {

    public GameObject boss;

    [Space]
    [Tooltip("Grafika tla podczas gry")]
    public Sprite normalBackground;
    [Tooltip("Grafika tla podczas walki z Bossem")]
    public Sprite bossBackground;
    [Tooltip("Predkosc poruszania sie tla podczas gry")]
    public float speed;
    [Tooltip("Predkosc poruszania sie tla przed Bossem")]
    public float speed2;

    [Space]
    [Tooltip("Komiksy wyswietlane przed gra")]
    public Sprite[] comicStart;
    [Tooltip("Komiksy wyswietlane przed Bossem")]
    public Sprite[] comicBoss;
    [Tooltip("Komiksy wyswietlane po pokonaniu Bossa")]
    public Sprite[] comicEnd;

    [Space]
    [Tooltip("Czy gra ma by� bez konca")]
    public bool endless;
    [Tooltip("Liczba przeciwnikow w tym poziomie")]
    public int waveCount;
    [Tooltip("Sposob w jaki b�d� wybierani przeciwnicy")]
    public SpawnChoice spawnChoice;
    [Tooltip("Lista przeciwnikow na poziom")]
    public GameObject[] enemies;
}
