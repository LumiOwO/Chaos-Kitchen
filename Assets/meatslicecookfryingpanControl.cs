using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meatslicecookfryingpanControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "meatslicecookfryingpan";
        block.type = "container";
        block.plustype = "fryingpan";
        block.foodincontainer = "meatslicecook";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
