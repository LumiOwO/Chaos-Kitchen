using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lettuceslicericecookplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "lettuceslicericecookplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "lettuceslicericecook";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
