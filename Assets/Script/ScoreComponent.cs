using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreComponent : MonoBehaviour {
    UnityEngine.UI.Text text;
    Level level;

    // Use this for initialization
    void Start () {
        text = GetComponent<UnityEngine.UI.Text>();
        level = GameObject.Find("WorldSetting").GetComponent<Level>();
	}

    // Update is called once per frame
    void Update() {
        if (level != null){
            text.text = "Score - " + level.Score + "\n"
                + "Level - " + level.Currentlevel;
        }
    }
}
