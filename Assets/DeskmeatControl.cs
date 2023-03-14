using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskmeatControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "meatdesk";
        block.type = "desk";
        block.content = "meat";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
