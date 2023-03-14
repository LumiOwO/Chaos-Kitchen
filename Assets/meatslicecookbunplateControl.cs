using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meatslicecookbunplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "meatslicecookbunplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "meatslicecookbun";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
