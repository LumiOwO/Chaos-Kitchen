using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tomatohamburgerplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "tomatohamburgerplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "tomatohamburger";

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
