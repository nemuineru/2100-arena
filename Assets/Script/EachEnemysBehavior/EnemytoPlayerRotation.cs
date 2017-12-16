using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemytoPlayerRotation : MonoBehaviour
{
    public float RotfromThistoPlayer, RotfromUp;
    float dirX, dirY;
    Vector3 playerPosition, enemyPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        enemyPosition = GetComponentInParent<Transform>().transform.position;
        playerPosition.z = 10f;

        dirX = (enemyPosition.x - playerPosition.x); dirY = (enemyPosition.y - playerPosition.y);

        RotfromThistoPlayer = (Mathf.Atan2(dirY, dirX) * Mathf.Rad2Deg + 360) % 360;
        RotfromUp = (RotfromThistoPlayer - 90 + 360) % 360;
    }
}
