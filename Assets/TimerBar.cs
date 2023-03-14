using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour {
    public Slider slider;

    public void SetTime(float t) {
        slider.maxValue = t;
        slider.value = 0;
    }

    public void SetCurrentTime(float t) {
        slider.value = t;
    }

    // Start is called before the first frame update
    void Start() {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update() {
        // if (isCounting) {
        //     Debug.Log("TimerBar: counting");
        //     _time += Time.deltaTime;
        //     slider.value = _time;
        //     
        //     if (_time > slider.maxValue) {
        //         isCounting = false;
        //         // Do other work
        //         Debug.Log("TimerBar: time's up");
        //     }
        // }
    }
}
