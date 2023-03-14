using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trashpotControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "trashpot";
        block.type = "container";
        block.plustype = "pot";
        block.foodincontainer = "trash";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
