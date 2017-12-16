using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D),typeof(BulletBehavior),typeof(BoxCollider2D))]
[RequireComponent(typeof(BulletStatus))]
public class BulletBehavior : MonoBehaviour {
    Rigidbody2D rigidbody2d;
    Vector2 vfacingstart;
    BulletStatus status;
    public float StarttoNowDot;
	// Use this for initialization
	void Start () {
        status = this.GetComponent<BulletStatus>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = transform.up.normalized * status.speed;
        vfacingstart = transform.up.normalized;
    }


    void OnDestroy()
    {
        if (status.Explosive)
        {
            GameObject A = Instantiate(status.BulletExplodEffect,transform.position + new Vector3(0,0,-5),Quaternion.Euler(0, 0, 0)) as GameObject;
            A.name = "ExplodeEff";
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        bool contractedeach = false;
        if (coll.gameObject.tag == "Enemy" && gameObject.tag != "EBullet" )
        {
            ActorBehavior collBehavior_E =
                coll.gameObject.GetComponent<ActorBehavior>();
            collBehavior_E.Life -= status.damage *
            GameObject.FindGameObjectWithTag("Player").GetComponent<StatusMultiple>().MulDamage;
            contractedeach = true;
        }
        if (coll.gameObject.tag == "PlayerHittags" && gameObject.tag != "PBullet") {
            ActorBehavior collBehavior_P =
                coll.gameObject.transform.parent.gameObject.GetComponent<ActorBehavior>();
            collBehavior_P.Life -= status.damage;
            contractedeach = true;
        }
            if ((!status.Penetrative && contractedeach == true )|| (coll.gameObject.tag == "Terrain"))
        {
            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void Update ()
    {
        Vector2 vfacingnow = rigidbody2d.velocity.normalized;
        float randomAngle = Random.Range(-90, 90);

        if (status.Lazer)
            GetComponent<Rigidbody2D>().velocity = transform.up.normalized * status.speed;

            Destroy(gameObject,status.lifeTime);
            StarttoNowDot = Vector2.Dot(vfacingstart, vfacingnow);
            rigidbody2d.AddForce(Quaternion.Euler(0, 0, randomAngle)  * - vfacingnow * status.Air_Drag);
    }
}
