using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseslicebunplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cheeseslicebunplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "cheeseslicebun";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
