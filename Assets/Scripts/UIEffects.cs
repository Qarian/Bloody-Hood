using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIEffects : MonoBehaviour {

    #region Blood Effect
    public float bloodEffectInterval = 0.1f;
    public float bloodEffectdistance = 20f;
    public float bloodEffectAcceleration;
    public Vector2 bloodEffectSize;
    public Sprite bloodEffectSprite;
    public Transform bloodEffectTarget;
    #endregion

    #region Singleton
    public static UIEffects singleton;

    void Awake()
    {
        singleton = this;
    }
    #endregion
    
    public void GenerateBloodEffect(Vector3 pos, int count)
    {
        StartCoroutine(SpawnBloodEffect(pos, count));
    }
    
    public IEnumerator SpawnBloodEffect(Vector3 pos, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(pos.x + Random.Range(-bloodEffectdistance, bloodEffectdistance), pos.y + Random.Range(-bloodEffectdistance, bloodEffectdistance));
            GameObject go = new GameObject("Blood");
            go.transform.parent = transform;
            go.transform.position = position;
            go.AddComponent<Image>().sprite = bloodEffectSprite;
            go.GetComponent<RectTransform>().sizeDelta = bloodEffectSize;
            BloodEffect be = go.AddComponent<BloodEffect>();
            be.target = bloodEffectTarget;
            be.acceleration = bloodEffectAcceleration;
            yield return new WaitForSeconds(bloodEffectInterval);
        }
    }

}
