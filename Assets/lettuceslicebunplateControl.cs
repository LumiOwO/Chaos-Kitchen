using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lettuceslicebunplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "lettuceslicebunplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "lettuceslicebun";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
