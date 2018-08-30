using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneStartButton01 : MonoBehaviour {
    // Use this for initialization
    public bool IsNextScene = false;
    public bool IsEscGame = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            SceneManager.LoadScene(1);
        }
	}
}
