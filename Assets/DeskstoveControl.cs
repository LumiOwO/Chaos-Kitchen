using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskstoveControl : LogicControl
{
    // Start is called before the first frame update
    public AudioSource fireAudio;
    public AudioSource didiAudio;
    void Start()
    {
        block.name = "stovedesk";
        block.type = "desk";
        
        GameObject timebar = GameObject.Find("TimerBarCanvas");
        timebar.SetActive(false);
        fireAudio.Stop();
        didiAudio.Stop();
    }

    // Update is called once per frame
    void Update()
    {
    }
}
