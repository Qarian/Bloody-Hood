using UnityEditor;
using UnityEngine;

public class UnityWindow : EditorWindow {

    static string enemyName = "Enemy";
    static string levelName = "Level";
    static string bossName = "Boss";
    bool createBool = false;

    bool showEnemy;
    bool showLevel;
    bool showBoss;

    [MenuItem("Editor/Window")]
    public static void ShowWindow()
    {
        GetWindow<UnityWindow>("Editor");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Create"))
        {
            createBool = !createBool;
        }

        EditorGUI.indentLevel++;
        if (createBool)
        {
            
            showEnemy = EditorGUILayout.Foldout(showEnemy, "Enemy");
            //EditorGUILayout.LabelField("Enemy");
            if (showEnemy)
            {
                EditorGUI.indentLevel++;
                enemyName = EditorGUILayout.TextField("Name:", enemyName);
                GUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * 18);
                if (GUILayout.Button("Create " + enemyName))
                {
                    CreateEnemyPrefab();
                }
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

            
            showLevel = EditorGUILayout.Foldout(showLevel, "Level");
            //EditorGUILayout.LabelField("Level");
            if(showLevel)
            {
                EditorGUI.indentLevel++;
                levelName = EditorGUILayout.TextField("Name:", levelName);
                GUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * 18);
                if (GUILayout.Button("Create " + levelName))
                {
                    CreateAsset<LevelScript>();
                }
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

            
            showBoss = EditorGUILayout.Foldout(showBoss, "Boss");
            //EditorGUILayout.LabelField("Enemy");
            if (showBoss)
            {
                EditorGUI.indentLevel++;
                bossName = EditorGUILayout.TextField("Name:", bossName);
                GUILayout.BeginHorizontal();
                GUILayout.Space(EditorGUI.indentLevel * 18);
                if (GUILayout.Button("Create " + bossName))
                {
                    CreateBossPrefab();
                }
                GUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }
        }
        EditorGUI.indentLevel--;
        EditorGUILayout.Space();

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

    static void CreateBossPrefab()
    {
        string localPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefab/Boss/" + bossName + ".prefab");

        GameObject go = new GameObject();
        go.name = bossName;
        go.tag = "Boss";
        go.layer = 8;
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>().size = new Vector2(3f, 3.7f);
        go.AddComponent<Rigidbody2D>().gravityScale = 0f;
        go.AddComponent<Boss>();
        go.AddComponent<BossMovement>();

        EditorUtility.FocusProjectWindow();
        CreateNew(go, localPath);
        DestroyImmediate(go);
    }

    static void CreateEnemyPrefab()
    {
        string localPath = AssetDatabase.GenerateUniqueAssetPath("Assets/Prefab/Enemy/" + enemyName + ".prefab");

        GameObject go = new GameObject();
        go.name = enemyName;
        go.tag = "Enemy";
        go.layer = 8;
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<BoxCollider2D>().size = new Vector2(1.15f, 2.3f);
        go.AddComponent<Rigidbody2D>().gravityScale = 0f;
        go.AddComponent<Enemy>();


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
        EditorUtility.FocusProjectWindow();
        CreateNew(go, localPath);
        DestroyImmediate(go);
    }

    /* Sprawdza czy jest cos zaznaczone i dopiero wtedy uruchamia MenuItem
    [MenuItem("Examples/Create Prefab", true)]
    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null;
    }
    */

    static void CreateNew(GameObject obj, string localPath)
    {
        Object prefab = PrefabUtility.CreatePrefab(localPath, obj);
        PrefabUtility.ReplacePrefab(obj, prefab, ReplacePrefabOptions.ConnectToPrefab);
    }

    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = CreateInstance<T>();

        //string path = AssetDatabase.GetAssetPath(Selection.activeObject);

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath("Assets/Levels/" + levelName + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
