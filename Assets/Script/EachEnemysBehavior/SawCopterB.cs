using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawCopterB : MonoBehaviour {
    GameObject Player;
    ActorBehavior Behavior;

    public AudioClip Hitsound;

    
    void OnCollisionEnter2D(Collision2D coll)
    {
        Vector2 VelThis = this.GetComponent<Rigidbody2D>().velocity;
        if (coll.gameObject.tag == "Enemy" &&  (this.transform.position - GameObject.FindGameObjectWithTag("Player").transform.position).magnitude < 100)
        {
            Vector2 VelThat = coll.gameObject.GetComponent<Rigidbody2D>().velocity;
            coll.gameObject.GetComponent<Rigidbody2D>().AddForce(VelThis * 30 + VelThat*10);
        }
        
    }

     void OnTriggerEnter2D(Collider2D Collide)
    {
        Vector2 VelThis = this.GetComponent<Rigidbody2D>().velocity;
        if (Collide.gameObject.tag == "PlayerHittags")
        {
            Rigidbody2D VelThat = Collide.gameObject.transform.root.GetComponent<Rigidbody2D>();
            VelThat.AddForce(VelThis * 40, ForceMode2D.Impulse);
            this.GetComponent<Rigidbody2D>().AddForce(VelThis.normalized * -30);
            AudioSource Play = GetComponent<AudioSource>();
            Play.clip = Hitsound; Play.Play();
            GameObject Player = Collide.gameObject.transform.root.gameObject;
            if (Player.GetComponent<ActorBehavior>().DoesDamaged == false) {
                ActorBehavior Status = Player.GetComponent<ActorBehavior>();
                Status.Life -= gameObject.GetComponent<ActorBehavior>().BasicAtP;
                }
        }
    }



    // Use this for initialization
    void Start ()
    {
        Behavior = GetComponent<ActorBehavior>();
        Player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector2 VelThis = GetComponent<Rigidbody2D>().velocity;
        this.GetComponent<Rigidbody2D>().AddForce((Player.transform.position - this.transform.position).normalized * Behavior.movespeed);
        GetComponent<Transform>().rotation = Quaternion.Euler(0, 0, 0);
        if (VelThis.magnitude > Behavior.movespeed)
            VelThis = VelThis.normalized * Behavior.movespeed;
    }

    bool isCameraOnsight(Vector3 GetPosition)
    {
        Vector3 Position = Camera.main.WorldToViewportPoint(GetPosition);
        if (Position.x < 0.3f ||
       Position.x > 0.7f ||
       Position.y < 0.3f ||
       Position.y > 0.7f)
        {
            // 範囲外 
            return false;
        }
        // 範囲内 
        return true;

    }

    int GetLayer(string Name)
    {
        return LayerMask.GetMask(Name);
    }
}
