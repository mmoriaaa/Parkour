using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;                                                  //引入DoTween插件
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

/* 思考题
 * 1：由于我们的地形有高有底，会导致我们的（playController.isGrounded）检测是否在地面有时失效。怎么解决呢？
 * 2：玩家在进行下滑时，身形会能够通过比较低一点的地形，这一块这个Demo里面没有。那么我们如何实现这个功能呢？
 */

public class PlayerController : MonoBehaviour {

    public CharacterController playController;
    public Animator playAnimtor;
    public Vector3 MoveIncrements;
    [SerializeField]
    float transverseSpeed = 5.0f;                                   //玩家横向的移动速度
    public float moveSpeed = 40.0f;                                  //玩家的游戏移动速度
    public float jumpPower;                                         //玩家的跳跃高度
    [HideInInspector]
    public GameObject nowRoad;                                      //现在玩家脚下的道路
    bool isTurnleftEnd = true;                                      //左转向是否完成
    bool isTurnRightEnd = true;                                     //右转向是否完成
    bool isJumpState;                                               //现在是否是转向状态
    bool conflictFlag = false;
    RuntimeAnimatorController nowController;                        //现在的动画控制器
    AnimationClip[] cilps;

    public static int countGrenade;
    public static int countUmbrella;
    public static int countChair;

    public Text Tools;

    private GameObject Grenade;
    private GameObject Umbrella;
    private GameObject Chair;


    void Start ()
    {
        jumpPower = 20.0f;
        playController = GetComponent<CharacterController>();
        playAnimtor = GetComponent<Animator>();
        nowController = playAnimtor.runtimeAnimatorController;
        cilps = nowController.animationClips;

        for (int i=0;i<cilps.Length; i++)
        {
            if (cilps[i].events.Length<=0)
            {
                switch (cilps[i].name)
                {
                    case "JUMP00":
                        AnimationEvent endEvent = new AnimationEvent();
                        endEvent.functionName = "JumpEnd";
                        endEvent.time = cilps[i].length - (20.0f / 56.0f) * 1.83f;
                        cilps[i].AddEvent(endEvent);
                        AnimationEvent centerEvent = new AnimationEvent();
                        centerEvent.functionName = "JumpCenter";
                        centerEvent.time = cilps[i].length * 0.3f;
                        cilps[i].AddEvent(centerEvent);
                        break;
                    case "SLIDE00":
                        AnimationEvent slideEvent = new AnimationEvent();
                        slideEvent.functionName = "SlideEnd";
                        slideEvent.time = cilps[i].length - (15.0f / 42.0f) * 1.33f;
                        cilps[i].AddEvent(slideEvent);
                        break;
                }
            }
        }

        Grenade = GameObject.Find("Grenade");
        Umbrella = GameObject.Find("Umbrella");
        Chair = GameObject.Find("Chair");
    }

