using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "New/Level")]
public class LevelScript : ScriptableObject {

    public LevelScript nextLevel;
    public GameObject boss;

    [Space]
    [Tooltip("Grafika tla podczas gry")]
    public Sprite normalBackground;
    [Tooltip("Grafika tla podczas walki z Bossem")]
    public Sprite bossBackground;
    public Sprite lastBackground;
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
    public AudioClip music;
    public bool changeMusic;
    public AudioClip music2;

    [Space]
    [Tooltip("Czy gra ma byc bez konca")]
    public bool endless;
    [Tooltip("Sposob w jaki beda wybierani przeciwnicy")]
    public SpawnChoice spawnChoice;
    [Tooltip("Liczba przeciwnikow w tym poziomie")]
    public int waveCount;
    [Tooltip("Lista przeciwnikow na poziom (tylko do wcczytywania z pliku)")]
    public LevelEnemies levelEnemies;
    [Tooltip("Lista przeciwnikow na poziom")]
    public GameObject[] enemies;
}
