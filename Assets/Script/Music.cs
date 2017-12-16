using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Music : MonoBehaviour {

    AudioSource Aud;

	// Use this for initialization
	void Start () {
        Aud = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (SceneManager.GetActiveScene().name == "GameOver")
            Aud.Stop();
	}
}
