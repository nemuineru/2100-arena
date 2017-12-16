using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBehavior : MonoBehaviour {
    BulletStatus status;
    public float ThislayMaxim, ThisLayLengNow, HitSize;
    int currentRefNum;

    //About this Ray
    public GameObject StartRay ,MiddleRay, EndRay;
    GameObject start, middle, end;

    //Raycast.
    RaycastHit2D RayHit;

    //debug
    public Vector2 DebugHitpoint;

    //Prepare for reflec
    GameObject NextReflecRay;
    Vector2 reflection;

    void Start () {

        status = this.GetComponent<BulletStatus>();
        ThislayMaxim = status.length;
        if (status.ReflecNo >= 0) {
            status.ReflecNo -= 1;
        }
        else
            Destroy(gameObject);
        if(status.lifeTime >= 0)
        Destroy(gameObject, status.lifeTime);
        ThisLayLengNow = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ThislayMaxim = status.length;
        if (start == null)
        {
                start = Instantiate(StartRay, transform) as GameObject;
                start.transform.localPosition = new Vector2(0, start.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);
            start.name = StartRay.name + "(Start)";
          
        }
        Vector2 startRaypos = start.transform.position;

        //-------- Laser Mode -------//
        if (status.Lazer)
        {
            start.transform.localPosition = new Vector2(0, start.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);
        }
        //---------

        if (middle == null)
        {
            middle = Instantiate(MiddleRay, start.transform) as GameObject;
            middle.name = MiddleRay.name + "(Mid)";
        }
        
        if (end == null)
        {
            end = Instantiate(EndRay, start.transform) as GameObject;
            end.name = EndRay.name + "(End)";
        }



        ThisLayLengNow += status.speed / Camera.main.pixelRect.height;

        if (ThisLayLengNow > ThislayMaxim )
        {
            ThisLayLengNow = ThislayMaxim;
            if (status.lifeTime < 0)
                Destroy(gameObject);
        }
        DebugHitpoint = startRaypos + (Vector2)start.transform.up * ThisLayLengNow;
        if (gameObject.tag == "PRay")
        {
            RayHit = Physics2D.Raycast(startRaypos, start.transform.up, ThisLayLengNow, GetLayer("Terrain") + GetLayer("Enemy"));
        }
        if (gameObject.tag == "ERay")
        {
            RayHit = Physics2D.Raycast(startRaypos, start.transform.up, ThisLayLengNow, GetLayer("Terrain") + GetLayer("PlayersHitLayer"));
        }
        if (RayHit.collider) {
            ThisLayLengNow = Vector2.Distance(startRaypos , RayHit.point);
            DebugHitpoint = RayHit.point;
            reflection = new Vector2(transform.up.x, -transform.up.y);
            //Damage
            if (gameObject.tag == "PRay" && RayHit.collider.tag == "Enemy")
            {
                RayHit.collider.GetComponent<ActorBehavior>().Life -= status.damage;
            }
            if (gameObject.tag == "ERay" && RayHit.collider.tag == "PlayerHittags")
            {
                RayHit.collider.transform.root.GetComponent<ActorBehavior>().Life -= status.damage;
            }
        }

        if (0 > ThislayMaxim)
        {
           ThislayMaxim = 0;
        }
        
        end.transform.localPosition = new Vector2(0, ThisLayLengNow - end.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);

        //mid ray Transform
        middle.transform.localPosition = 
            new Vector2(0, (start.transform.localPosition.y + end.transform.localPosition.y)/2
            + start.GetComponent<SpriteRenderer>().bounds.size.y / 2 - end.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);
        middle.transform.localScale = 
            new Vector2(1, (ThisLayLengNow) / start.GetComponent<SpriteRenderer>().sprite.bounds.size.y);


        //DEBUG
        Debug.DrawLine(startRaypos, DebugHitpoint ,Color.red);
        Debug.DrawRay(transform.position, transform.up * 10, Color.blue);



    }

    int GetLayer(string Name)
    {
        return LayerMask.GetMask(Name); 
    }
}
