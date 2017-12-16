using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator),typeof(AudioSource), typeof(S_Dir))]
[RequireComponent(typeof(SpriteRenderer), typeof(EnemytoPlayerRotation))]
public class EnemyGunBehavior : MonoBehaviour {

    Vector3 mouse, playerposition, playerScreenPos;

    [Range(0,360)]
    public float aimspeed, precizeaimspeed, GunDangerArea;
    [Header("Sounds")]
    public Sound sound;
    [Header("Ammo")]
    public Ammo ammo;
    [Header("Statistic:Time")]
    public GunStat_Time stat_Time;
    [Header("Statistic:Number")]
    public GunStat_Number stat_Number;
    public Sprite WeaponIcon;

    [System.Serializable]
    public class Sound
    {
        public AudioClip Shot, Reload_Start, Reload_End, warmUp, Reload_Feed;
    }
    [System.Serializable]
    public class Ammo
    {
        public GameObject Shot, splash;
        public int RestBullet, MagCap;
        public float UseEnergy;
        public bool isGunAuto,isGunShooting, isAimed;
    }
    [System.Serializable]
    public class GunStat_Time
    {
        public float Cooling, Wait, Reload, WarmUp;
    }
    [System.Serializable]
    public class GunStat_Number
    {
        public int Burst, Repeat;
       public float Accuracy, RecoilFreq;
    }

    public bool Reloading, NextShotReady = true;

    public float  GunRotate , Shotpressedtime, GunRange;
    
    public Vector3 shotrotate;

    public Vector2 shotD8pos;


    GameObject bullet, LazCaster;
    int GunDir;
    bool ShotReady = false;
    float Chargetime = 0;

    // Use this for initialization
    IEnumerator Start()
    {
        ammo.RestBullet = 0; 
        AudioSource Audio = GetComponent<AudioSource>();
        bool lazstyle = ammo.Shot.GetComponent<BulletStatus>().Lazer;
        while (true)

        {
            if (ammo.isGunShooting)//チャージスタート
            {
                if (Chargetime == 0 && sound.warmUp != null)
                {
                    Audio.clip = sound.warmUp;
                    Audio.Play();
                }
                Chargetime += Time.deltaTime;
            }
            else
                Chargetime = 0;//押して無いときはチャージしない。

            if (ammo.RestBullet > 0 && ammo.isAimed)
            {
                Shotpressedtime += Time.deltaTime;
                ammo.isGunShooting = true;
            }
            else {
                Shotpressedtime = 0;
                ammo.isGunShooting = false;
                 }

            if (Chargetime > stat_Time.WarmUp || stat_Time.WarmUp <= 0)//もしチャージタイムがワームアップ以上の時
            {
                //発射レディ
                if (
                    (ammo.RestBullet > 0) &&
                    ((ammo.isGunAuto && ammo.isGunShooting) || //もしオートで発射時
                    (!ammo.isGunAuto && Shotpressedtime > 0) || //セミオートで発射
                    (!ammo.isGunAuto && Shotpressedtime < 0 && stat_Time.WarmUp > 0) //ウォームアップタイムが存在している
                    ))
                {
                    NextShotReady = false;
                    if (lazstyle && LazCaster == null)//レーザ用に発射口を出す
                    {
                        LazCaster = new GameObject("LazCaster");
                        LazCaster.transform.SetParent(transform, false);
                    }
                    for (int j = 0; j < stat_Number.Repeat; j++)//バースト射撃
                    {
                        for (int i = 0; i < stat_Number.Burst; i++)//同時発射数
                        {
                            //Ray Mode:
                            if (lazstyle)
                            {
                                shotrotate = (shotD8pos + ammo.Shot.GetComponent<BulletStatus>().bulletunder / 2);
                                bullet = Instantiate(ammo.Shot,
                               shotrotate,
                               Quaternion.Euler(0, 0, Random.Range(-stat_Number.Accuracy, stat_Number.Accuracy))
                               ) as GameObject;
                                if (LazCaster && bullet)
                                    bullet.transform.SetParent(LazCaster.transform, false);
                            }
                            //Bullet Mode:
                            else
                            {
                                shotrotate = this.transform.position + Quaternion.Euler(0, 0, GunRotate) * 
                                    (shotD8pos + ammo.Shot.GetComponent<BulletStatus>().bulletunder / 2);
                                bullet = Instantiate(ammo.Shot, shotrotate,
                                Quaternion.Euler(0, 0, GunRotate + Random.Range(-stat_Number.Accuracy, stat_Number.Accuracy))) as GameObject;
                            }
                        }
                        Instantiate(ammo.splash, this.transform.position + Quaternion.Euler(0, 0, GunRotate) *
                            shotD8pos, Quaternion.Euler(0, 0, GunRotate), parent: transform);
                        //発射時マズルフラッシュを出す
                        if (sound.Shot != null)
                        {
                            Audio.clip = sound.Shot;
                            Audio.Play();//ショットサウンド…
                        }
                        --ammo.RestBullet; if (ammo.RestBullet <= 0)
                            break;
                        yield return new WaitForSeconds(stat_Time.Cooling);
                    }
                    yield return new WaitForSeconds(stat_Time.Wait);
                }
                else
                yield return null;

            }
            NextShotReady = true;
            if (ammo.RestBullet <= 0)
            {
                Reloading = true;
                if (sound.Reload_Start != null)
                {
                    Audio.clip = sound.Reload_Start;
                    Audio.Play();
                }
                yield return new WaitForSeconds(stat_Time.Reload);
                if (sound.Reload_End != null)
                    AudioSource.PlayClipAtPoint(sound.Reload_End, transform.position, 1f);
                ammo.RestBullet = ammo.MagCap;
                Reloading = false;
            }
            yield return null;
        }
    }

