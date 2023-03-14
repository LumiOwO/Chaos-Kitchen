using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lettuceslicehamburgerplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "lettuceslicehamburgerplate";
        block.type = "container";
        block.plustype = "plate";
        block.foodincontainer = "lettuceslicehamburger";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
