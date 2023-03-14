using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskplateControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "platedesk";
        block.type = "desk";
        block.content = "plate";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
