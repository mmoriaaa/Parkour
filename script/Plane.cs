using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : Grenade
{
    public GameObject plane; 
    
	// Use this for initialization]
   void Awake()
    {
       
    }
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnCollisionEnter(Collision collision)
    {
        plane.SetActive(false);
        obj.SetActive(true);
        StartCoroutine(wait());


    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(2f);
        Destroy(_obj);
    }
}
