using System.Collections;
using System.Collections.Generic;
using GamepadInput;
using UnityEngine;

public class MousePoint : MonoBehaviour
{

    Vector3 mouse;
    GameObject Player;
    // Use this for initialization
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Player != null)
        {
            var Joystic = Input.GetJoystickNames();
            if (Joystic.Length == 0)
            {
                Destroy(gameObject);
                mouse = Input.mousePosition;
                transform.position = (Vector3)Camera.main.ScreenToWorldPoint(mouse) + new Vector3(0, 0, -5);
            }
            else
            {
                transform.position = (Vector3)RightAxisInput() * 40 + Player.transform.position;
            }
        }
    }
    Vector2 RightAxisInput()
    {
            return GamePad.GetAxis(GamePad.Axis.RightStick, GamePad.Index.Any);
    }
}
