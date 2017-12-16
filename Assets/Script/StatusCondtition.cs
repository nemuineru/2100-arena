using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusCondtition : MonoBehaviour {
    Animator Anim;
    ActorBehavior playerAct;
	// Use this for initialization
	void Start () {
        playerAct = GameObject.FindGameObjectWithTag("Player").GetComponent<ActorBehavior>();
        Anim = GetComponent<Animator>();
    }

    bool EffectDone = false;
    int A = 0;
	// Update is called once per frame
	void Update () {
        if (playerAct != null)
        {
            if (playerAct.DoesDamaged)
            {
                A += 1;
            }
            else
            {
                A = 0;
            }

        }
        else
            A = 0;

        if (A == 1)
        {
            EffectDone = true;
        }
        else
        {
            EffectDone = false;
        }
        Anim.SetBool("Damaged", EffectDone);
    }
}
