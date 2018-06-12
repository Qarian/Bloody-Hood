using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    public Text amount;
    public int maxAmount;

    public int itemNumber;
    string itemName;

    void Start()
    {
        itemName = "Item" + itemNumber.ToString();
        amount.text = PlayerPrefs.GetInt(itemName).ToString() + "/" + maxAmount.ToString();
    }

    public void BuyItem()
    {
        int num = PlayerPrefs.GetInt(itemName);
        if (num >= maxAmount)
            return;
        //zabierz pieniazki graczowi
        num++;
        PlayerPrefs.SetInt(itemName, num);
        amount.text = num.ToString() + "/" + maxAmount.ToString();
    }

}
