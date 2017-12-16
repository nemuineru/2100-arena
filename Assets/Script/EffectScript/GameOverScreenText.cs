using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class GameOverScreenText : MonoBehaviour {

    UnityEngine.UI.Text text;
    Level level;
    GameObject FadeScreen;
    Animator Anim;
    [SerializeField]
    Fade fade;

    // Use this for initialization
    void Start () {
        text = GetComponent<UnityEngine.UI.Text>();
        level = GameObject.Find("WorldSetting").GetComponent<Level>();
        Anim = GameObject.Find("gameoverscreen").GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update ()
    {
        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
        {
            text.text = "Final Score - " 
                + level.Score + "\n"
                 + "You reached " + level.Floor + " level of Floor" +
                 "\n" + "Press Anything to Back to menu";
            ;
            if (InputAny(GamePad.Button.A) || InputAny(GamePad.Button.B) ||
                InputAny(GamePad.Button.X) || InputAny(GamePad.Button.Y))
                SceneManager.LoadScene("StartScene");
        }
        
    }
    bool InputAny(GamePad.Button button) {
        if (GamePad.GetButton(button, GamePad.Index.Any))
            return true;
        else return false;
        }
}
