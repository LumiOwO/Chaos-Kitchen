using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskcutControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cutdesk";
        block.type = "desk";
        block.currentCutTime = -1;
        block.cutTimeNeeded = -1;
        GameObject timebar = GameObject.Find("TimerBarCanvas");
        timebar.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
