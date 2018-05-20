using UnityEngine;

[CreateAssetMenu(fileName = "Enemies", menuName = "New/Level Enemies")]
public class LevelEnemies : ScriptableObject
{
    public Wave[] waves;
	
}

[System.Serializable]
public struct Wave
{
    public int enemyId;
    public int row;
    public float time;
}