	void Update ()
    {
        if (conflictFlag == false)
        {
            moveSpeed += Time.deltaTime * 0.1f;
            float moveDir = Input.GetAxis("Horizontal");
            MoveIncrements = transform.forward * moveSpeed * Time.deltaTime;
            MoveIncrements += transform.right * transverseSpeed * Time.deltaTime * moveDir;
            if (isJumpState)                        //如果现在正在进行跳跃
            {
                MoveIncrements.y += jumpPower * Time.deltaTime;
            }
            else
            {
                MoveIncrements.y += playController.isGrounded ? 0f : (float)(-1.0f  * Math.Sqrt(Time.deltaTime) * 1f);           //更新重力
            }

            playController.Move(MoveIncrements);
            playAnimtor.SetFloat("MoveSpeed", playController.velocity.magnitude);                            //动画状态更新
        }
        else
        {
            Debug.Log(conflictFlag);
            MoveIncrements.x = 0;
            MoveIncrements.z = 0;
            transform.position += new Vector3(0, 1, 0);
            if(transform.position.y <= 200){
                playController.Move(MoveIncrements);
                //Debug.Log(MoveIncrements.x);
                Debug.Log(transform.position.y);
                //Debug.Log(MoveIncrements.z);
            }
            else
            {
                conflictFlag = false;
            }
        }

        if (Input.GetKey(KeyCode.J) && isTurnleftEnd)
        {
            isTurnleftEnd = false;                                                                              //更新转向状态
            transform.Rotate(Vector3.up,-30);
            Quaternion tmpQuaternion = transform.rotation;                                                      //计算转向后的四元数并保存
            transform.Rotate(Vector3.up, 30);                                                                   //角度回滚
            Tween tween = transform.DORotateQuaternion(tmpQuaternion, 0.3f);                                    //使用DoTween插件进行转向的平滑运动
            tween.OnComplete(() => isTurnleftEnd = true);                                                       //动画结束后转向状态更新
        }
        if (Input.GetKey(KeyCode.L) && isTurnRightEnd)
        {
            isTurnRightEnd = false;
            transform.Rotate(Vector3.up,30);
            Quaternion tmpQuaternion = transform.rotation;
            transform.Rotate(Vector3.up, -30);
            Tween tween = transform.DORotateQuaternion(tmpQuaternion, 0.3f);
            tween.OnComplete(() => isTurnRightEnd = true);
        }
        if (Input.GetKeyDown(KeyCode.Space) && playController.isGrounded)
        {
            isJumpState = true;                     //更新跳跃状态
            playAnimtor.SetBool("IsJump", true);    //播放跳跃动画
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            playAnimtor.SetBool("IsSlide", true);
        }
        if (this.transform.position.y <= 135)
        {
            SceneManager.LoadScene(2);
        }


            Tools.text = "Grenade" + countGrenade.ToString() + "Umbrella" + countUmbrella.ToString() + "Chair" + countChair.ToString();
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject!=nowRoad)                                             //去重复避免删除错误
        {
            //nowRoad = hit.gameObject;
            //Destroy(hit.gameObject,1.0f);
            //GameMode.instance.BuidRoad();                                        //生成道路
            //GameMode.instance.CloseRoadAnimator();
            //conflictFlag = true;
        }
    }

    public void JumpEnd()
    {
        playAnimtor.SetBool("IsJump", false);
    }

    public void JumpCenter()
    {
        isJumpState = false;
    }

    public void SlideEnd()
    {
        playAnimtor.SetBool("IsSlide", false);
    }

    void OnCollisionEnter (Collider col)
    {
        if (col.CompareTag("TubeCube"))
        {
            Rigidbody tmp = this.GetComponent<Rigidbody>();
            tmp.isKinematic = false;
            tmp.velocity = new Vector3(0, 10, 0);
            Debug.Log(tmp.velocity);
            Debug.Log("conflict tube");
            conflictFlag = true;
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("conflict");
        if(other.CompareTag("TubeCube")){
            
            Rigidbody tmp = this.GetComponent<Rigidbody>();
            tmp.isKinematic = false;
            tmp.velocity = new Vector3(0, 100, 0);
            Debug.Log(tmp.velocity);
            Debug.Log("conflict tube");
            conflictFlag = true;
        }
        if(other.CompareTag("Tool")){
            Debug.Log("Tool");
            switch(other.gameObject.name){
                case "Grenade":
                    Debug.Log("Grenade");
                    countGrenade += 1;
                    other.transform.position = transform.TransformPoint(0, 0, 2);
                    other.transform.parent = this.transform;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                    break;
                case "Umbrella":
                    countUmbrella += 1;
                    other.transform.position = transform.TransformPoint(0, 0, 2);
                    other.transform.parent = this.transform;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                    break;
                case "Chair":
                    countChair += 1;
                    other.transform.position = transform.TransformPoint(0, 0, 2);
                    other.transform.parent = this.transform;
                    other.GetComponent<Rigidbody>().isKinematic = true;
                    break;      
            }
            //other.gameObject.SetActive(false);
        }
        if (other.CompareTag("box"))
        {
            Debug.Log("box");
            SceneManager.LoadScene(4);
        }
    }

    void tossObject(Rigidbody rigidbody)
    {
        rigidbody.transform.Translate(Vector3.forward * 10);
        //设置角速度
        //rigidbody.angularVelocity = Vector3;
    }

}