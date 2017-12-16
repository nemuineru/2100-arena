using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody2D),typeof(BoxCollider2D),typeof(AudioSource))]
public class ActorBehavior : MonoBehaviour {

    public float Life, MaxLife , CurrentLife , movespeed, BasicAtP, BasicDefP, HitInvisibleTime;
    public float Energy, MaxEnergy;
    public string Actorname;
    public Color DamagingColor;
    public bool DoesDamaged = false,
        isQuitting = false,
        isReallyinvisible;
    [Space]
    public AudioClip Hurtsound;

    public GameObject DestroyEffect1,DestroyEffect2;
    SpriteRenderer SprRend;
    
    void OnApplicationQuit()
    {
        //ゲームは終了しているか
        isQuitting = true;
    }
    
    

    float damagedsec = 0;
    

    // Use this for initialization
    void Start () {
        CurrentLife = Life;
        SprRend = GetComponent<SpriteRenderer>();
        SprRend.material.SetColor("_MaskColor", DamagingColor);
    }


    float sec2 = 0, sec3 = 0;

    // Update is called once per frame
    void Update()
    {
        if (Energy < 0)
        {
            Energy = 0;
        }

        if (Life > MaxLife)
            Life = MaxLife;

        if (Life < CurrentLife)
        {
            DoesDamaged = true;
            if (damagedsec == 0) {
                CurrentLife = Life;//ライフ減少
                GetComponent<AudioSource>().clip = Hurtsound;
                GetComponent<AudioSource>().Play();
            }
            if (HitInvisibleTime > damagedsec && isReallyinvisible)
            {
                Life = CurrentLife; //ライフはそのまま
            }
        }

        if (CurrentLife < Life)
        {
            CurrentLife = Life;
        }


        if (DoesDamaged && HitInvisibleTime > damagedsec)
        {
            damagedsec += Time.deltaTime;
        }
        else {
            DoesDamaged = false;
            damagedsec = 0;
        }

        if (DoesDamaged && sec2 < HitInvisibleTime)
        {
            sec3 += Time.deltaTime;
            if (sec3 > HitInvisibleTime / 100)
            {
                SprRend.material.SetFloat("_MaskOn", Mathf.Abs(SprRend.material.GetFloat("_MaskOn") - 1));
                sec3 = 0;
            }
            sec2 += Time.deltaTime;
        }
        else if (!DoesDamaged)
        {
            SprRend.material.SetFloat("_MaskOn", 0);
            sec2 = 0;
        }

        if (gameObject.tag == "Enemy" && !isQuitting && CurrentLife <= 0) { 
            GameObject Expld = Instantiate(DestroyEffect1, transform) as GameObject;
            Expld.transform.SetParent(null);
        }
    }
}
