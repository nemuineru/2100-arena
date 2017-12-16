using UnityEngine;
using System.Collections;
using GamepadInput;
using UnityEngine.UI;

public class DemoScene : MonoBehaviour {

    [SerializeField] Text inputText;
	
	void Update () {

        inputText.text = CheckInput();
    }

    string CheckInput()
    {
        /*
         * GamePad.GetButton     : 押している間ずっと
         * GamePad.GetButtonDown : 押した瞬間
         * GamePad.GetButtonUp   : 離した瞬間
         * 
         * GamePad.GetAxis  : スティックの傾き情報がVector2型で取得できます
         * 
         * テンプレ:
         *      GamePad.GetButton (キーコード, プレイヤー番号);
         *      
         * ※コントローラーのANALOGを有効にするとGamePad.Axis.LeftStickでスティック情報が、
         * 　無効にすると十字キー情報が取得できます。
         * 　
         * ※一人プレイゲームではプレイヤー番号を「GamePad.Index.Any」に、
         * 　マルチプレイゲームでは「GamePad.Index.One」「GamePad.Index.Two」などとすることで各コントローラーの入力を取得できます。
         */
        string res =
            "A \t : " + GamePad.GetButton(GamePad.Button.A, GamePad.Index.Any) + "\n" +
            "B \t : " + GamePad.GetButton(GamePad.Button.B, GamePad.Index.Any) + "\n" +
            "X \t : " + GamePad.GetButton(GamePad.Button.X, GamePad.Index.Any) + "\n" +
            "Y \t : " + GamePad.GetButton(GamePad.Button.Y, GamePad.Index.Any) + "\n" +
            "Start \t : " + GamePad.GetButton(GamePad.Button.Start, GamePad.Index.Any) + "\n" +
            "Select \t : " + GamePad.GetButton(GamePad.Button.Select, GamePad.Index.Any) + "\n" +
            "L1 \t : " + GamePad.GetButton(GamePad.Button.LeftShoulder1, GamePad.Index.Any) + "\n" +
            "L2 \t : " + GamePad.GetButton(GamePad.Button.LeftShoulder2, GamePad.Index.Any) + "\n" +
            "R1 \t : " + GamePad.GetButton(GamePad.Button.RightShoulder1, GamePad.Index.Any) + "\n" +
            "R2 \t : " + GamePad.GetButton(GamePad.Button.RightShoulder2, GamePad.Index.Any) + "\n" +
            "左スティック \t : " + GamePad.GetAxis(GamePad.Axis.LeftStick, GamePad.Index.Any) + "\n" +
            "右スティック \t : " + GamePad.GetAxis(GamePad.Axis.RightStick, GamePad.Index.Any) + "\n\n" +
            "※ANALOG有効時しかスティック情報は取得できません";

        return res;
    }
}
