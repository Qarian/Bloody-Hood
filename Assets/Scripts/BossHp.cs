using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour {

    Slider slider;

    int maxHp;

	public void Begin(int hp) {
        slider = GetComponent<Slider>();
        maxHp = hp;
        slider.maxValue = maxHp;
        slider.value = maxHp;
	}

    public void NewHp(int hp)
    {
        slider.value = hp;
    }

}
