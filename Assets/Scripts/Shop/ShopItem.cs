using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour {

    Image image;
    int itemNumber;

    public void Begin(Item item, int num)
    {
        if(image == null)
            image = GetComponent<Image>();
        itemNumber = num;
        image.sprite = item.icon;
    }

    public void Begin(Weapon weapon, int num)
    {
        if (image == null)
            image = GetComponent<Image>();
        itemNumber = num;
        image.sprite = weapon.icon;
    }

    public void Begin(Armor armor, int num)
    {
        if (image == null)
            image = GetComponent<Image>();
        itemNumber = num;
        image.sprite = armor.icon;
    }

    public void Click()
    {
        Shop.singleton.BuyWindowOpen(itemNumber);
    }
}
