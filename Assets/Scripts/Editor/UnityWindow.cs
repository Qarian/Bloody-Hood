using UnityEditor;
using UnityEngine;

public class UnityWindow : EditorWindow {

    #region Variables
    #region Create
    bool createBool;
    bool showEnemy;
    bool showLevel;
    bool showBoss;

    static string enemyName = "Enemy";
    static string levelName = "Level";
    static string bossName = "Boss";
    #endregion
    #region Preview
    bool previewBool;
    bool show;
    static bool previewActive;
    bool previewActiveBool;

    static LevelScript level;
    static int numberOfEnemies = 15;
    static float firstRowHeight = 10f;
    static float distance = 2f;
    static float width = 3f;
    static GameObject previewGO;
    static float height;

    static string previewSaveName = "Preview Save";
    static Wave[] waves;
    #endregion
    #region PlayerPref
    bool playerPrefsFoldout;
    #endregion
    #endregion

    [MenuItem("Editor/Window")]
    public static void ShowWindow()
    {
        GetWindow<UnityWindow>("Editor");
    }

    private void OnGUI()
    {
        createBool = EditorGUILayout.Foldout(createBool, "Create new");
        if (createBool)
        {
            EditorGUI.indentLevel++;
            
            showEnemy = EditorGUILayout.Foldout(showEnemy, "Enemy");
            if (showEnemy)
            {
                EditorGUI.indentLevel++;
                enemyName = EditorGUILayout.TextField("Name: ", enemyName);
                if (Button(EditorGUI.indentLevel, "Create " + enemyName))
                {
                    CreateEnemyPrefab();
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

            showLevel = EditorGUILayout.Foldout(showLevel, "Level");
            if(showLevel)
            {
                EditorGUI.indentLevel++;
                levelName = EditorGUILayout.TextField("Name: ", levelName);
                if (Button(EditorGUI.indentLevel, "Create " + levelName))
                {
                    CreateLevel();
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }
            
            showBoss = EditorGUILayout.Foldout(showBoss, "Boss");
            if (showBoss)
            {
                EditorGUI.indentLevel++;
                bossName = EditorGUILayout.TextField("Name: ", bossName);
                if (Button(EditorGUI.indentLevel, "Create " + bossName))
                {
                    CreateBossPrefab();
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Space();

        previewBool = EditorGUILayout.Foldout(previewBool, "Preview");
        if (previewBool)
        {
            EditorGUI.indentLevel++;

            level = (LevelScript)EditorGUILayout.ObjectField("Level", level, typeof(LevelScript), false);

            numberOfEnemies = EditorGUILayout.IntField("Number: ", numberOfEnemies);
            distance = EditorGUILayout.FloatField("Distance: ", distance);

            if(level != null)
            {
                if (Button(EditorGUI.indentLevel, "Create new preview"))
                {
                    CreatePreview();
                }
            }

            if (previewActive)
            {
                previewActiveBool = EditorGUILayout.Foldout(previewActiveBool, "Active Preview");
                if (previewActiveBool)
                {
                    EditorGUI.indentLevel++;
                    if (Button(EditorGUI.indentLevel, "Delete preview"))
                    {
                        DeletePreview();
                    }

                    if(level.spawnChoice != SpawnChoice.LoadFromFile)
                    {
                        if (Button(EditorGUI.indentLevel, "Save preview"))
                        {
                            SavePreview();
                        }
                    }
                    EditorGUI.indentLevel--;
                }
            }

            EditorGUI.indentLevel--;
        }
        EditorGUILayout.Space();

        playerPrefsFoldout = EditorGUILayout.Foldout(playerPrefsFoldout, "PlayerPrefs");
        if (playerPrefsFoldout)
        {
            EditorGUI.indentLevel++;
            if (Button(EditorGUI.indentLevel, "Delete all PlayerPrefs"))
            {
                PlayerPrefs.DeleteAll();
            }

            if (Button(EditorGUI.indentLevel, "Set start PlayerPrefs"))
            {
                MenuScript.SetPlayerPrefs();
            }
            EditorGUI.indentLevel--;
        }
    }
    
    static bool Button(int indentLevel, string name)
    {
        bool ret;
        GUILayout.BeginHorizontal();
        GUILayout.Space(indentLevel * 18);
        ret = GUILayout.Button(name);
        GUILayout.EndHorizontal();
        return ret;
    }

    #region Functions
    #region Create
    static void CreateBossPrefab()
    {
        string localPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefab/Boss/" + bossName + ".prefab");

        GameObject go = new GameObject();
        go.name = bossName;
        go.tag = "Boss";
        go.layer = 10;
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>().size = new Vector2(3f, 3.7f);
        go.AddComponent<Rigidbody2D>().gravityScale = 0f;
        go.AddComponent<Boss>();
        go.AddComponent<BossMovement>();

        EditorUtility.FocusProjectWindow();
        CreateNew(go, localPath);
        DestroyImmediate(go);
        Selection.activeObject = AssetDatabase.LoadAssetAtPath<GameObject>(localPath);
    }

    static void CreateEnemyPrefab()
    {
        string localPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefab/Enemy/" + enemyName + ".prefab");

        GameObject go = new GameObject();
        go.name = enemyName;
        go.tag = "Enemy";
        go.layer = 10;
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>().size = new Vector2(1.15f, 2.3f);
        go.AddComponent<Rigidbody2D>().gravityScale = 0f;
        go.AddComponent<Enemy>().title = enemyName;
        go.AddComponent<EM_Standard>();


        #region CzyNadpisac zakomentowane
        /*
        if (AssetDatabase.LoadAssetAtPath(localPath, typeof(GameObject)))
        {
            if (EditorUtility.DisplayDialog("Are you sure?",
                    "The prefab already exists. Do you want to overwrite it?",
                    "Yes",
                    "No"))
            {
                CreateNew(gameObject, localPath);
            }
        }
        */
        #endregion
        CreateNew(go, localPath);
        DestroyImmediate(go);
        EditorUtility.FocusProjectWindow();
    }

    static void CreateNew(GameObject obj, string localPath)
    {
        Object prefab = PrefabUtility.CreatePrefab(localPath, obj);
        PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }
    
    static void CreateLevel()
    {
        LevelScript asset = CreateInstance<LevelScript>();

        //string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Levels/" + levelName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
    #endregion
    #region Preview
    static void CreatePreview()
    {
        if(previewGO != null)
            DestroyImmediate(previewGO);

        previewGO = new GameObject("Preview");
        if (numberOfEnemies <= 0)
            waves = new Wave[level.waveCount];
        else
            waves = new Wave[numberOfEnemies];

        height = firstRowHeight;

        switch ((int)level.spawnChoice)
        {
            case 0:
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    Construct(Random.Range(-1, 2), 0, i);
                }
                break;
            case 1:
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    Construct(Random.Range(-1, 2), Random.Range(0,level.enemies.Length), i);
                }
                break;
            case 3:
                for (int i = 0; i < numberOfEnemies; i++)
                {
                    Construct(level.levelEnemies.waves[i].row - 1, level.levelEnemies.waves[i].enemyId, i, level.levelEnemies.waves[i].time);
                }
                break;
            default:
                break;
        }
        previewActive = true;
    }

    static void Construct(int x, int enemyId, int number, float t = 0)
    {
        waves[number].row = x + 1;
        waves[number].enemyId = enemyId;
        float time = level.enemies[enemyId].GetComponent<Enemy>().timeAfterSpawn;
        waves[number].time = time;

        GameObject go = new GameObject(level.enemies[enemyId].name);
        go.AddComponent<SpriteRenderer>().sprite = level.enemies[enemyId].GetComponent<SpriteRenderer>().sprite;
        go.transform.Translate(new Vector2(x * width, height));
        go.transform.parent = previewGO.transform;
        if (t == 0)
            height += time * distance;
        else
            height += t * distance;
    }

    static void DeletePreview()
    {
        waves = null;
        DestroyImmediate(previewGO);
        previewActive = false;
    }

    static void SavePreview()
    {
        LevelEnemies levelEnemies = CreateInstance<LevelEnemies>();
        levelEnemies.waves = waves;

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Level Enemies/" + previewSaveName + ".asset");

        AssetDatabase.CreateAsset(levelEnemies, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = levelEnemies;
    }

    #endregion
    #endregion

    /* Sprawdza czy jest cos zaznaczone i dopiero wtedy uruchamia MenuItem
    [MenuItem("Examples/Create Prefab", true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null;
    }
    */
    /* Example
    GUILayout.Label("Description");
    string text = "123";
    text = EditorGUILayout.TextField(text);
    if(GUILayout.Button("Do something"))
    {
        //Function
    }
    */
}
