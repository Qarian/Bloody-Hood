using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public GameObject animationsHolder;
    GameObject[] animations;

    [Space]
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
        StartCoroutine(WCommand(time1));

        button = tutorialScreen.transform.GetChild(1).gameObject;
        button.SetActive(false);

        int count = animationsHolder.transform.childCount;
        animations = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            animations[i] = animationsHolder.transform.GetChild(i).gameObject;
            animations[i].SetActive(false);
        }
    }

    public void Run(int num)
    {
        //Debug.Log("Num: " + num + ", Part: " + part);
        if (num == 1 && part == 1)
        {
            Resume();
            StartCoroutine(WCommand(time2));
            part = 2;
            pmt.Move(-1);
        }
        if (num == 2 && part == 2)
        {
            Resume();
            pmt.pause = false;
            GenerateEnemyWave();
            StartCoroutine(WCommand(time3));
            part = 3;
            pmt.Move(1);
        }
        if (num == 3 && part == 3)
        {
            Resume();
            pmt.player.Tap();
            pmt.pause = false;
            StartCoroutine(EndTutorial());
            part = 4;
        }
    }


    IEnumerator WCommand(float time)
    {
        yield return new WaitForSeconds(time);
        Command();
    }

    void Command()
    {
        if (enemies != null)
        {
            foreach (var e in enemies)
            {
                e.movement.speed = 0;
            }
        }
        pmt.pause = true;
        speed = bm.speed;
        bm.speed = 0;
        tutorialScreen.SetActive(true);
        tutorialScreen.transform.GetChild(0).GetComponent<Text>().text = "";
        if (part-1 < animations.Length)
            animations[part-1].SetActive(true);
    }

    void Resume()
    {
        if (enemies != null)
        {
            foreach (var e in enemies)
            {
                e.movement.speed = 10;
            }
        }
        tutorialScreen.SetActive(false);
        bm.speed = speed;
        if (part-1 < animations.Length)
            animations[part - 1].SetActive(false);
    }

    IEnumerator EndTutorial()
    {
        yield return new WaitForSeconds(1f);
        tutorialScreen.SetActive(true);
        tutorialScreen.transform.GetChild(0).GetComponent<Text>().text = textFinal;
        button.SetActive(true);
        Destroy(this);
    }

    void GenerateEnemyWave()
    {
        Transform points = pmt.playerPositions;
        enemies = new Enemy[3];
        for (int i = 0; i < 3; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, new Vector2(points.GetChild(i).position.x, points.GetChild(i).position.y + distance), Quaternion.identity).GetComponent<Enemy>();
        }
    }

}
