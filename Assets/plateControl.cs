using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "plate";
        block.type = "container";
        block.plustype = "plate";
        //block.foodincontainer = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
