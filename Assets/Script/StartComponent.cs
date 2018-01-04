using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        if ((Input.GetAxis("LS_Vertical") > 0.3)
            ||(( Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) )))
            menuselect = 0;
        else if ((Input.GetAxis("LS_Vertical") < -0.3)||
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
            if (Input.GetButton("Accept"))
            {
                sync = SceneManager.LoadSceneAsync("LoadScreen");
            }
            text.text = "- Start - \n End ";
        }
        if (menuselect == 1)
        {
            if (Input.GetButton("Accept"))
            {
                Application.Quit();
            }
            text.text = " Start \n - End -";
        }
    }
}
