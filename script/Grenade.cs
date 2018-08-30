using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    float MoveSpeed;
    bool grenadeFlag = false;
    int countTime;
    public GameObject Plane;
    public GameObject _obj;
    public GameObject obj;
    bool alreadyDes = false;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))//扔出物体
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                //首先判断是否为子物体
                if (PlayerController.countGrenade >= 1)
                {
                    grenadeFlag = true;
                    PlayerController.countGrenade -= 1;
                }
            }
        }
        if (grenadeFlag == true)
        {
            this.transform.Translate(Vector3.forward * 0.005f,Space.World);
            countTime += 1;
        }
        if (countTime == 10 && alreadyDes == false)
        {
            Plane.SetActive(false);
            Debug.Log("enter destroy");
            _obj.SetActive(true);
            Destroy(obj);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Destroy"))
        {
            Debug.Log("plane conflict");
            _obj.SetActive(true);
            Destroy(obj);
            Destroy(Plane);
            alreadyDes = true;
            //other.gameObject.SetActive(false);
        }
    }
}
      