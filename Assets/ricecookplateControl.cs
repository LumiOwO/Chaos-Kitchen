using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ricecookplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "ricecookplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "ricecook";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