    GameObject ray;
    RaycastHit2D hit2d;

    // Update is called once per frame
    void Update ()
    {
        playerposition = GameObject.FindGameObjectWithTag("Player").transform.position;
        

        hit2d = Physics2D.Linecast(transform.position , playerposition
            , GetLayer("Terrain") + GetLayer("Player") );

        Debug.DrawLine(transform.position, hit2d.point, Color.green);

        var E_r = GetComponent<EnemytoPlayerRotation>();

        float Rangeprec = (Mathf.Sin(Mathf.Deg2Rad * (E_r.RotfromUp - GunRotate)));
        if (Mathf.Abs(Rangeprec) > Mathf.Deg2Rad * GunDangerArea || Mathf.Abs(Vector2.Distance
            (transform.position, GameObject.FindGameObjectWithTag("Player")
            .transform.position)) > GunRange )

        {
                ammo.isAimed = false;

            if (Rangeprec < 0)
                GunRotate += aimspeed;
            else if (Rangeprec > 0)
                GunRotate -= aimspeed;
        }
        else
        {
            if (hit2d.collider.tag == "Player")
                ammo.isAimed = true;
            if (Rangeprec < 0)
                GunRotate += aimspeed / 100;
            else if (Rangeprec > 0)
                GunRotate -= aimspeed / 100;
        }
        
        
        GunDir = GetComponent<S_Dir>().Rot2Num8Dir(E_r.RotfromThistoPlayer + 90
            * System.Convert.ToInt32(stat_Time.Reload));
        GetComponent<Animator>().runtimeAnimatorController = Resources.Load
            ("Weapon/EnemyGun/" + this.name + "/Animation/Direction/" + GunDir)
            as RuntimeAnimatorController;
        switch (GunDir)
        {
            case 2:
            case 3:
            case 6:
            case 9:
                transform.position = new Vector3(transform.position.x , transform.position.y, -1);
                break;
            default:
               transform.position = new Vector3(transform.position.x, transform.position.y, 1);
                break;
        }

        //Ray Mode:
        if (LazCaster)
        {
            LazCaster.transform.rotation = Quaternion.Euler(0, 0, GunRotate);
            //LazCaster.transform.rotation = Quaternion.Euler(0, 0, GunRotate);
            if ((!ammo.isGunShooting || !ammo.isGunAuto || Reloading
                || ammo.RestBullet <= 0) && NextShotReady)
            {
                LazCaster.transform.DetachChildren();
                Destroy(LazCaster);
            }
        }

    }

    int GetLayer(string Name)
    {
        return LayerMask.GetMask(Name);
    }
}
