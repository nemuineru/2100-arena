using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour {
    GameObject Player;
    ActorBehavior actor;
    UnityEngine.UI.Image image;

	// Use this for initialization
	void Start () {
        Player = GameObject.FindGameObjectWithTag("Player");
        actor = Player.GetComponent<ActorBehavior>();
        image = GetComponent<UnityEngine.UI.Image>();

    }

    float a = -1f;
    // Update is called once per frame
    void Update () {
        image.fillAmount = actor.Life / actor.MaxLife;
        
    }
}
