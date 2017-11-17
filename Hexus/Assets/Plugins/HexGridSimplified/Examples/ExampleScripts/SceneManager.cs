using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void LoadScene(int sceneID)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneID);
    }
}
