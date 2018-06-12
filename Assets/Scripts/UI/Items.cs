using UnityEngine;
using UnityEngine.UI;
using System;

public class Items : MonoBehaviour {

    public int slot;
    public Text amountText;

    int amount;
    int item;
    Action[] potions;

    private void Start()
    {
        item = PlayerPrefs.GetInt("Slot" + slot.ToString());
        if (item < 1)
        {
            gameObject.SetActive(false);
            return;
        }
        amount = PlayerPrefs.GetInt("Item" + item.ToString());
        amountText.text = amount.ToString();
        SetPotions();
    }

    void SetPotions()
    {
        potions = new Action[1];
        potions[0] = DestroyAllEnemies;
    }

    public void Use()
    {
        if (amount > 0)
        {
            potions[item - 1]();
            amount--;
            amountText.text = amount.ToString();
            PlayerPrefs.SetInt("Item" + item.ToString(), amount);
            PlayerPrefs.Save();
        }
    }

    #region Potions
    void DestroyAllEnemies()
    {
        Enemy[] objects = FindObjectsOfType<Enemy>();
        foreach (Enemy obj in objects)
        {
            if(obj.alive == true)
            {
                obj.Death();
            }
        }
    }

    #endregion
}
