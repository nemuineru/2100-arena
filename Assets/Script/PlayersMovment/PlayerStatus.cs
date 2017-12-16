using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerStatus : MonoBehaviour
{
    public GameObject Guns;
    public GameObject Primary, Secondary, Melee;
    [Space]
    public GameObject CurrentGuns;

    AudioSource ASource;

    // Use this for initialization
    void Start()
    {
        ASource = GetComponent<AudioSource>();
        Guns = Secondary;
        CurrentGuns = Guns;
        CurrentGuns = Instantiate(Guns, transform) as GameObject;
        CurrentGuns.name = Guns.name;
    }
    // Update is called once per frame
    void Update()
    {
        if (CurrentGuns.name != Guns.name)
        {
            Object.Destroy(CurrentGuns);
            CurrentGuns = Instantiate(Guns, transform) as GameObject;
            CurrentGuns.name = Guns.name;
            CurrentGuns.GetComponent<ShooterBehavior>().Reloading = true;
        }
        if (SwitchWeapon())
        {
            if (Guns.name != Primary.name)
            {
                Guns = Primary;
            }
            else if (Guns.name != Secondary.name)
            {
                Guns = Secondary;
            }
        }
    }

    bool SwitchWeapon()
    {
        var Joystic = Input.GetJoystickNames();
        if ((GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Any)
            || GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Any)) || (Joystic.Length == 0 && Input.GetKeyDown(KeyCode.F)))
            return true;
        else
            return false;

    }
}

