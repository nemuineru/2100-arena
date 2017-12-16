
using System.Collections;
using System.Collections.Generic;

using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadEff : MonoBehaviour
{
    AsyncOperation sync;

    // Use this for initialization
    void Start()
    {
        sync = SceneManager.LoadSceneAsync("GameField");
        StartCoroutine("LoadSample");
    }

    IEnumerator LoadSample()
    {
        new WaitForSeconds(2.0f);
        float BSync = 0, Time;
        while (!sync.isDone)
        {
            Time = Mathf.Abs(BSync - sync.progress) ;
            GetComponent<Animator>().SetFloat("PlaySpd",Time);
            BSync = sync.progress;
            yield return null;
        }
    }
    
    
    // Update is called once per frame
	void Update () {
		
	}
}
