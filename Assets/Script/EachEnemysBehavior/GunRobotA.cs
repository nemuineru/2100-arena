using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRobotA : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rigidplayer;
    AnimatorStateInfo anim;
    ActorBehavior Actor;
    public float angle;
    int Num = 3, BNum = 5;
    RuntimeAnimatorController[] anim_cont = new RuntimeAnimatorController[10];
    
    //---------//
    
    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidplayer = GetComponent<Rigidbody2D>();
        Actor = GetComponent<ActorBehavior>();
        for (int i = 0; i < 10; i++)
        {
        anim_cont[i] = Resources.Load
                ("Enemy/" + Actor.Actorname + "/Animation/Overrider/D" + i)
                as RuntimeAnimatorController;
        }
        //---//
    }

    // Update is called once per frame
    void Update()
    {     
            angle = (Mathf.Atan2(rigidplayer.velocity.y, rigidplayer.velocity.x) * Mathf.Rad2Deg + 360) % 360;

            Num = GetComponent<S_Dir>().Rot2Num4Dir(angle);

            if (BNum != Num)
            {
                animator.runtimeAnimatorController = anim_cont[Num];
                BNum = Num;
            }
            
    }
}