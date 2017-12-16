
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeMaze : MonoBehaviour
{

    public int FloorWidth, FloorHeight;
    public bool isRandomNumRoom, WillCheckRoomCollide;
    public GameObject Floor, Wall, Door, MazeFloor, MazeEnd;
    public float sizeTx;
    
    public int MxWidth, MxHeight, RoomNum, CurveFreq , EnemNum;
    [Space]
    public GameObject[] Enemys;

    string[,] Tiletag;

    private void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        Level level = GameObject.Find("WorldSetting").GetComponent<Level>();

        FloorWidth = Mathf.FloorToInt(FloorWidth * (level.Currentlevel + 300) / 300);
        FloorHeight = Mathf.FloorToInt(FloorHeight * (level.Currentlevel + 300) / 300);
        MxWidth = Mathf.FloorToInt(MxWidth * (level.Currentlevel + 120) / 120);
        MxHeight = Mathf.FloorToInt(MxHeight * (level.Currentlevel + 120) / 120);
        RoomNum = Mathf.FloorToInt(RoomNum * (level.Currentlevel + 60) / 60);
        EnemNum = Mathf.FloorToInt(EnemNum * (level.Currentlevel + 80) / 80);

        sizeTx = Floor.
            GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        Tiletag = new string[FloorWidth, FloorHeight];
        for (int i = 0; i < FloorWidth; i++)
            for (int j = 0; j < FloorHeight; j++)
            {
                Tiletag[i, j] = "Impassable";
            }



        if (isRandomNumRoom)
        {
            RoomNum = Random.Range(5, 20);
        }



        GameObject[] room = new GameObject[RoomNum];

        //ルーム作成
        for (int i = 0; i < RoomNum; i++)
        {
            room[i] = new GameObject("Room_" + (i + 1));
            room[i].transform.parent = transform;
            Roomproperty roomProperty = room[i].AddComponent<Roomproperty>();
            while (roomProperty.isRoomCollide == true)
            {
                roomProperty.x = Random.Range(1, FloorWidth - 1);
                roomProperty.y = Random.Range(1, FloorHeight - 1);
                roomProperty.Width = Random.Range(2, MxWidth);
                roomProperty.Height = Random.Range(2, MxHeight);
                if (i != 0 && WillCheckRoomCollide) // 最初の部屋を除く部屋同士で‥
                {
                    //ルーム同士の接触を調べる。
                    //-----------
                    for (int j = 0; j < i; j++)
                    {
                        Roomproperty property_j = room[j].GetComponent<Roomproperty>();
                        int AveWidth = (property_j.Width + roomProperty.Width) / 2;
                        int AveHeight = (property_j.Height + roomProperty.Height) / 2;
                        Vector2 pr_cp = new Vector2((roomProperty.x + roomProperty.Width / 2), (roomProperty.y + roomProperty.Height / 2));
                        Vector2 pr_j_cp = new Vector2((property_j.x + property_j.Width / 2), (property_j.y + property_j.Height / 2));

                        if ((
                            Mathf.Abs(pr_cp.x - pr_j_cp.x) > AveWidth
                            ||
                            Mathf.Abs(pr_cp.y - pr_j_cp.y) > AveHeight
                            ) &&(
                            (FloorWidth > roomProperty.x + roomProperty.Width) &&
                            (FloorHeight > roomProperty.y + roomProperty.Height)
                            ))
                        {
                            roomProperty.isRoomCollide = false;
                        }
                        else
                        {
                            roomProperty.isRoomCollide = true;
                            break;
                        }
                    }
                }
                // ---------ここまで


                else if((FloorWidth > roomProperty.x + roomProperty.Width) &&
                            (FloorHeight > roomProperty.y + roomProperty.Height)
                    )
                {
                    roomProperty.isRoomCollide = false;
                }
            }


            //ルームの床を生成。
            for (int y = 0; y < roomProperty.Height; y++)
            {
                for (int x = 0; x < roomProperty.Width; x++)
                {
                    GameObject Tile = new GameObject("Tile:" +( roomProperty.x + x )+ "," +( roomProperty.y + y));
                    Tileproperty tileproperty = Tile.AddComponent<Tileproperty>();
                    tileproperty.x = x + roomProperty.x; tileproperty.y = y + roomProperty.y; tileproperty.type = "Room";
                    Tile.transform.parent = room[i].transform;
                    SpriteRenderer TileSplite = Tile.AddComponent<SpriteRenderer>();
                    TileSplite.sprite = Floor.GetComponent<SpriteRenderer>().sprite;
                    Tile.transform.position = new Vector3((roomProperty.x + x) * TileSplite.bounds.size.x, 
                        (roomProperty.y + y) * TileSplite.bounds.size.y, 5);
                    Tiletag[(x + roomProperty.x), (y + roomProperty.y)] = "Room";
                    if (i == 0) {
                        Tiletag[(x + roomProperty.x), (y + roomProperty.y)] = "FirstRoom";
                    }
                }
            }
            if (i == RoomNum - 1)
            {
                bool isExitNotplaced = true;
                GameObject Exit = Instantiate(MazeEnd) as GameObject;
                Exit.name = "Exit";
                while (isExitNotplaced)
                {
                    int x = roomProperty.x + Random.Range(0, roomProperty.Width);
                    int y = roomProperty.y + Random.Range(0, roomProperty.Height);
                    SpriteRenderer Sprite = Exit.GetComponent<SpriteRenderer>();
                    Exit.transform.position = new Vector3(x * 
                        Sprite.bounds.size.x, y * Sprite.bounds.size.y, 4);
                    isExitNotplaced = false;

                }
            }
        }


        //プレイヤーを配置。
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        bool isPlayerNotatFirstRoom = true;
        while (isPlayerNotatFirstRoom)
        {
            int x, y;
            Roomproperty property = room[0].GetComponent<Roomproperty>();
            x = (property.x + Random.Range(0, property.Width)); y = property.y + (Random.Range(0, property.Height));
            player.transform.position =
                new Vector2
                (x * Floor.GetComponent<SpriteRenderer>().bounds.size.x,
                y * Floor.GetComponent<SpriteRenderer>().bounds.size.y);
            isPlayerNotatFirstRoom = false;
        }


        //迷路のホールウェイを生成‥。
        GameObject maze = new GameObject("maze");
        for (int i = 0; i < RoomNum - 1; i++)
        {
            GameObject Hallway = new GameObject("Hallway : " + i);
            Hallway.transform.parent = maze.transform;
            int x1, y1, x2, y2, Hallway_Width, Hallway_Height,Mul_X,Mul_Y;
            Roomproperty room1 = room[i].GetComponent<Roomproperty>();
            Roomproperty room2 = room[i + 1].GetComponent<Roomproperty>();
            x1 = Random.Range(room1.x, room1.x + room1.Width - 1);
            x2 = Random.Range(room2.x, room2.x + room2.Width - 1);
            y1 = Random.Range(room1.y, room1.y + room1.Height - 1);
            y2 = Random.Range(room2.y, room2.y + room2.Height - 1);

            if (x1 > x2)
                Mul_X = -1;
            else
                Mul_X = 1;
            if (y1 > y2)
                Mul_Y = 1;
            else
                Mul_Y = -1;

            Hallway_Width = Mathf.Abs(x2 - x1);
            Hallway_Height = Mathf.Abs(y2 - y1);

                    for (int X = 0; Mathf.Abs(Hallway_Width - X) > 0 ; X++)
                if(Tiletag[(x1 + X * Mul_X),(y1)] == "Impassable")
                {
                GameObject Tile = new GameObject("Tile" + (x1 + X * Mul_X) + "," + (y1) );
                Tile.transform.parent = Hallway.transform;
                SpriteRenderer Tsprite = Tile.AddComponent<SpriteRenderer>();
                Tsprite.sprite = MazeFloor.GetComponent<SpriteRenderer>().sprite;
                Tile.transform.position = new Vector3((x1 + X * Mul_X) * Tsprite.bounds.size.x, y1 * Tsprite.bounds.size.y , 5);
                Tiletag[(x1 + X * Mul_X), (y1)] = "Hallway";
                }

                    for (int Y = 0; Mathf.Abs(Hallway_Height - Y) > 0; Y++)
                if (Tiletag[(x1 + Hallway_Width * Mul_X), (y1 - Y * Mul_Y)] == "Impassable")
                {
                GameObject Tile = new GameObject("Tile" + (x1 + Hallway_Width * Mul_X) + "," + (y1 - Y * Mul_Y));
                Tile.transform.parent = Hallway.transform;
                SpriteRenderer Tsprite = Tile.AddComponent<SpriteRenderer>();
                Tsprite.sprite = MazeFloor.GetComponent<SpriteRenderer>().sprite;
                Tile.transform.position = new Vector3((x1 + Hallway_Width * Mul_X) * Tsprite.bounds.size.x, 
                    (y1 - Y * Mul_Y) * Tsprite.bounds.size.y , 5);
                    Tiletag[(x1 + Hallway_Width * Mul_X), (y1 - Y * Mul_Y)] = "Hallway";
                }

        }

        //残された空間をフィル。
        GameObject wallGroup = new GameObject("WallGROUP");
        for (int i = 0; i < FloorWidth; i++)
            for (int j = 0; j < FloorHeight; j++)
            {
                if (Tiletag[i, j] == "Impassable")
                {
                    GameObject wall = Instantiate(Wall, wallGroup.transform) as GameObject;
                    wall.name = Wall.name;
                    wall.transform.parent = wallGroup.transform;
                    float size_X = wall.GetComponent<SpriteRenderer>().bounds.size.x;
                    float size_Y = wall.GetComponent<SpriteRenderer>().bounds.size.y;
                    wall.transform.position = new Vector3(i * size_X, j * size_Y);
                }
            }

        //------------!!--------------//
        //      敵の生成、仮として    //
        //------------!!--------------//
        for (int i = 0; i < EnemNum; i++)
        {
            GameObject Enemy = Enemys[(Random.Range(0,Enemys.Length))];
            while (true)
            {
                int x = Random.Range(0, FloorWidth);
                int y = Random.Range(0, FloorHeight);
                if (Tiletag[x,y] == "Room") {
                    Enemy.transform.position = new Vector3(x * sizeTx, y * sizeTx, 2);
                    Instantiate(Enemy);
                    break;
                    }
            }

        }
    }

    private void Update()
    {
        
    }


    bool CheckDoorPlaceAble(string Tag1, string Tag2, string[,] Floor, int x, int y)
    {
        if ((Floor[x - 1, y] == Tag1 &&
            Floor[x + 1, y] == Tag1) ||
            (Floor[x, y - 1] == Tag2 &&
            Floor[x, y + 1] == Tag2)
            ||
            (Floor[x - 1, y] == Tag2 &&
            Floor[x + 1, y] == Tag2) ||
            (Floor[x, y - 1] == Tag1 &&
            Floor[x, y + 1] == Tag1)
            )
        {
            return true;
        }
        else return false;
    }
    


}

 


class Roomproperty : MonoBehaviour
{
    public int Width, Height, x, y;
    public bool isRoomCollide = true;
}

class Tileproperty : MonoBehaviour
{
    public int x, y;
    public string type;
}