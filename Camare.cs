using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camare : MonoBehaviour {
    public float Speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(Vector3.forward * Speed , Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(Vector3.back * Speed , Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(Vector3.left * Speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(Vector3.right * Speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            this.transform.Translate(Vector3.up * Speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.E))
        {
            this.transform.Translate(Vector3.down * Speed, Space.Self);
        }
        if (Input.GetKey(KeyCode.J))
        {
            this.transform.Rotate(Vector3.down * Speed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            this.transform.Rotate(Vector3.up * Speed);
        }
        if (Input.GetKey(KeyCode.I))
        {
            this.transform.Rotate(Vector3.left * Speed);
        }
        if (Input.GetKey(KeyCode.K))
        {
            this.transform.Rotate(Vector3.right * Speed);
        }
	}
}
