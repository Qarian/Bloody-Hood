using UnityEngine;

[CreateAssetMenu(fileName = "List of Levels", menuName = "New/List of Levels")]
public class ListOfLevels : ScriptableObject {

    public LevelScript[] list;
}
