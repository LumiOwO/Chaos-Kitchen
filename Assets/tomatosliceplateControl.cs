using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomatosliceplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "tomatosliceplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "tomatoslice";

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
