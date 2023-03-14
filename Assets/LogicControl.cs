using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicControl : MonoBehaviour
{
    // Start is called before the first frame update
    public class Message
    {
        public string name;
        public string type;
        public string plustype;
        public string content;
        public GameObject ondesk;

        public string foodincontainer;
        public float currentCutTime;
        public float cutTimeNeeded;

        public string afterdealname;
        public bool deal;
        public bool needprocess;
        public float currentTime;
        public float needTime;


        // 着火后冷却时间
        public float cooldownTime;
        public bool overcooked = false;
        public GameObject fire;


    };
    public Message block = new Message();
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
