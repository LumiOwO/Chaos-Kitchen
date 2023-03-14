using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktomatoControl : LogicControl
{
    // Start is called before the first frame update
    void Start()
    {
        block.name = "tomatodesk";
        block.type = "desk";
        block.content = "tomato";
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
