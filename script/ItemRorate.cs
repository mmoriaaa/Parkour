using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRorate : MonoBehaviour {
    public float RorateSpeed = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(Vector3.down * RorateSpeed , Space.World);
	}
}
