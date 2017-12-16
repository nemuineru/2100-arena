using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusAppering : MonoBehaviour {
    UnityEngine.UI.Text text;
    GameObject player;
    public ShooterBehavior PlayerGun;
    public ActorBehavior Actor;

    int bulletrest, bulletMax;
    float Maxlife, Life;

    string WeaponString;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        text = GetComponent<UnityEngine.UI.Text>();
        Actor = player.GetComponent<ActorBehavior>();
    }

    float i = 0;
// Update is called once per frame
void Update ()
    {
        PlayerGun = player.GetComponentInChildren<ShooterBehavior>();
        Maxlife = Mathf.RoundToInt(Actor.MaxLife * 10);
        Life = Mathf.RoundToInt(Actor.CurrentLife * 10);
        bulletrest = PlayerGun.ammo.RestBullet;
        bulletMax = PlayerGun.ammo.MagCap;

        if(gameObject.name == "StatusWeapon")
        {
            if (player.GetComponentInChildren<ShooterBehavior>().Reloading == true)
            {
                i += Time.deltaTime;
                WeaponString = "Reloading " +
                    Mathf.CeilToInt((player.GetComponentInChildren<ShooterBehavior>()
                    .stat_Time.Reload - i) * 100);

            }
            else
            {
                i = 0;
                WeaponString = "Ammo : " +
                    bulletrest + "/" + bulletMax;
            }
            text.text = WeaponString;
        }

        if (gameObject.name == "StatusHealth")
        {
            text.text =  Life + "/" + Maxlife;
        }
        this.transform.SetSiblingIndex(10);
    }
}
