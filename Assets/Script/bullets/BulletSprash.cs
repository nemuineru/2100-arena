using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSprash : MonoBehaviour {
    Animator Anim;
	// Use this for initialization
	void Start () {
        Anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!(Anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1))
            DestroyObject(gameObject);
	}
}
