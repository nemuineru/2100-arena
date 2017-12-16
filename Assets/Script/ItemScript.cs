using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(SpriteRenderer),typeof(Animator),typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class ItemScript : MonoBehaviour {

    public float lifetime;

    [Space]
    public float AddLife,AddEnergy;
    [Space]
    public float AddMaxlife;
        public float AddMulSpeed, AddMulDamage, Addmagazine, AddBurst;
    public GameObject ChangeGuns;
    public AudioClip GetSound;
    public Font Scriptfont;
    
    public string GetScript;
    Color color;
    SpriteRenderer Render;

    // Use this for initialization
    void Start () {
        Render = GetComponent<SpriteRenderer>();
        color = GetComponent<SpriteRenderer>().material.GetColor("_Color");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
     if (collision.gameObject.tag == "Player") {
         StatusMultiple StatusMultiple = collision.gameObject.GetComponent<StatusMultiple>();
         PlayerStatus Status = collision.gameObject.GetComponent<PlayerStatus>();
         ActorBehavior Actor = collision.gameObject.GetComponent<ActorBehavior>();
         AudioSource Audio = gameObject.AddComponent<AudioSource>();
            if (ChangeGuns != null && ChangeGuns.GetComponent<ShooterBehavior>()){
             Status.Guns = ChangeGuns;
            }
            if(GetSound != null )
            Audio.PlayOneShot(GetSound);
            gameObject.GetComponent<Renderer>().enabled = false;

            Actor.Life += AddLife;
            Actor.MaxLife += AddMaxlife;
            StatusMultiple.MulSpeed += AddMulSpeed;
            StatusMultiple.MulDamage += AddMulDamage;

            GameObject Script = new GameObject("ItemScript") as GameObject;
            ItemScriptAppears itemScript = Script.AddComponent<ItemScriptAppears>();
            itemScript.text = GetScript;
            itemScript.font = Scriptfont;

            if (Actor.Energy < Actor.MaxEnergy * 2) {
                if (Actor.Energy >= Actor.MaxEnergy) {
                    Actor.Energy += AddEnergy / 5;
                    Actor.MaxEnergy += AddEnergy / 50;
                }
                if (Actor.Energy < Actor.MaxEnergy)
                {
                    Actor.Energy += AddEnergy;
                }
            }

            Destroy(gameObject, GetSound.length);
        } 
    }
    
// Update is called once per frame
void Update () {
        transform.rotation = Quaternion.Euler(0,0,0);
        if (lifetime < 3) {
            if (Mathf.Round(lifetime * 10) % 2 == 1) {
                color.a = 1 - color.a;
                Render.material.SetColor("_Color",color);
                Debug.Log("!");
                    }
        }
        if (lifetime < 0)
        Destroy(gameObject);
        lifetime -= Time.deltaTime;
    }
}
