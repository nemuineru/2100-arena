using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerMoving : MonoBehaviour {

    ActorBehavior Actorbehavior;
    Rigidbody2D PVel;
    StatusMultiple Multi;
    ActorBehavior Actor;

    public static PlayerMoving Instance
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
    }

    void ActiveSceneLD(Scene scene1, Scene scene2)
    {
        if (scene2.name == "StartScene" && Instance == null)
        {
            Destroy(Instance);
        }
    }
    // Use this for initialization
    void Start()
    {
        Actor = GetComponent<ActorBehavior>();
        PVel = GetComponent<Rigidbody2D>();
        Actorbehavior = GetComponent<ActorBehavior>();
        Multi = GetComponent<StatusMultiple>();
        SceneManager.activeSceneChanged += ActiveSceneLD;
    }

    bool isQuitting = false;


    void OnApplicationQuit()
    {
        //ゲームは終了しているか
        isQuitting = true;
    }

    


    // Update is called once per frame
    void Update () {
        float x = Input.GetAxisRaw("Horizontal") * Actorbehavior.movespeed* Multi.MulSpeed;
        float y = Input.GetAxisRaw("Vertical") * Actorbehavior.movespeed * Multi.MulSpeed;

        Vector2 movements = new Vector2(x, y);

        if (movements.magnitude != 0)
        {
            PVel.AddForce(movements);
            PVel.velocity += movements / 10;
        }
        else
        {

            PVel.velocity -= PVel.velocity / 10;
        }


        if (PVel.velocity.magnitude > GetComponent<ActorBehavior>().movespeed)
            PVel.velocity = PVel.velocity.normalized * GetComponent<ActorBehavior>().movespeed;

        PVel.rotation = 0f;
        this.transform.rotation = Quaternion.Euler(0,0,0);

        if (Actor.CurrentLife <= 0)
        {
            Instantiate(Actor.DestroyEffect1, transform.position, transform.rotation);
            Destroy(GameObject.Find("Canvas"));
            Destroy(gameObject);
        }
    }
}
