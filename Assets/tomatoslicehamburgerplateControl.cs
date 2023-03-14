using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomatoslicehamburgerplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "tomatoslicehamburgerplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "tomatoslicehamburger";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
