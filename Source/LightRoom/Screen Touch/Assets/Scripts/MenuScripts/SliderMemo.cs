using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SliderMemo : MonoBehaviour {

    public Slider slider;
	
	void Start () {
        slider.value = PlayerPrefs.GetFloat("VolumeMemory", 1f);
    }
}
