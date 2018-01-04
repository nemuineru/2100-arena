using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamScript : MonoBehaviour {
    public GameObject player,Cursor;
    public Vector3 BtwPlayerandCusor;
    public Vector2 movPD;
    // Use this for initialization
    void Start () {
         player = GameObject.FindGameObjectWithTag("Player");
        Cursor = GameObject.Find("MousePoint");
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(player != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
                Application.Quit();

            movPD = player.GetComponent<Rigidbody2D>().velocity;

            var Joystic = Input.GetJoystickNames();
            if (Joystic.Length == 0)
            {
                BtwPlayerandCusor = Vector3.Lerp(player.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.5f);
                transform.position = new Vector3(BtwPlayerandCusor.x + movPD.x / 25, BtwPlayerandCusor.y + movPD.y / 25, -5);
            }
            else
            {
                BtwPlayerandCusor = Vector3.Lerp(player.transform.position, Cursor.transform.position, 0.8f);
                transform.position = new Vector3(BtwPlayerandCusor.x + movPD.x / 25, BtwPlayerandCusor.y + movPD.y / 25, -5);
            }
        }
    }
    Vector2 RightAxisInput()
    {
        return new Vector2(Input.GetAxis("RS_Horizontal"), Input.GetAxis("RS_Vertical"));
    }
}
