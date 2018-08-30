using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {
    public float MoveSpeed = 0f;
    public float MoveSpeedAddition = 0.5f;
    public bool MoveForward = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            MoveSpeed = 0.1f;
            this.transform.Translate(Vector3.forward * MoveSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.down * 2f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up * 2f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Rotate(Vector3.back * 2f);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Translate(Vector3.left * 2f);
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Translate(Vector3.right * 2f);
        }
    }
}
