using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meatslicepanControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "fryingpan";
        block.type = "container";
        block.plustype = "fryingpan";
        //block.foodincontainer = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
