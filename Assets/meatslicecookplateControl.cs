using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meatslicecookplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "meatslicecookplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "meatslicecook";

        //block.currentTime = -1.0F;
        //block.needTime = -1.0F;
        //block.deal = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
