using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomatoslicericecookplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "tomatoslicericecookplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "tomatoslicericecook";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
