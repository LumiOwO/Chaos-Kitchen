using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashfryingpanControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "trashfryingpan";
        block.type = "container";
        block.plustype = "fryingpan";
        block.foodincontainer = "trash";
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
