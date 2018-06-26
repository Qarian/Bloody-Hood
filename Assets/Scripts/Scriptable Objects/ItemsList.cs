using UnityEngine;

[CreateAssetMenu(fileName = "Items List", menuName = "New/Items List")]
public class ItemsList : ScriptableObject{

    public Item[] items;
    public Weapon[] weapons;
    public Armor[] armors;

}

[System.Serializable]
public struct Item
{
    public Sprite icon;
    public string name;
    public int cost;
    public int maxAmount;
}

[System.Serializable]
public struct Weapon
{
    public Sprite icon;
    public string name;
    public int cost;
    public int minAttack;
    public int maxAttack;
}

[System.Serializable]
public struct Armor
{
    public Sprite icon;
    public string name;
    public int cost;
    public int defence;
}