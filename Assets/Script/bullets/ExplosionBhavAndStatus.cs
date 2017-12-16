using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBhavAndStatus : MonoBehaviour {

    public float Damage , KnockPower;

    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == ("Enemy") || collision.gameObject.tag == ("PlayerHittags"))
        {
            if (collision.transform.root.gameObject.GetComponent<ActorBehavior>().DoesDamaged == false)
            {
                GameObject CollObj = collision.transform.root.gameObject;
                if(collision.gameObject.tag == ("PlayerHittags"))
                    CollObj.GetComponent<ActorBehavior>().Life -= Damage/3;
                else
                CollObj.GetComponent<ActorBehavior>().Life -= Damage * GameObject.FindGameObjectWithTag("Player").GetComponent<StatusMultiple>().MulDamage;
                Rigidbody2D collObjPhis2D = CollObj.GetComponent<Rigidbody2D>();
                collObjPhis2D.AddForce(-(transform.position - CollObj.transform.position).normalized * KnockPower);
            }
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
