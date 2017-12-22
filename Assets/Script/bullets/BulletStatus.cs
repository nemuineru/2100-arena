using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletStatus : MonoBehaviour {
    public float speed, accelation , lifeTime = 5 , damage, length, Air_Drag;
    public bool Explosive, Lazer , Homing , Releaseable, Penetrative;
    public int  ReflecNo ;
    public Vector2 bulletunder; public GameObject BulletEraseEffect, BulletExplodEffect;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
