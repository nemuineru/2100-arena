using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoving : MonoBehaviour {

    GameObject Player;
    public GameObject DropMany_1, DropMany_2;
    public GameObject[] ItemCommon, ItemRare, ItemEpic;
    [Range(0,1)]
    public float ItemChance;
    [Range(0, 100)]
    public int Drop1Num;
    [Range(0, 100)]
    public int Drop2Num;
    [Space]
    public float Score = 100;
    ActorBehavior Behavior;
    Level level;
    // Use this for initialization
    void Start()
    {
        Behavior = GetComponent<ActorBehavior>();
        level = GameObject.Find("WorldSetting").GetComponent<Level>();
        Behavior.MaxLife = Behavior.MaxLife * (1.0f + level.Currentlevel / 100);
        Behavior.Life = Behavior.MaxLife;
        Behavior.CurrentLife = Behavior.MaxLife;
    }
    

    // Update is called once per frame
    void Update() {

        if (Behavior.CurrentLife <= 0) {
            GameObject Itempops;

            if (DropMany_1 != null)
                for (int i = 0; i < Drop1Num; i++)
            {
                Itempops = DropMany_1;
                GameObject Item = Instantiate(Itempops, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                Rigidbody2D Rigid = Item.GetComponent<Rigidbody2D>();
                    float x = Random.Range(-2f, 2f);
                    float y = Random.Range(-2f, 2f);
                    Rigid.AddForce(new Vector2(x,y));
            }

            if(DropMany_2 != null)
            for (int i = 0; i < Drop2Num; i++)
            {
                Itempops = DropMany_2;
                GameObject Item = Instantiate(Itempops, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                Rigidbody2D Rigid = Item.GetComponent<Rigidbody2D>();

                    float x = Random.Range(-2f, 2f);
                    float y = Random.Range(-2f, 2f);
                    Rigid.AddForce(new Vector2(x, y));
                }


            if (Random.Range(0f, 1f) < ItemChance && ItemCommon.Length > 0) {
                int Rand = Random.Range(0,ItemCommon.Length);
                Itempops = ItemCommon[Rand];
                GameObject Item1 = Instantiate(Itempops,transform.position,Quaternion.Euler(0,0,0)) as GameObject;
                Rigidbody2D Rigid = Item1.GetComponent<Rigidbody2D>();

                float x = Random.Range(-2f, 2f);
                float y = Random.Range(-2f, 2f);
                Rigid.AddForce(new Vector2(x, y));
                if (Random.Range(0f, 1f) < ItemChance / 1.5 && ItemRare.Length > 0)
                    {
                    Rand = Random.Range(0, ItemRare.Length);
                    Itempops = ItemRare[Rand];
                    GameObject Item2 = Instantiate(Itempops, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject; 
                        Rigid = Item2.GetComponent<Rigidbody2D>();

                    float x2 = Random.Range(-2f, 2f);
                    float y2 = Random.Range(-2f, 2f);
                    Rigid.AddForce(new Vector2(x2, y2));
                    if (Random.Range(0f, 1f) < ItemChance / 2.5 && ItemEpic.Length > 0)
                        {
                            Rand = Random.Range(0, ItemEpic.Length);
                            Itempops = ItemEpic[Rand];
                            GameObject Item3 = Instantiate(Itempops, transform.position, Quaternion.Euler(0, 0, 0)) as GameObject;
                                Rigid = Item3.GetComponent<Rigidbody2D>();

                        float x3 = Random.Range(-2f, 2f);
                        float y3 = Random.Range(-2f, 2f);
                        Rigid.AddForce(new Vector2(x3, y3));
                    }
                    }
                }

            level.Score += Score;
            Destroy(this.gameObject);
        }
    }
}
