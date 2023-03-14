using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SimpleSampleCharacterControl : MonoBehaviour
{
    public int PAUSE = 0;
    public bool INCUT = false;
    public bool LASCUT = false;
    const double eps = 1e-8F;
    double pi = System.Math.Acos(-1.0F);
    bool pushwater = false;
    bool LASWATER = false;
    private const int TESTLEVEL = 0;
    private GameObject water;

    List<GameObject> selection = new List<GameObject>();
    string[] selectTag = new string[]{"Desk", "Food", "stovedesk","coolfire"};
    GameObject[] desks;

    Transform last_trasform;
    GameObject last_Obj;
    GameObject currentselectObj;
    GameObject handObj;
    //bool FirsttimeTag = false; 
    GameObject[] frypots;
    GameObject[] stovedesks;

    private Vector3 _pos, _velocity;

    public AudioSource cutAudio;
    public AudioSource waterAudio;
    public AudioSource submitAudio;

    public class FoodMessage
    {

        public bool combine_check(string food1, string food2)
        {
            bool tag = true;
            if(food1 == null || food2 == null)return true;
            if(food1 == "" || food2 == "")return true;
            if (food1 == food2)
            {
                //两个food相同不能合并
                tag = false;
                return false;
            }
            else
            {
                switch (food1)
                {
                    case "bun":
                        if (food2 == "cheeseslice" || food2 == "meatslicecook" || food2 == "tomatoslice"
                            || food2 == "lettuceslice" || food2 == "cheeseslicemeatslicecook"
                            || food2 == "lettuceslicemeatclicecook" || food2 == "tomatoslicemeatclicecook"
                            )
                        {
                            //能和bun合并的情况
                            tag = true;
                        }
                        else
                        {
                            tag = false;
                        }
                        break;
                    case "cheese":
                    case "meat":
                    case "tomato":
                    case "lettuce":
                    case "meatslice":
                    case "rice":
                        //未切的raw类和没有cook的肉不能用
                        tag = false;
                        break;
                    case "cheeseslice":
                    case "tomatoslice":
                    case "lettuceslice":
                        //蔬菜切片可以和bun和煮过的肉、米饭合并
                        if (food2 == "bun" || food2 == "meatslicecookbun" || food2 == "meatslicecook" || food2 == "ricecook")
                        {
                            tag = true;
                        }
                        break;
                    case "meatslicecook":
                        //煮过的肉可以和菜或者bun合并
                        if (food2 == "bun" || food2 == "cheeseslice"
                            || food2 == "tomatoslice " || food2 == "lettuceslice"
                            || food2 == "cheeseslicebun"
                            || food2 == "tomatoslicebun" || food2 == "lettuceslicebun")
                        {
                            tag = true;
                        }
                        break;
                    case "ricecook":
                        //煮过的米饭可以和XXXslice合并
                        if ( food2 == "cheeseslice"
                           || food2 == "tomatoslice " || food2 == "lettuceslice")
                        {
                            tag = true;
                        }
                        break;
                    case "meatslicecookbun":
                        if (food2 == "cheeseslice"
                           || food2 == "tomatoslice " || food2 == "lettuceslice")
                        {
                            tag = true;
                        }
                        break;
                    case "cheeseslicemeatclicecook":
                    case "lettuceslicemeatclicecook":
                    case "tomatoslicemeatclicecook":
                        //肉+菜可以和bun合并
                        if (food2 == "bun")
                        {
                            tag = true;
                        }
                        break;
                    case "cheeseslicebun":
                    case "lettuceslicebun":
                    case "tomatoslicebun":
                        if (food2 == "meatslicecook")
                        {
                            tag = true;
                        }
                        break;
                    default:
                        tag = false;
                        break;
                }
            }

            return tag;
        }
        public string combinefood(string food1, string food2)
        {
            Debug.Log("combine : "+ food1 +" | " + food2);
            if(food1 == null)return food2;
            if(food2 == null)return food1;
            if(food1 == "" || food2 == "")return food1+food2;
            string newfood = "";
            switch (food1)
            {
                case "":
                    newfood = food2;
                    break;
                case "bun":
                    //XXXXbun格式 
                    newfood = food2 + food1;
                    break;
                case "cheeseslice":
                case "tomatoslice":
                case "lettuceslice":
                    //4种情况 XXXslicebun / XXXXmeatslicecookbun / XXXXmeatslicecook
                    //XXXslicericecook
                    newfood = food1 + food2;
                    break;
                case "meatslicecook":
                    if (food2 == "bun")
                    {
                        //meatslicecookbun
                        newfood = food1 + food2;
                    }
                    else if (food2 == "cheeseslicebun")
                    {
                         newfood = "cheesehamburger";
                    }
                       
                   else if (food2 == "tomatoslicebun")
                    {
                        newfood = "tomatohamburger";
                    }
                    else if (food2 == "lettuceslicebun")
                    {
                        newfood = "lettucehamburger";
                    }
                    else
                    {
                        //XXXslice+meatslicecook
                        newfood = food2 + food1;
                    }
                    break;
                case "ricecook":
                    newfood = food2 + food1;
                    break;
                case "meatslicecookbun":
                    if (food2 == "cheeseslice")
                    {
                        newfood = "cheesehamburger";
                    }
                    if (food2 == "lettuceslice")
                    {
                        newfood = "lettucehamburger";
                    }
                    if (food2 == "tomatoslice")
                    {
                        newfood = "tomatohamburger";
                    }
                    break;
                //以下三种只能与bun合并，XXXslicemeatslicecookbun返回XXXhamburger
                case "cheeseslicemeatclicecook":
                    newfood = "cheesehamburger";
                    break;
                case "lettuceslicemeatclicecook":
                    newfood = "lettucehamburger";
                    break;
                case "tomatoslicemeatclicecook":
                    newfood = "tomatohamburger";
                    break;
                //以下三种只能与meatslicecook组合，直接返回hamburger
                case "cheeseslicebun":
                    newfood = "cheesehamburger";
                    break;
                case "lettuceslicebun":
                    newfood = "lettucehamburger";
                    break;
                case "tomatoslicebun":
                    newfood = "tomatohamburger";
                    break;
                default:
                    Debug.Log("!!!!!food combine error!!!!!");
                    break;
            }

            return newfood;
        }

        public bool cutable(string food)
        {
            bool tag = false;
            if(food == "cheese" || food == "tomato" || food == "lettuce" || food == "meat") tag = true;
            return tag;
        }

        public float cuttime(string food)
        {
            float needtime = 2.5F;
            return needtime;
        }
        public string aftercutname(string food)
        {
            string newfoodname ="";
            if(food == "cheese")newfoodname = "cheeseslice";
            else if(food == "tomato")newfoodname = "tomatoslice";
            else if(food == "lettuce")newfoodname = "lettuceslice";
            else if (food == "meat") newfoodname = "meatslice";
            return newfoodname;
        }

        public bool dealable(string food,string plustype)
        {
            bool tag = false;
            if(food == "trash")tag = false;
            if(food == "rice" && plustype == "pot")tag = true;
            if(food == "meatslice" && plustype == "fryingpan")tag = true;
            return tag;
        }

        public float dealtime(string food,string plustype)
        {
            float needtime = 10.0F;
            return needtime;
        }
        public string afterdealname(string food,string plustype)
        {
            string newfoodname = "";
            if (food == "meatslice"&&plustype == "fryingpan")
            {
                newfoodname = "meatslicecook";
            }
            else if(food=="rice")//煮饭锅的plustype？？
            {
                newfoodname = "ricecook";
            }
            else newfoodname = "cheeseslice";
            return newfoodname;
        }

    };

    public FoodMessage foodmanage = new FoodMessage();

    void Start()
    {
        Debug.Log("hello world - start!");

        /*绑定frying pan 和 stovedesk*/

        //cutAudio.playOnAwake = false;
        cutAudio.Stop();
        waterAudio.Stop();
        submitAudio.Stop();

        frypots = GameObject.FindGameObjectsWithTag("frypot");
        stovedesks = GameObject.FindGameObjectsWithTag("stovedesk");

        Debug.Log("fry stove number:! "+frypots.Length + stovedesks.Length);
        foreach(var frypotsObj in frypots)
        {
            foreach(var stovedeskObj in stovedesks)
            {
                if(Vector3.Distance(frypotsObj.transform.position,stovedeskObj.transform.position) < 1.0F)
                {
                    LogicControl stoveLogic = stovedeskObj.GetComponentInChildren<LogicControl>();
                    stoveLogic.block.ondesk = frypotsObj;
                    Debug.Log("Match pan-stove: " + frypotsObj.transform.position + " " +stovedeskObj.transform.position);
                    break;
                }
            }
        }
        int typeNum = selectTag.Length;

        foreach(var tagname in selectTag)
        {
            desks = GameObject.FindGameObjectsWithTag(tagname);
            foreach(var item in desks)
            {
                selection.Add(item);
            }    
        }
        Debug.Log("total item number : " + selection.Count);
        //FirsttimeTag = true;
    }

    public bool checkSelect(Transform trans,Vector3 pos)
    {
        bool tag = false;
        if(Vector3.Distance(trans.position,pos) < 1.5F)
        {
            /*Vector3 mid = trans.position;
            Vector3 dlt = pos - mid;
            double tangle;
            if(System.Math.Abs(dlt.x)<eps)
            {
                if(dlt.z>0)tangle = 0F;
                else tangle = -180.0F;
            }   
            else
            {
                double aco = System.Math.Acos(dlt.z/dlt.x);
                tangle = aco * 180.0 / pi;
                tangle = -tangle + 90.0F;
            }*/
            tag = true;
        }
        return tag;
    }

    public GameObject findselectObj(Transform trans)
    {
        GameObject res = null;
        bool find_tag = false;
        double mindis = 1e8F;
        GameObject mindisObj = null;
        foreach(var Obj in selection)
        {
            if(Obj == null)continue;
            if(Obj == handObj)continue;
            Vector3 Objposition = Obj.transform.position;
            if(Vector3.Distance(trans.position,Objposition)<mindis)
            {
                mindis = Vector3.Distance(trans.position,Objposition);
                mindisObj = Obj;
            }
            /*if(checkSelect(trans,Objposition))
            {
                res = Obj;
                find_tag = true;
                break;
            }*/
        }
        if(mindis < 1.65F)
        {
            find_tag = true;
            res = mindisObj;
        }
        if(find_tag)return res;
        else return null;
    } 

    public void highlight(GameObject obj,bool tag)
    {
        foreach (var renderers in obj.GetComponentsInChildren<Renderer>()) {
            if (tag) {
                renderers.material.color += new Color(0.3f, 0.3f, 0.3f);
            } else if (!tag) {
                renderers.material.color -= new Color(0.3f, 0.3f, 0.3f);
            }
        }
    }

    public void updateHighlight()
    {
        Transform now_trasform = transform;
        /*if(!FirsttimeTag)
        {   
            GameObject pre = findselectObj(last_trasform);
            if(pre != null)highlight(pre,false);
        }*/
        if(last_Obj != null)highlight(last_Obj,false);
        GameObject now = findselectObj(now_trasform);
        currentselectObj = now;
        if(now != null)highlight(now,true);
        last_Obj = now;
        last_trasform = transform;
        //FirsttimeTag = false;
    }

    public void Initialize(GameObject character)
    {
        m_animator = character.GetComponent<Animator>();
        m_rigidBody = character.GetComponent<Rigidbody>();
    }

    private enum ControlMode
    {
        /// <summary>
        /// Up moves the character forward, left and right turn the character gradually and down moves the character backwards
        /// </summary>
        Tank,
        /// <summary>
        /// Character freely moves in the chosen direction from the perspective of the camera
        /// </summary>
        Direct,
    }
    

    [SerializeField] private float m_moveSpeed = 2;
    [SerializeField] private float m_turnSpeed = 200;
    [SerializeField] private float m_jumpForce = 1F;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody m_rigidBody;

    [SerializeField] private ControlMode m_controlMode = ControlMode.Tank;

    private float m_currentV = 0;
    private float m_currentH = 0;

    private readonly float m_interpolation = 10;
    private readonly float m_walkScale = 0.33f;
    private readonly float m_backwardsWalkScale = 0.16f;
    private readonly float m_backwardRunScale = 0.66f;

    private bool m_wasGrounded;
    private Vector3 m_currentDirection = Vector3.zero;

    private float m_jumpTimeStamp = 0;
    private float m_minJumpInterval = 0.25f;

    private bool m_isGrounded;
    
    private List<Collider> m_collisions = new List<Collider>();

    void Awake()
    {
        if(!m_animator) { gameObject.GetComponent<Animator>(); }
        if(!m_rigidBody) { gameObject.GetComponent<Animator>(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        for(int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                if (!m_collisions.Contains(collision.collider)) {
                    m_collisions.Add(collision.collider);
                }
                m_isGrounded = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        ContactPoint[] contactPoints = collision.contacts;
        bool validSurfaceNormal = false;
        for (int i = 0; i < contactPoints.Length; i++)
        {
            if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5f)
            {
                validSurfaceNormal = true; break;
            }
        }

        if(validSurfaceNormal)
        {
            m_isGrounded = true;
            if (!m_collisions.Contains(collision.collider))
            {
                m_collisions.Add(collision.collider);
            }
        } else
        {
            if (m_collisions.Contains(collision.collider))
            {
                m_collisions.Remove(collision.collider);
            }
            if (m_collisions.Count == 0) { m_isGrounded = false; }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(m_collisions.Contains(collision.collider))
        {
            m_collisions.Remove(collision.collider);
        }
        if (m_collisions.Count == 0) { m_isGrounded = false; }
    }

	void FixedUpdate ()
    {
        if(PAUSE == 1 )return;
        m_animator.SetBool("Grounded", m_isGrounded);

        ParticleUpdate();
        switch(m_controlMode)
        {
            case ControlMode.Direct:
                DirectUpdate();
                break;

            case ControlMode.Tank:
                TankUpdate();
                break;
            default:
                Debug.LogError("Unsupported state in FixedUpdate.");
                break;
        }

        m_wasGrounded = m_isGrounded;
        
        updateHighlight();

        // return the x y face 
    }
    private void AudioUpdate()//Audio
    {
        //Debug.Log(INCUT);

        if(LASCUT != INCUT)
        {
            if(INCUT == true)cutAudio.Play();
            else cutAudio.Stop();
        }
        LASCUT = INCUT;

        if(LASWATER != pushwater)
        {
            if(pushwater == true)waterAudio.Play();
            else waterAudio.Stop();
        }
        LASWATER = pushwater;

        //else cutAudio.Stop();
    }
    private void Update() {

        if(PAUSE == 1 )return;
        LogicUpdate();
        AudioUpdate();
    }

    private void BindObj(GameObject Obj)
    {
        if(Obj == null)return;
        Vector3 dlty = new Vector3(0F,0.65F,0F);
        double y = transform.localEulerAngles.y/180.0*pi;
        Vector3 dltR = new Vector3((float)System.Math.Sin(y),0F,(float)System.Math.Cos(y));
        dltR *= 0.5F;
        Obj.transform.position = transform.position + dlty + dltR;
    }

    private void handlecut()
    {
        LogicControl logic;
        if(currentselectObj == null)return;
        logic = currentselectObj.GetComponentInChildren<LogicControl>();
        if(logic == null || logic.block.name != "cutdesk")return;
        if(logic.block.ondesk == null)return;
        GameObject ondeskObj = logic.block.ondesk;
        LogicControl ondesklogic = ondeskObj.GetComponentInChildren<LogicControl>();
        if(ondesklogic.block.type != "food")return;

        string foodname = ondesklogic.block.name;
        if(foodmanage.cutable(foodname) == false)return;

        //if(ondesklogic.block.deal == true)return;

        float cutTimeNeeded = foodmanage.cuttime(foodname);
        
        //currentselectObj son
        GameObject timebarCanvas = currentselectObj.transform.Find("TimerBarCanvas").gameObject;

        timebarCanvas.SetActive(true);

        ondesklogic.block.currentCutTime += Time.deltaTime;
        INCUT = true;

        float currentcutTime = ondesklogic.block.currentCutTime;

        TimerBar timerbarComponent = logic.GetComponentInChildren<TimerBar>();

        timerbarComponent.SetTime(cutTimeNeeded);
        timerbarComponent.SetCurrentTime(currentcutTime);

        /*
        lhh cut time ++
        */
        if(currentcutTime >= cutTimeNeeded)
        {
            /*
            食物变更 新建Obj
            */
            string newfoodname = foodmanage.aftercutname(foodname);

            Vector3 newpos = new Vector3(0F,1.0F,0F);
            newpos = newpos + currentselectObj.transform.position;

            //Debug.Log("new obj name: " + newfoodname + " new pos: " + newpos);

            GameObject newfoodObj = (GameObject)Instantiate(Resources.Load(newfoodname), newpos, transform.rotation);
            LogicControl newfoodLogic = newfoodObj.GetComponentInChildren<LogicControl>();
            
            //这东西会被丢地上 需add in selection
            selection.Add(newfoodObj);


            /*
                更新图画:新建一个名字为newfoodname的物体
                删除obj*2
            */
                  
            GameObject deleteObj = ondeskObj;
            Destroy(deleteObj);
            logic.block.ondesk = newfoodObj;

            timebarCanvas.SetActive(false);
        }

    }
    
    void ParticleUpdate() {
        var trans = GetComponent<Transform>();

        var position = trans.position;
        _velocity = (position - _pos) / Time.deltaTime; 
        _pos = position;
        //Debug.Log($"V: {_velocity.magnitude}");

        var particle = GetComponentInChildren<ParticleSystem>();

        if(particle == null)return;
        if (_velocity.magnitude < 0.8) {
            if (particle.isEmitting) {
                //Debug.Log($"V: stop");
                particle.Stop();
            }
        } else if(!particle.isEmitting) {
            //Debug.Log($"V: start");
            particle.Play();
        }
    }
    
    private void handlecoolfire()
    {
        if(handObj == null )return;
        LogicControl  handlogic = handObj.GetComponentInChildren<LogicControl>();
        if(handlogic.block.type != "coolfire")return;

        pushwater = true;
        
        if(water == null)
        {//启动水花效果lhh~
            water = (GameObject)Instantiate(Resources.Load("Water"),transform.position,transform.rotation);
            water.transform.parent = handObj.transform;
            water.transform.localPosition = Vector3.zero;
            var particle = water.GetComponentInChildren<ParticleSystem>();
            particle.simulationSpace = ParticleSystemSimulationSpace.World;
            particle.Stop();
            var main = particle.main;
            main.playOnAwake = false;
            particle.Play();
        }


        if(currentselectObj == null)return;
        LogicControl currentlogic = currentselectObj.GetComponentInChildren<LogicControl>();

        if(currentlogic.block.ondesk == null)return;
        GameObject ondesk = currentlogic.block.ondesk;
        LogicControl ondesklogic = ondesk.GetComponentInChildren<LogicControl>();
        if(ondesklogic.block.overcooked == false)return;

        ondesklogic.block.cooldownTime -= 3.0F * Time.deltaTime;
    }
    private void LogicUpdate() {

        LogicControl logic;
        if( TESTLEVEL >= 1 )
        {
            if(currentselectObj!=null)
            {
                logic = currentselectObj.GetComponentInChildren<LogicControl>();
                Debug.Log("states ouput:"  + logic.block.name +"  " + logic.block.type + " " + logic.block.content);

                if(logic.block.type == "desk")
                {
                    if(logic.block.ondesk == null)Debug.Log("nothing on the desk.");
                    else Debug.Log("something on the desk.");
                }
            }
            if(handObj!=null)
            {
                logic = handObj.GetComponentInChildren<LogicControl>();
                Debug.Log("hand obj output: "+ logic.block.name +"  "+logic.block.type);
            }    
        }
        
        if (handObj != null) {
            BindObj(handObj);
        }
        //if (!Input.anyKey)
        //    return;

        // Z
        if (Input.GetKeyDown(KeyCode.Z)) {
            Debug.Log("Get Z down ");
            if(currentselectObj == null) {
                Handle_SelectInvalid();
            } else if (handObj == null) {
                Handle_HandEmpty_SelectValid();
            } else {
                Handle_HandFull_SelectValid();
            }    
        }

        // X
        if (Input.GetKeyDown(KeyCode.X)) {
            Debug.Log("Get X");
        }

        // V键是不是不需要啊
        if (Input.GetKeyDown(KeyCode.V)) {
            Debug.Log("Get V");
            //if(handObj != null)
            //{
            //    handObj.transform.parent = null;
            //    GameObject deleteObj = handObj;
            //    handObj = null;
            //    Destroy(deleteObj);
            //}
        }
        pushwater = false;
        INCUT = false;
        if(Input.GetKey(KeyCode.C))
        {
            Debug.Log("Get C");
            handlecut();
            handlecoolfire();
        }
        if(pushwater == false && water != null)
        {
            Destroy(water);
        }


        //处理锅炉!
        foreach(var stovedeskObj in stovedesks) {
            Handle_Stove(stovedeskObj);
        }
    }

    Color origin_color;
    void Handle_Stove(GameObject stovedeskObj) {
        LogicControl stovedeskLogic = stovedeskObj.GetComponentInChildren<LogicControl>();
        GameObject ondesk = stovedeskLogic.block.ondesk;
        origin_color = Color.green;
        //Debug.Log("!! stove desk zhuzhuzhu 1");
        if (ondesk == null) return;

        LogicControl ondeskLogic = ondesk.GetComponentInChildren<LogicControl>();

        //if(ondesk)
        Debug.Log("zx2017");
        Debug.Log((ondeskLogic==null));
        Debug.Log("?? "+ondeskLogic.block.plustype+ " "+ ondeskLogic.block.name);
        if (ondeskLogic.block.plustype != "fryingpan" && ondeskLogic.block.plustype != "pot") return;

        //Debug.Log("!! stove desk zhuzhuzhu 2");
        if (TESTLEVEL >= 1) {
            Debug.Log(ondeskLogic.block.needprocess + " * " + ondeskLogic.block.needTime + " * " + ondeskLogic.block.deal + " ) "
            + stovedeskLogic.block.needTime + "! " + stovedeskLogic.block.deal);
        }

        //if(ondeskLogic.block.needTime < 0F)continue;
        if (ondeskLogic.block.needprocess != true) return;
        
       // Debug.Log("!! stove desk zhuzhuzhu 3");

        ////说明这东西已经煮好了!!!
        //// 但是有可能煮糊了，因此煮好了不return
        //if (ondeskLogic.block.deal == true) {
        //    return;
        //}

        //激活时间槽
        GameObject timebarCanvas = stovedeskObj.transform.Find("TimerBarCanvas").gameObject;
        timebarCanvas.SetActive(true);
        TimerBar timerbarComponent = stovedeskLogic.GetComponentInChildren<TimerBar>();

        GameObject fillobj = timerbarComponent.transform.Find("Fill").gameObject;
        Image image = fillobj.GetComponentInChildren<Image>();

        // 着火了
        const float cooldownTime = 10f;
        if (ondeskLogic.block.overcooked) {
            if (ondeskLogic.block.cooldownTime >= 10f) {
                if(TESTLEVEL >= 1)Debug.Log("Fire: Generate");
                GameObject fire = (GameObject)Instantiate(Resources.Load("Fire"));
                ondeskLogic.block.fire = fire;
            
                fire.transform.parent = stovedeskObj.transform;
                
                fire.transform.localScale = new Vector3(0.8f,0.8f,0.8f);
                fire.transform.localPosition = Vector3.zero;
                fire.transform.rotation = new Quaternion(0, 0, 0, 0);

                GameObject firesoundObj = GameObject.FindGameObjectWithTag("firesource");
                AudioSource firesound = firesoundObj.GetComponentInChildren<AudioSource>();
                firesound.Play();
                ondeskLogic.block.cooldownTime = 9.9f;
            }
            
            timerbarComponent.SetTime(cooldownTime);
            timerbarComponent.SetCurrentTime(ondeskLogic.block.cooldownTime);
            //ondeskLogic.block.cooldownTime -= Time.deltaTime;
            //逻辑更正 只有在有水桶且按下C之后才会灭火


            if (ondeskLogic.block.cooldownTime <= 0) {
                
                timebarCanvas.SetActive(false);
                
                Destroy(ondeskLogic.block.fire);
                ondeskLogic.block.fire = null;

                // 切换成垃圾
                // TODO ???lhh
                // ondesk会变成新Obj 注意前后
                GameObject firesoundObj = GameObject.FindGameObjectWithTag("firesource");
                AudioSource firesound = firesoundObj.GetComponentInChildren<AudioSource>();
                firesound.Stop();
                
                string newObjname = "trash" + ondeskLogic.block.plustype;
                
                Vector3 newpos = new Vector3(0F,0F,0F);
                newpos = newpos + ondesk.transform.position;

                if(TESTLEVEL >= 1)
                {
                    Debug.Log("new obj name: " + newObjname + "new pos: " + newpos);
                }
                Quaternion roa =  new Quaternion(0F,0F,0F,0F);
                GameObject newObj = (GameObject)Instantiate(Resources.Load(newObjname), newpos, roa);//这东西不会被丢地上 无需add in selection

                GameObject deleteObj = stovedeskLogic.block.ondesk;
                stovedeskLogic.block.ondesk = newObj;
                Destroy(deleteObj);
                /*删除原来的东西*/


                
                // 扔掉垃圾之后
                // 
                stovedeskLogic.block.overcooked = false;
                ondeskLogic.block.overcooked = false;

                image.color = origin_color;
            }
            return;
        }
        
        ondeskLogic.block.currentTime += Time.deltaTime;

        timerbarComponent.SetTime(ondeskLogic.block.needTime);
        timerbarComponent.SetCurrentTime(ondeskLogic.block.currentTime);

        if (TESTLEVEL >= 1) {
            Debug.Log("delta time ! = " + Time.deltaTime);
            Debug.Log("delta time ! = " + ondeskLogic.block.currentTime + " || " + ondeskLogic.block.needTime);
        }
        
        float currentTime = ondeskLogic.block.currentTime;
        float needTime = ondeskLogic.block.needTime;
        const float overcook_begin_rate = 1.2f;
        const float overcook_end_rate = 2.5f;

        

        //Debug.Log("currentTime = " + currentTime + ", needTime = " + needTime);
        // [0, 1) 正在煮
        // [1, overcook_begin_rate) 煮好了，完美
        // [overcook_begin_rate, overcook_end_rate) 进度条渐变，这个时候食物仍然可以正常拿走
        // [overcook_end_rate, ) 煮糊了
        if (currentTime < needTime) {
            // 没煮好，什么也不做
            if(origin_color != null) image.color = origin_color;

        } else if (currentTime < needTime * overcook_begin_rate) {
            // 煮好了
            if (TESTLEVEL >= 1) {
                Debug.Log("!! complete deal !!");
            }
            //烧煮的时间到了
            ondeskLogic.block.deal = true;
            //origin_color = image.color;
            
            if(origin_color != null) image.color = origin_color;
            //ondeskLogic.block.needprocess = true;
            //ondeskLogic.block.needTime = 0F;
            //ondeskLogic.block.currentTime = 0F;
        } else if (currentTime < needTime * overcook_end_rate) {
            // 快煮糊了，改进度条颜色
            float t = (currentTime / needTime - overcook_begin_rate) 
                / (overcook_end_rate - overcook_begin_rate);
            image.color = Color.Lerp(origin_color, Color.red, t);
            
            float rate = currentTime / needTime - 1.0f;
            rate = 2.0F -rate;
            float cas_time = currentTime / rate;
            int cas_int = (int)cas_time;
            if(cas_int % 2 == 0)
            {
                timebarCanvas.SetActive(false);
            }
            else{
                
                GameObject didisoundObj = GameObject.FindGameObjectWithTag("didisource");
                AudioSource didisound = didisoundObj.GetComponentInChildren<AudioSource>();
                didisound.Play();
            }

        } else {
            // 煮糊了
            timebarCanvas.SetActive(true);
            stovedeskLogic.block.overcooked = true;
            ondeskLogic.block.overcooked = true;
            ondeskLogic.block.cooldownTime = cooldownTime;
            ondeskLogic.block.currentTime = 0;
        }
    }

    void Handle_SelectInvalid() {
        if (handObj == null) return; //手上空 无事发生

        // 手上的东西放下
        LogicControl handLogic = handObj.GetComponentInChildren<LogicControl>();
        if (handLogic.block.type == "food" || handLogic.block.type == "coolfire") {
            handObj.transform.parent = null;
            handObj.transform.position = transform.position;
            handObj = null;
        }
    }

    void submithandle()
    {
        LogicControl handLogic = handObj.GetComponentInChildren<LogicControl>();
        string submitname = handLogic.block.name;
        GameObject deleteObj = handObj;
        handObj.transform.parent = null;
        handObj = null;
        Destroy(deleteObj);
        submitAudio.Play();
        // 加分
        GameObject moneyObj = GameObject.FindGameObjectWithTag("money");
        Text moneyText = moneyObj.GetComponentInChildren<Text>();
        int point = 100;
        moneyText.text = (int.Parse(moneyText.text) + point).ToString();
        // 隐藏菜单并判断胜利
        GameObject[] targets = GameObject.FindGameObjectsWithTag("targetfood");
        bool complete = true;
        bool do_inactive = true;
        foreach(var target in targets) {
            if (!target.activeInHierarchy) continue;
            targetfoodcontrol targetControl = target.GetComponent<targetfoodcontrol>();
            Debug.Log(targetControl.name + ", " + submitname);
            if(do_inactive && targetControl.name == submitname) {
                target.SetActive(false);
                do_inactive = false;
            } else {
                complete = false;
            }
        }
        if (complete) {
            SceneManager.LoadScene("WinGame");
        }
        //没收盘子
        /*string newObjname = "plate";
        handObj = (GameObject)Instantiate(Resources.Load(newObjname), transform, false);
        BindObj(handObj);
        Debug.Log("submit food: " + submitname);*/
    }
    void HandFull_Desk()
    {
        LogicControl logic = currentselectObj.GetComponentInChildren<LogicControl>();
        LogicControl handLogic = handObj.GetComponentInChildren<LogicControl>();
        if (logic.block.name == "trashdesk")
        {
            if(handLogic.block.type == "container")
            {
                //倒掉菜
                GameObject deleteObj = handObj;
                handObj.transform.parent = null;
                handObj = null;
                Destroy(deleteObj);

                string newObjname = handLogic.block.plustype;
                handObj = (GameObject)Instantiate(Resources.Load(newObjname), transform, false);
                LogicControl newfoodLogic = handObj.GetComponentInChildren<LogicControl>();
                handObj.transform.localScale /= 1.8F;
                BindObj(handObj);
            }
            else if(handLogic.block.type == "food")
            {
                GameObject deleteObj = handObj;
                handObj.transform.parent = null;
                handObj = null;
                Destroy(deleteObj);
            }
            return;
        }
        if(logic.block.name == "submitdesk")
        {
            if(handLogic.block.plustype != "plate")return;

            submithandle();
            
            return;
        }
        if(logic.block.overcooked == true)
        {
            return;
        }
            //added
        if (logic.block.ondesk == null) { //桌子上面空的 
                        /*+++++ 区分桌子 有洗盘子的地方了!*/
            handObj.transform.parent = null;
            Vector3 dltH = new Vector3(0F,0.9F,0F);
            if(handLogic.block.plustype == "pot")
            {
                Vector3 dltforpots = new Vector3(-0.1F,0F,-0.1F);
                dltH += dltforpots;
            }
            handObj.transform.position = currentselectObj.transform.position + dltH;
            logic.block.ondesk = handObj;
                        /*sbl*/
            handObj = null;

        } else { //什么也不做
                    
                    
            GameObject ondesk = logic.block.ondesk;
                    LogicControl ondeskLogic = ondesk.GetComponentInChildren<LogicControl>();
                    
            /*盘子的食物组合*/
            //处理盘子逻辑
                 
            if(ondeskLogic.block.type == "container" && ondeskLogic.block.plustype == "plate" && handLogic.block.type == "food" )
            {
                if(foodmanage.combine_check(ondeskLogic.block.foodincontainer,handLogic.block.name))
                {
                    string newfoodname = foodmanage.combinefood(ondeskLogic.block.foodincontainer,handLogic.block.name);
                    string newObjname = newfoodname + ondeskLogic.block.plustype;
                    Vector3 newpos = new Vector3(0F,1.0F,0F);
                    newpos = newpos + currentselectObj.transform.position;

                    if(TESTLEVEL >= 1)
                    {
                        Debug.Log("new obj name: " + newObjname + "new pos: " + newpos);
                        Debug.Log("new obj name: " + newfoodname + " - "+newObjname + "new pos: " + newpos);
                    }
                    Quaternion roa =  new Quaternion(0F,0F,0F,0F);
                    GameObject newObj = (GameObject)Instantiate(Resources.Load(newObjname), newpos, roa);//这东西不会被丢地上 无需add in selection

                    /*
                    更新图画:新建一个名字为newfoodname的物体
                    删除obj*2
                    删除hand关系
                    */
                    handObj.transform.parent = null;
                    GameObject deleteObj = handObj;
                    handObj = null;
                    Destroy(deleteObj);

                    deleteObj = logic.block.ondesk;
                    Destroy(deleteObj);
                    logic.block.ondesk = newObj;
                    /*删除原来的东西*/
                }
            }
            else if(ondeskLogic.block.type == "food" && handLogic.block.type == "container" && handLogic.block.plustype == "plate")
            {
                //暂时不允许把盘子放食物上
            }
            foodmerge_handle();

            //准备烧/煮菜 是fryingpan/且为空 没少菜 手上拿的是食物 可以被烧
            if( ondeskLogic.block.type == "container" && (ondeskLogic.block.plustype == "fryingpan" || ondeskLogic.block.plustype == "pot" ) 
            && ondeskLogic.block.deal == false && (ondeskLogic.block.foodincontainer == null || ondeskLogic.block.foodincontainer == "" )&& handLogic.block.type == "food" )
            {
                string plustype = ondeskLogic.block.plustype;
                if(foodmanage.dealable(handLogic.block.name,plustype))
                {   
                    //建立新物体 开始烧菜
                    //123
                    string newfoodname = foodmanage.afterdealname(handLogic.block.name,plustype);
                    string newObjname = newfoodname + plustype;
                    Vector3 newpos = new Vector3(0F,0.7F,0F);
                    
                    Quaternion roa =  new Quaternion(0F,0F,0F,0F);
                    //??
                    newpos = ondesk.transform.position;
                    Debug.Log("zzz ! : "+ newObjname);
                    GameObject newfoodObj = (GameObject)Instantiate(Resources.Load(newObjname), ondesk.transform.position, roa);

                    LogicControl newObjlogical = newfoodObj.GetComponentInChildren<LogicControl>();
                        
                    logic.block.ondesk = null;
                    GameObject deleteObj = ondesk;//delete原来的物体
                    ondesk = null;
                    Destroy(deleteObj);

                    handObj.transform.parent = null;//delete手上的物体
                    deleteObj = handObj;
                    handObj = null;
                    Destroy(deleteObj);

                        
                    newObjlogical.block.needTime = foodmanage.dealtime(handLogic.block.name,plustype);
                    newObjlogical.block.currentTime = 0F;
                    newObjlogical.block.deal = false;
                    newObjlogical.block.foodincontainer = newfoodname;
                    //newObjlogical.block.deal = true; 换个意思 true表示处理好了!
                    newObjlogical.block.afterdealname = foodmanage.afterdealname(handLogic.block.name,plustype);
                    newObjlogical.block.needprocess = true;
                    Debug.Log("????  need process = " + newfoodObj );
                    Debug.Log(newObjlogical.block.needprocess);
                    logic.block.ondesk = newfoodObj;
                }
            }

        }       
    }
    void foodmerge_handle()
    {
        if(currentselectObj == null)return;
        LogicControl logic = currentselectObj.GetComponentInChildren<LogicControl>();
        if(handObj == null)return;
        LogicControl handLogic = handObj.GetComponentInChildren<LogicControl>();
        if(handLogic.block.plustype == "plate")
        {
            //Debug.Log("in food merge!");
            if(logic.block.type != "desk")return;
            if(logic.block.ondesk == null)return;
            GameObject ondesk = logic.block.ondesk;
            LogicControl ondesklogic = ondesk.GetComponentInChildren<LogicControl>();
            if(ondesklogic.block.plustype != "pot" && ondesklogic.block.plustype != "fryingpan" )return;

            
            //Debug.Log("in food merge 2!");

            if(ondesklogic.block.deal!= true)return;//没煮熟
            
            //取消一下进度条 
            GameObject timebarCanvas = currentselectObj.transform.Find("TimerBarCanvas").gameObject;
            if(timebarCanvas != null)timebarCanvas.SetActive(false);

            string foodincontainer = ondesklogic.block.foodincontainer;
            //Debug.Log("in food merge!3 " + foodincontainer);
            if(foodincontainer == null || foodincontainer == "" )return;
            
            //Debug.Log("in food merge!4");
            string foodinplate = handLogic.block.foodincontainer;
            if(foodmanage.combine_check(foodincontainer,foodinplate));
            string newfoodname = foodmanage.combinefood(foodincontainer,foodinplate);

            //handObj = (GameObject)Instantiate(Resources.Load(newObjname), transform, false);
///ggg
            Quaternion roa =  new Quaternion(0F,0F,0F,0F);
            GameObject newcontainer = (GameObject)Instantiate(Resources.Load(ondesklogic.block.plustype), ondesk.transform.position, roa);
            //这东西不会被丢地上 无需add in selection
            
            //新的container
            GameObject deleteObj = ondesk;
            logic.block.ondesk = newcontainer;
            Destroy(deleteObj);

            handLogic.transform.parent = null;
            deleteObj = handObj;
            string newhandname = newfoodname + "plate";
            Debug.Log(ondesklogic.block.plustype + "** "+ newhandname);
            GameObject newplate = (GameObject)Instantiate(Resources.Load(newhandname), handObj.transform.position, roa);
            handObj = newplate;
            Destroy(deleteObj);
            //foodmergeplate 456
            return;
        }
    }
    void Handle_HandFull_SelectValid() {
        // 手上有东西，且选中了东西
        // 比如放下盘子
        LogicControl logic = currentselectObj.GetComponentInChildren<LogicControl>();
        LogicControl handLogic = handObj.GetComponentInChildren<LogicControl>();
        if (logic == null)
            return;
        switch(logic.block.type) {

            case "desk":
                HandFull_Desk();
                
                break; 
            case "food"://放下手中东西
                //LogicControl handLogic = handObj.GetComponentInChildren<LogicControl>();
                if(handLogic.block.type == "container")
                {
                    //不支持放下盘子
                }
                else if(handLogic.block.type == "food")
                {
                    handObj.transform.parent = null;
                    handObj.transform.position = transform.position;
                    handObj = null;
                    // 手上的东西放下
                }
                break;
            case "container"://不支持
                //foodmerge_handle();
                break;
            case "coolfire":
                break;
            default:
                Debug.LogError("Unknown type in Handle_HandFull_SelectValid.");
                break;
        }
    }

    void Handle_HandEmpty_SelectValid() {
        // 手上没有东西，且选中了东西
        // 比如拿起食材
        LogicControl logic = currentselectObj.GetComponentInChildren<LogicControl>();
        if (logic == null)
            return;

        switch(logic.block.type) {
            case "desk"://???
                if (logic.block.ondesk == null ) { //桌子上面空的 
                    
                    if(!string.IsNullOrEmpty(logic.block.content))
                    {
                        handObj = (GameObject)Instantiate(Resources.Load(logic.block.content), transform, false);
                        handObj.transform.localScale /= 1.8F;
                        BindObj(handObj);
                        selection.Add(handObj);//食材是可以被选中的
                    }
                    else{
                        //无事发生
                    }

                } else { //只能拿桌子上的东西
                    /*
                    此处可能拿下了一个锅炉 注意后面的影响 tag ???
                    */
                    /*!!*/
                    if(logic.block.overcooked == true)
                    {

                    }
                    else{
                        handObj = logic.block.ondesk;
                        //GameObject ondeskObj = logic.block.ondesk;
                        //LogicControl ondesklogic = ondeskObj.GetComponentInChildren<LogicControl>();
                        handObj.transform.parent = transform;
                        logic.block.ondesk = null;
                        BindObj(handObj);

                        //进度条去掉 好像不需要其他逻辑 直接false就没错
                        if(logic.block.name == "stovedesk" || logic.block.name == "cutdesk" )
                        {
                            GameObject timebarCanvas = currentselectObj.transform.Find("TimerBarCanvas").gameObject;
                            if(timebarCanvas != null)timebarCanvas.SetActive(false);
                        
                        }    
                    }
                   
                }
                break;
            case "food":
                //捡到了食物
                handObj = currentselectObj;
                handObj.transform.parent = transform;
                BindObj(handObj);
                break;
            case "container":
                //不支持盘子落地 X捡到了盘子
                /*handObj = currentselectObj;
                handObj.transform.parent = transform;
                BindObj(handObj);*/
                break;
            case "coolfire":
                handObj = currentselectObj;
                handObj.transform.parent = transform;
                BindObj(handObj);//捡起灭火器
                break;
            default:
                Debug.LogError("Unknown type in Handle_HandEmpty_SelectValid.");
                break;
        }
    }

    private void TankUpdate()
    {
        Debug.Log("in tank Update");
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.LeftShift);

        if (v < 0) {
            if (walk) { v *= m_backwardsWalkScale; }
            else { v *= m_backwardRunScale; }
        } else if(walk)
        {
            v *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        transform.position += transform.forward * m_currentV * m_moveSpeed * Time.deltaTime;
        transform.Rotate(0, m_currentH * m_turnSpeed * Time.deltaTime, 0);

        m_animator.SetFloat("MoveSpeed", m_currentV);

        JumpingAndLanding();
    }

    private void DirectUpdate()
    {
        Debug.Log("in direct Update");
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        Transform camera = Camera.main.transform;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            v *= m_walkScale;
            h *= m_walkScale;
        }

        m_currentV = Mathf.Lerp(m_currentV, v, Time.deltaTime * m_interpolation);
        m_currentH = Mathf.Lerp(m_currentH, h, Time.deltaTime * m_interpolation);

        Vector3 direction = camera.forward * m_currentV + camera.right * m_currentH;

        float directionLength = direction.magnitude;
        direction.y = 0;
        direction = direction.normalized * directionLength;

        if(direction != Vector3.zero)
        {
            m_currentDirection = Vector3.Slerp(m_currentDirection, direction, Time.deltaTime * m_interpolation);

            transform.rotation = Quaternion.LookRotation(m_currentDirection);
            transform.position += m_currentDirection * m_moveSpeed * Time.deltaTime;

            m_animator.SetFloat("MoveSpeed", direction.magnitude);
        }

        JumpingAndLanding();
    }

    private void JumpingAndLanding()
    {
        bool jumpCooldownOver = (Time.time - m_jumpTimeStamp) >= m_minJumpInterval;

        if (jumpCooldownOver && m_isGrounded && Input.GetKey(KeyCode.Space))
        {
            m_jumpTimeStamp = Time.time;
            m_rigidBody.AddForce(Vector3.up * m_jumpForce, ForceMode.Impulse);
        }

        if (!m_wasGrounded && m_isGrounded)
        {
            m_animator.SetTrigger("Land");
        }

        if (!m_isGrounded && m_wasGrounded)
        {
            m_animator.SetTrigger("Jump");
        }
    }
}
