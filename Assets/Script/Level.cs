using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public static float SavedCurrentlevel = 0, SavedScore = 0, Savedtime = 0, SavedFloor = 1;
    public float Currentlevel, Score, time , Floor = 0;
    public Texture2D Cursors;
    [Space]
    public GameObject World;
    Scene CurrentScene;

    void Scenetrans(Scene sc1, Scene sc2)
    {
        Debug.Log(sc1.name + ", to the" + sc2.name);
        

        SavedCurrentlevel = Currentlevel;
        if (sc2.name == "GameOver")
            SavedFloor = Floor;
        Savedtime = time;
        SavedScore = Score;

    }

    void Sceneload(Scene A, LoadSceneMode LSM) {
        {
            if(A.name == "GameField" )
            Floor += 1;
        }
    }

    public static Level Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        if (SavedFloor == 0)
        {
            SavedCurrentlevel = 0;
            SavedFloor = 1;
            Savedtime = 0;
            SavedScore = 0;
        }

        SceneManager.activeSceneChanged += Scenetrans;
        SceneManager.sceneLoaded += Sceneload;
    }

    // Use this for initialization
    void Start ()
    {
        Currentlevel = SavedCurrentlevel;
        Floor = SavedFloor;
        time = Savedtime;
        Score = SavedScore;
    }
    

    float TickTime = 0;
	// Update is called once per frame
	void Update ()
    {
        CurrentScene = SceneManager.GetActiveScene();
        if (CurrentScene.name == "GameField" && GameObject.Find("World") == null)
        {
            GameObject NewWorld =
                Instantiate(World, new Vector3(0, 0, -2), Quaternion.Euler(0, 0, 0)) as GameObject;

            NewWorld.name = "World";
        }

        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            TickTime += Time.deltaTime;
            if (TickTime > 0.01)
            {
                Score += 1;
                time += 1;
                TickTime = 0;
            }
            if (time % 250 == 1 && time > 0 && TickTime == 0)
            {
                Currentlevel += 1 * Floor;
            }
        }
        //if (SceneManager.GetActiveScene().name == "StartScene") ;
        if(GameObject.FindGameObjectsWithTag("Player").Length == 0 && GameObject.Find("Burst(Clone)") == null
            && SceneManager.GetActiveScene().name == "GameField")
        {
            Debug.Log("Gameover");
            SceneManager.LoadScene("GameOver");
        }

        if (SceneManager.GetActiveScene().name == "StartScene")
        {
            SavedCurrentlevel = 0;
            SavedFloor = 0;
            Savedtime = 0;
            SavedScore = 0;
            Currentlevel = SavedCurrentlevel;
            Floor = SavedFloor;
            time = Savedtime;
            Score = SavedScore;
            Destroy(Instance);
            Destroy(gameObject);
        }
    }
}
