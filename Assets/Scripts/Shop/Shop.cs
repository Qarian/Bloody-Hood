using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

    public ItemsList list;
    public ShopItem[] slots;
    public GameObject buyWindow;
    public GameObject leftArrow;
    public GameObject rightArrow;
    public Text money;
    public AudioSource music;

    [Space]
    public AudioClip enterSound;
    public AudioClip buySound;
    public AudioClip ClickSound;

    AudioSource sound;

    [HideInInspector]
    public Category category = Category.Items;
    int startIndex =0;

    #region Singleton
    public static Shop singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion

    void Start()
    {
        leftArrow.SetActive(false);
        ShowItems();
        sound = GetComponent<AudioSource>();
        sound.clip = enterSound;
        sound.volume = PlayerPrefs.GetFloat("Sound");
        music.volume = PlayerPrefs.GetFloat("Music");
        sound.Play();
        money.text = PlayerPrefs.GetInt("Money").ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            GoToMenu();
    }

    public void GoToMenu()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(0);
    }

    public void Play()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene("Game");
    }

    public void ChangeCategory(int num)
    {
        if (category == (Category)num)
            return;
        category = (Category)num;
        startIndex = 0;
        ShowItems();
        leftArrow.SetActive(false);
        rightArrow.SetActive(true);
        MakeClickSound();
    }

    public void NextItems()
    {
        startIndex += slots.Length;
        ShowItems();
        leftArrow.SetActive(true);
    }

    public void PreviousItems()
    {
        startIndex -= slots.Length;
        if (startIndex == 0)
            leftArrow.SetActive(false);
        rightArrow.SetActive(true);
        ShowItems();
    }

    void ShowItems()
    {
        int max = 0;
        switch (category)
        {
            case Category.Items:
                max = slots.Length;
                if (slots.Length + startIndex > list.items.Length)
                {
                    max = list.items.Length % slots.Length;
                    for (int i = max; i < slots.Length; i++)
                    {
                        slots[i].gameObject.SetActive(false);
                    }
                    rightArrow.SetActive(false);
                }
                else if (slots.Length + startIndex == list.items.Length)
                    rightArrow.SetActive(false);
                for (int i = 0; i < max; i++)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].Begin(list.items[i + startIndex], i + startIndex);
                }
                break;

            case Category.Weapon:
                max = slots.Length;
                if (slots.Length + startIndex > list.weapons.Length)
                {
                    max = list.weapons.Length % slots.Length;
                    for (int i = max; i < slots.Length; i++)
                    {
                        slots[i].gameObject.SetActive(false);
                    }
                    rightArrow.SetActive(false);
                }
                else if (slots.Length + startIndex == list.weapons.Length)
                    rightArrow.SetActive(false);
                for (int i = 0; i < max; i++)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].Begin(list.weapons[i + startIndex], i + startIndex);
                }
                break;

            case Category.Armor:
                max = slots.Length;
                if (slots.Length + startIndex > list.armors.Length)
                {
                    max = list.armors.Length % slots.Length;
                    for (int i = max; i < slots.Length; i++)
                    {
                        slots[i].gameObject.SetActive(false);
                    }
                    rightArrow.SetActive(false);
                }
                else if (slots.Length + startIndex == list.armors.Length)
                    rightArrow.SetActive(false);
                for (int i = 0; i < max; i++)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].Begin(list.armors[i + startIndex], i + startIndex);
                }
                break;
            default:
                break;
        }
    }

    public void BuyWindowOpen(int num)
    {
        switch (category)
        {
            case Category.Items:
                buyWindow.SetActive(true);
                buyWindow.GetComponent<BuyWindow>().Begin(list.items[num], num);
                break;
            case Category.Weapon:
                buyWindow.SetActive(true);
                buyWindow.GetComponent<BuyWindow>().Begin(list.weapons[num], num);
                break;
            case Category.Armor:
                buyWindow.SetActive(true);
                buyWindow.GetComponent<BuyWindow>().Begin(list.armors[num], num);
                break;
            default:
                break;
        }
    }

    public void Buy(int num)
    {
        sound.clip = buySound;
        sound.Play();
        switch (category)
        {
            case Category.Items:
                PlayerPrefs.SetInt("Item" + num, PlayerPrefs.GetInt("Item" + num) + 1); // add item
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - list.items[num].cost); // take money
                break;
            case Category.Weapon:
                PlayerPrefs.SetInt("Weapon" + num, 1);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - list.weapons[num].cost);
                break;
            case Category.Armor:
                PlayerPrefs.SetInt("Armor" + num, 1);
                PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money") - list.armors[num].cost);
                break;
            default:
                break;
        }
        money.text = PlayerPrefs.GetInt("Money").ToString();
        PlayerPrefs.Save();
    }

    public void Equip(int num)
    {
        MakeClickSound();
        switch (category)
        {
            case Category.Weapon:
                PlayerPrefs.SetInt("Hand", num);
                break;
            case Category.Armor:
                PlayerPrefs.SetInt("Body", num);
                break;
            default:
                Debug.Log("Blad - prawdopodobnie equip na przedmiocie");
                break;
        }
        PlayerPrefs.Save();
    }

    public void MakeClickSound()
    {
        sound.clip = ClickSound;
        sound.Play();
    }
}

public enum Category
{
    Items = 0,
    Weapon,
    Armor
}
