using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public float time1 = 2f;
    public string text1;
    [Space]
    public float time2 = 1f;
    public string text2;
    [Space]
    public float time3 = 5f;
    public string text3;
    [Space]
    [TextArea]
    public string textFinal;

    GameObject tutorialScreen;
    BackgroundMenager bm;
    PlayerMovementTutorial pmt;
    [Space]
    [Space]
    public GameObject enemyPrefab;
    [SerializeField]
    float distance = 20;
    Enemy[] enemies;

    GameObject button;

    //public EnemySpawner es;

    int part = 1;
    float speed;

    void Start () {
        tutorialScreen = FindObjectOfType<TutorialScreen>().gameObject;
        tutorialScreen.SetActive(false);
        bm = FindObjectOfType<BackgroundMenager>();
        pmt = FindObjectOfType<PlayerMovementTutorial>();
        StartCoroutine(Tut1());

        button = tutorialScreen.transform.GetChild(1).gameObject;
        button.SetActive(false);
    }

    public void Run(int num)
    {
        if (num == 1 && part == 1)
        {
            StartCoroutine(Tut2());
            pmt.Move(-1);
        }
        if (num == 2 && part == 2)
        {
            StartCoroutine(Tut3());
            pmt.Move(1);
        }
        if (num == 3 && part == 3)
        {
            Resume();
            pmt.player.Tap();
            pmt.pause = false;
            Debug.Log("part 1");
            StartCoroutine(EndTutorial());
        }
    }

    // Rusz w lewo
    IEnumerator Tut1()
    {
        yield return new WaitForSeconds(time1);
        Command(text1);
    }

    // Rusz w prawo
    IEnumerator Tut2()
    {
        Resume();
        part = 2;
        yield return new WaitForSeconds(time2);
        Command(text2);
        
    }

    // Atakuj
    IEnumerator Tut3()
    {
        Resume();
        pmt.pause = false;
        part = 3;
        GenerateEnemyWave();
        yield return new WaitForSeconds(time3);
        Command(text3);
    }



    void Command(string text)
    {
        if(enemies != null)
        {
            foreach(var e in enemies)
            {
                e.speed = 0;
            }
        }
        pmt.pause = true;
        tutorialScreen.SetActive(true);
        speed = bm.speed;
        bm.speed = 0;
        tutorialScreen.transform.GetChild(0).GetComponent<Text>().text = text;
    }

    void Resume()
    {
        if (enemies != null)
        {
            foreach (var e in enemies)
            {
                e.speed = -10;
            }
        }
        tutorialScreen.SetActive(false);
        bm.speed = speed;
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1f);
        tutorialScreen.SetActive(true);
        tutorialScreen.transform.GetChild(0).GetComponent<Text>().text = textFinal;
        button.SetActive(true);

        //Destroy(tutorialScreen);
        //es.enabled = true;
        Destroy(this);
    }

    void GenerateEnemyWave()
    {
        Transform points = pmt.playerPositions;
        enemies = new Enemy[3];
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(enemyPrefab, new Vector2(points.GetChild(i).position.x, points.GetChild(i).position.y + distance), Quaternion.identity, null);
            enemies[i] = go.GetComponent<Enemy>();
            enemies[i].speed = -10;
        }
    }

}
