using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRotation: MonoBehaviour
{

    public float RotfromThistoMouse, RotfromUp;
    float dirX, dirY;
    Vector3 mouse ,playerposition, playerScreenPos;

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        var Joystic = Input.GetJoystickNames();
        playerposition = GetComponentInParent<Transform>().transform.position;
        playerScreenPos = Camera.main.WorldToScreenPoint(playerposition);
        Debug.Log("J : " + Joystic.Length);
        if (Joystic.Length == 0)
        {
            mouse = Input.mousePosition;
            mouse.z = 10f;

            dirX = (mouse.x - playerScreenPos.x); dirY = (mouse.y - playerScreenPos.y);

            RotfromThistoMouse = (Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg + 360) % 360;
            RotfromUp = (RotfromThistoMouse - 90 + 360) % 360;
        }
        else
        {
            if(RightAxisInput().magnitude != 0)
            dirX = RightAxisInput().normalized.x; dirY = RightAxisInput().normalized.y;

            RotfromThistoMouse = (Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg + 360) % 360;
            RotfromUp = (RotfromThistoMouse - 90 + 360) % 360;
        }
    }

    Vector2 RightAxisInput()
    {
        return new Vector2(Input.GetAxis("RS_Horizontal"), Input.GetAxis("RS_Vertical"));
    }
}