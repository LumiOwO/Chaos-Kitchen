using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskcheeseControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "cheesedesk";
        block.type = "desk";
        block.content = "cheese";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
