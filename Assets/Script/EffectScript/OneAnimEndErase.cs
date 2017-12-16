using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneAnimEndErase : MonoBehaviour {
    public AudioClip ExplodSound;
	// Use this for initialization
	void Start () {
        if (ExplodSound != null)
        gameObject.GetComponent<AudioSource>().PlayOneShot(ExplodSound);
	}
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            Destroy(gameObject);
        }
	}
}
