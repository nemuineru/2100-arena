using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.SceneManagement;

public class StartComponent : MonoBehaviour
{
    UnityEngine.UI.Text text;
    int menuselect = 0;
    AsyncOperation sync;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<UnityEngine.UI.Text>();
    }

    IEnumerator MenuControl()
    {
        if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any).y > 0.3)
            ||(( Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) )))
            menuselect = 0;
        else if ((GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any).y < -0.3)||
            (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.S))))
            menuselect = 1;
        yield return null;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine("MenuControl");
        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
        if (menuselect == 0)
        {
            if ((InputAny(GamePad.Button.A) || InputAny(GamePad.Button.B) ||
                InputAny(GamePad.Button.X) || InputAny(GamePad.Button.Y))
                ||(Input.GetKey(KeyCode.Return)))
            {
                sync = SceneManager.LoadSceneAsync("LoadScreen");
            }
            text.text = "- Start - \n End ";
        }
        if (menuselect == 1)
        {
            if ((InputAny(GamePad.Button.A) || InputAny(GamePad.Button.B) ||
                InputAny(GamePad.Button.X) || InputAny(GamePad.Button.Y))
                || (Input.GetKey(KeyCode.Return)))
            {
                Application.Quit();
            }
            text.text = " Start \n - End -";
        }
    }
    bool InputAny(GamePad.Button button)
    {
        if (GamePad.GetButton(button, GamePad.Index.Any))
            return true;
        else return false;
    }
}
