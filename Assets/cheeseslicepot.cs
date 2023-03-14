using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheeseslicepot : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cheeseslicepot";
        block.type = "container";
        block.plustype = "pot";
        block.foodincontainer = "cheeseslice";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
