using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomatoslicebunplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "tomatoslicebunplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "tomatoslicebun";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
