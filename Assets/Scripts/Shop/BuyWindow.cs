using UnityEngine;
using UnityEngine.UI;

public class BuyWindow : MonoBehaviour {

    public Text text;
    public Image icon;
    public Text amountText;
    public Text costText;
    public GameObject buyButton;
    public GameObject EquipButton;

    int number;
    int amount;
    int maxAmount;

    public void Begin(Item item, int num)
    {
        text.text = item.name;
        icon.sprite = item.icon;
        maxAmount = item.maxAmount;
        amount = PlayerPrefs.GetInt("Item" + num);
        amountText.text = amount + " / " + maxAmount;
        costText.text = item.cost.ToString();
        number = num;
        if (amount < maxAmount && PlayerPrefs.GetInt("Money") >= item.cost)
            buyButton.SetActive(true);
    }

    public void Begin(Weapon weapon, int num)
    {
        text.text = weapon.name;
        icon.sprite = weapon.icon;
        maxAmount = weapon.maxAttack;
        amount = weapon.minAttack;
        amountText.text = amount + " / " + maxAmount;
        costText.text = weapon.cost.ToString();
        number = num;
        
        if (PlayerPrefs.GetInt("Weapon" + num) == 0 && PlayerPrefs.GetInt("Money") >= weapon.cost)
            buyButton.SetActive(true);
        else if (PlayerPrefs.GetInt("Hand") != num)
            EquipButton.SetActive(true);
    }

    public void Begin(Armor armor, int num)
    {
        text.text = armor.name;
        icon.sprite = armor.icon;
        amount = armor.defence;
        amountText.text = amount + " HP";
        costText.text = armor.cost.ToString();
        number = num;
        if (PlayerPrefs.GetInt("Armor" + num) == 0 && PlayerPrefs.GetInt("Money") >= armor.cost)
            buyButton.SetActive(true);
        else if (PlayerPrefs.GetInt("Body") != num)
            EquipButton.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
        buyButton.SetActive(false);
        EquipButton.SetActive(false);
    }

    public void Buy()
    {
        Shop.singleton.Buy(number);
        switch (Shop.singleton.category)
        {
            case Category.Items:
                amount++;
                amountText.text = amount + " / " + maxAmount;
                if (amount >= maxAmount || PlayerPrefs.GetInt("Money") < int.Parse(costText.text))
                    buyButton.SetActive(false);
                break;
            case Category.Weapon:
                buyButton.SetActive(false);
                EquipButton.SetActive(true);
                break;
            case Category.Armor:
                buyButton.SetActive(false);
                EquipButton.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void Equip()
    {
        Shop.singleton.Equip(number);
        EquipButton.SetActive(false);
    }

}
