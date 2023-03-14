using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheesesliceplalteControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cheesesliceplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "cheeseslice";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
