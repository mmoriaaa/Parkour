using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Umbrella : MonoBehaviour
{
    float MoveSpeed;
    bool umbrellaFlag = false;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))//扔出物体
        {
            if (Input.GetKey(KeyCode.Alpha2))
            {
                //首先判断是否为子物体
                if (PlayerController.countUmbrella >= 1)
                {
                    umbrellaFlag = true;
                    PlayerController.countUmbrella -= 1;

                }
            }
        }
        if (umbrellaFlag == true)
        {
            this.transform.Translate(Vector3.forward * 0.1f);
        }

    }
}