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

    public ComicLevel comics;

    [Space]
    public AudioClip music;
    public bool changeMusic;
    public AudioClip music2;

    [Space]
    [Tooltip("Sposob w jaki beda wybierani przeciwnicy")]
    public SpawnChoice spawnChoice;
    [Tooltip("Liczba przeciwnikow w tym poziomie")]
    public int waveCount;
    [Tooltip("Lista przeciwnikow na poziom (tylko do wczytywania z pliku)")]
    public LevelEnemies levelEnemies;
    [Tooltip("Lista przeciwnikow na poziom")]
    public GameObject[] enemies;
}
