using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseslicemeatslicecookplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cheeseslicemeatslicecookplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "cheeseslicemeatslicecook";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
