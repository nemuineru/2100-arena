using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    Animator animator;
    MouseRotation mouseRotation;
    Rigidbody2D rigidplayer;
    AnimatorStateInfo anim;
    SpriteRenderer Sprite;
    public float WalkDuration = 0f;
    public int Num, BNum = 5;
    float sec1;
    //---------//
    private AnimatorOverrideController overrideController;
    
    // Use this for initialization
    void Start()
    {
        mouseRotation = GetComponent<MouseRotation>();
        animator = GetComponent<Animator>();
        rigidplayer = GetComponent<Rigidbody2D>();
        //---//
    }

    float sec2 = 0,sec3 = 0;

    // Update is called once per frame
    void Update()
    {
        float playSpd;
        anim = animator.GetCurrentAnimatorStateInfo(0);
        playSpd = animator.GetFloat("PlaySpdMul");



        if (anim.IsTag("Move"))
        {
            WalkDuration += Time.deltaTime;
            if (playSpd > 0 && anim.length < WalkDuration)
            {
                WalkDuration += Time.deltaTime;
                animator.SetFloat("PlaySpdMul", playSpd * -1);
                WalkDuration = 0;
            }
            else if (playSpd < 0 && anim.length < WalkDuration)
            {
                animator.SetFloat("PlaySpdMul", playSpd * -1);
                WalkDuration = 0;
            }
        }
        else
        {
            WalkDuration = 0;
            animator.SetFloat("PlaySpdMul", 1);
        }

        Num = GetComponent<S_Dir>().Rot2Num8Dir(mouseRotation.RotfromThistoMouse);
            
        if (BNum != Num)
        {
            animator.runtimeAnimatorController = Resources.Load("Player/Animation/Directions/OvRd_D" + Num) as RuntimeAnimatorController;
            BNum = Num;
        }

        animator.SetInteger("Rotation_NumKey", Num);
        animator.SetFloat("MovingVector", rigidplayer.velocity.sqrMagnitude);
    }

}