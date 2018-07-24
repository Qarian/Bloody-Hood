using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {

    public GameObject animationsHolder;
    GameObject[] animations;

    [Space]
    public float time1 = 2f;
    [Space]
    public float time2 = 1f;
    [Space]
    public float time3 = 5f;
    public float timeForEnemyWave = 2.6f;
    [Space]
    public float time4= 5f;

    [Space]
    [TextArea]
    public string textFinal;

    [Space]
    public GameObject rock;
    public int wavesRock = 5;

    [Space]
    public GameObject enemy;
    public int wavesEnemy = 5;

    GameObject tutorialScreen;
    BackgroundMenager bm;
    PlayerMovementTutorial pmt;
    EnemySpawner spawner;
    [Space]
    [Space]
    GameObject enemyPrefab;
    [SerializeField]
    float distance = 20;
    Enemy[] enemies;

    GameObject button;

    int part = 1;
    float speed;

    void Start()
    {
        tutorialScreen = FindObjectOfType<TutorialScreen>().gameObject;
        tutorialScreen.SetActive(false);
        bm = FindObjectOfType<BackgroundMenager>();
        pmt = FindObjectOfType<PlayerMovementTutorial>();
        spawner = FindObjectOfType<EnemySpawner>();

        button = tutorialScreen.transform.GetChild(1).gameObject;
        button.SetActive(false);

        int count = animationsHolder.transform.childCount;
        animations = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            animations[i] = animationsHolder.transform.GetChild(i).gameObject;
            animations[i].SetActive(false);
        }

        enemyPrefab = GameManager.singleton.level.enemies[0];
    }

    public void Begin()
    {
        StartCoroutine(WCommand(time1));
    }

    public void Run(int num)
    {
        if (num == 1 && part == 1)
        {
            Resume();
            StartCoroutine(WCommand(time2));
            part = 2;
            pmt.movement.MoveLeft();
        }
        if (num == 2 && part == 2)
        {
            Resume();
            pmt.pause = false;
            spawner.SpawnWaves(rock, wavesRock);
            StartCoroutine(WCommand(time3 + timeForEnemyWave));
            StartCoroutine(WaitAction(time3, GenerateEnemyWave));
            part = 3;
            pmt.movement.MoveRight();
        }
        if (num == 3 && part == 3)
        {
            Resume();
            pmt.player.Attack();
            pmt.pause = false;
            spawner.SpawnWaves(enemy, wavesEnemy);
            StartCoroutine(WaitAction(time4, EndTutorial));
            part = 4;
        }
    }

    IEnumerator WaitAction(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);
        action();
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

    void EndTutorial()
    {
        tutorialScreen.SetActive(true);
        tutorialScreen.transform.GetChild(0).GetComponent<Text>().text = textFinal;
        button.SetActive(true);
        Destroy(this);
    }


    void GenerateEnemyWave()
    {
        Transform[] points = pmt.movement.positions;
        enemies = new Enemy[3];
        for (int i = 0; i < 3; i++)
        {
            enemies[i] = Instantiate(enemyPrefab, new Vector2(points[i].position.x, points[i].position.y + distance), Quaternion.identity).GetComponent<Enemy>();
        }
    }

}
