using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fryingpanControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "fryingpan";
        block.type = "container";
        block.plustype = "fryingpan";
        //block.foodincontainer = "";

        //block.currentTime = -1.0F;
        //block.needTime = -1.0F;
        //block.deal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
