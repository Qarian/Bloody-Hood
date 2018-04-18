using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour {

    Slider slider;

    float maxHp;

	public void Begin(float hp) {
        slider = GetComponent<Slider>();
        maxHp = hp;
        slider.maxValue = maxHp;
        slider.value = maxHp;
	}

    public void NewHp(float hp)
    {
        slider.value = hp;
    }

}
