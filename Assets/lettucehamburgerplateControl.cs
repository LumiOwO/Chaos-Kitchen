using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lettucehamburgerplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "lettucehamburgerplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "lettucehamburger";

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
