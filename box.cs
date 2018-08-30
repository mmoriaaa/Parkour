using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class box : MonoBehaviour {
    int i;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        i += 1;
        if (i == 100)
        {
            i = 0;
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(4);
    }*/
}
