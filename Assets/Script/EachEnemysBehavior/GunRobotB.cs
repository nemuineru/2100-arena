using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRobotB : MonoBehaviour {
    GameObject Player;
    ActorBehavior Behavior;
    public GameObject AttachedGun;
    Rigidbody2D Rigid2d;
    // Use this for initialization
    void Start ()
    {
        Behavior = GetComponent<ActorBehavior>();
        Player = GameObject.FindGameObjectWithTag("Player");
        Rigid2d = GetComponent<Rigidbody2D>();
        GameObject CurrentGuns = Instantiate(AttachedGun, transform) as GameObject;
        CurrentGuns.name = AttachedGun.name;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 PlayerDirection = ((Player.transform.position - this.transform.position).normalized);
        PlayerDirection = new Vector2(Mathf.RoundToInt(PlayerDirection.x), Mathf.RoundToInt(PlayerDirection.y));

        GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        Rigid2d.velocity = PlayerDirection * Behavior.movespeed;
    }

    int GetLayer(string Name)
    {
        return LayerMask.GetMask(Name);
    }
}
