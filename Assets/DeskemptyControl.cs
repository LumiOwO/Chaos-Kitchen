using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskemptyControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "emptydesk";
        block.type = "desk";
        block.content = "";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
