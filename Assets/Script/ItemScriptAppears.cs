using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemScriptAppears : MonoBehaviour {

    public Font font;
    public string text;
    public Text Script;
    GameObject Player;
	// Use this for initialization
	void Start () {
        Script = gameObject.AddComponent<Text>();
        GameObject Canvas = GameObject.Find("Canvas");
        transform.SetParent(Canvas.transform);

        Script.text = text;
        Script.alignment = TextAnchor.MiddleCenter;
        GetComponent<RectTransform>().sizeDelta = new Vector2(240, 80);
        Script.font = font;


        Player = GameObject.FindGameObjectWithTag("Player");
        Object.Destroy(gameObject,1.5f);
    }

    float Y = 0;
	// Update is called once per frame
	void Update () {
        Y += Time.deltaTime;
    transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main,Player.transform.position) + 20 * new Vector2(0,Y);
	}
}
