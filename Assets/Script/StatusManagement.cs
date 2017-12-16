using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusManagement : MonoBehaviour {
    public GameObject Heart, Status_Portrait;
    [Space]
    public Vector2 Health_Axis, Health_AddAx, Health_Size;
    GameObject Player;
    GameObject[] MultiHeart;
    // Use this for initialization
    void Start () {
        MultiHeart = new GameObject[200];
        Player = GameObject.FindGameObjectWithTag("Player");
        Instantiate(Status_Portrait, this.transform);  
    }

    private void Update()
    {
    }
}
