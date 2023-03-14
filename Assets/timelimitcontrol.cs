using UnityEngine;
using UnityEngine.UI;

public class timelimitcontrol : MonoBehaviour
{
    Slider slider;
    float _time = 0;

    public void SetTime(float t) {
        slider.maxValue = t;
        slider.value = 0;
    }

    public void SetCurrentTime(float t) {
        slider.value = t;
    }

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        SetTime(_time);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _time -= Time.deltaTime;
        SetCurrentTime(_time);
    }
}
