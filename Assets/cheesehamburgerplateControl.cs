using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheesehamburgerplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cheesehamburgerplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "cheesehamburger";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
