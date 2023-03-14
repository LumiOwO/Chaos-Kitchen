using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseslicericecookplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cheeseslicericecookplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "cheeseslicericecook";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
