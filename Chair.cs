using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour
{
    float MoveSpeed;
    bool chairFlag = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))//扔出物体
        {
            if (Input.GetKey(KeyCode.Alpha3))
            {
                //首先判断是否为子物体
                if (PlayerController.countChair >= 1)
                {
                    chairFlag = true;
                    PlayerController.countChair -= 1;

                }
            }
        }
        if (chairFlag == true)
        {
            this.transform.Translate(Vector3.forward * 0.1f);
        }

    }
}