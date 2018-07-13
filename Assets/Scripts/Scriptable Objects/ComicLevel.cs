using UnityEngine;

[CreateAssetMenu(fileName = "Comic Level", menuName = "New/Comic Level")]
public class ComicLevel : ScriptableObject {

    [Tooltip("Komiksy wyswietlane przed gra")]
    public GameObject[] start;
    [Tooltip("Komiksy wyswietlane przed Bossem")]
    public GameObject[] boss;
    [Tooltip("Komiksy wyswietlane po pokonaniu Bossa")]
    public GameObject[] end;
}
