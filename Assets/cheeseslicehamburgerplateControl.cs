using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseslicehamburgerplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cheeseslicehamburgerplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "cheeseslicehamburger";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
