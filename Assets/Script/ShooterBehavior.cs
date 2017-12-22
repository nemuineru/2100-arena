using System.Collections;
using System.Collections.Generic;
using GamepadInput;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(AudioSource), typeof(S_Dir))]
[RequireComponent(typeof(SpriteRenderer), typeof(MouseRotation))]
public class ShooterBehavior : MonoBehaviour {

    Vector3 mouse, playerposition, playerScreenPos;

    public string Name;
    public enum GunType
    {
        Primary,
        Secondary
    }
    [SerializeField] 
    [Space]
    GunType guntype = GunType.Secondary;
    float GunRotate;
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
        public float UseEnergyByAmmo;
        public bool isGunAuto;
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
    GameObject bullet, LazCaster;

    StatusMultiple statMul;


    public Vector3 shotrotate;
    public Vector2 shotD8pos;

    ActorBehavior Actor;

    AudioSource Audio;


    int GunDir, TotalMagcap;
    bool ShotReady = false;
    float Chargetime = 0;

    // Use this for initialization
    IEnumerator Start()
    {
        bool reloadSoundToggle = true;

        if(GameObject.Find("WorldSetting"))
        Cursor.SetCursor(GameObject.Find("WorldSetting").GetComponent<Level>().Cursors,
        new Vector2(16f, 16f), cursorMode: 0);

        statMul = GetComponentInParent<StatusMultiple>();
        TotalMagcap
            = ammo.MagCap
            + statMul.AddMagazine;

        Audio = GetComponent<AudioSource>();
        Actor = transform.root.GetComponent<ActorBehavior>();
        bool isLaser = ammo.Shot.GetComponent<BulletStatus>().Lazer;


        if (Actor.Energy < TotalMagcap * ammo.UseEnergyByAmmo 
            && ammo.UseEnergyByAmmo > 0)
            // もしアクターのエネルギーが満タンに出来ない時
            ammo.RestBullet = Mathf.CeilToInt(Actor.Energy / ammo.UseEnergyByAmmo);
        else
            ammo.RestBullet = TotalMagcap;


        while (true)
        {
            if (Reloading)
            {

                if (sound.Reload_Start != null && Audio.clip != sound.Reload_Start)
                {
                    Audio.clip = sound.Reload_Start;
                    Audio.Play();
                }
                yield return new WaitForSeconds(stat_Time.Reload);
                if (sound.Reload_End != null && Audio.clip != sound.Reload_End)
                {
                    Audio.clip = sound.Reload_End;
                    Audio.Play();
                }

                if (Actor.Energy < ammo.UseEnergyByAmmo
                    && ammo.UseEnergyByAmmo > 0)
                    // もしアクターのエネルギーが満タンに出来ない時
                    ammo.RestBullet = Mathf.CeilToInt(TotalMagcap * (Actor.Energy / ammo.UseEnergyByAmmo));
                else
                    ammo.RestBullet = TotalMagcap;

                Reloading = false;
                yield return null;
            }

            Chargetime = ShootReadyupTime(Chargetime);

            if (Chargetime > 0)
            {
                Debug.Log("Chargetime :" + Chargetime);
            }
            if (stat_Time.WarmUp > 0)
            {
                if (Chargetime == 0.1f && sound.warmUp != null)
                {

                    Audio.clip = sound.warmUp;
                    Audio.Play();
                }

            }

            //発射レディ
            if ((ammo.RestBullet > 0 && Reloading == false) &&
                ((stat_Time.WarmUp > 0 && Chargetime > stat_Time.WarmUp && (
                    (Chargetime > 0 && ammo.isGunAuto) ||
                    (Chargetime == -1 && !ammo.isGunAuto))
                )
                || (stat_Time.WarmUp <= 0 && (
                    (Chargetime > 0 && ammo.isGunAuto) ||
                    (Chargetime == 0.1f && !ammo.isGunAuto))
               ))
               )
            {
                NextShotReady = false;
                if (isLaser && LazCaster == null)//レーザ用に発射口を出す
                {
                    LazCaster = new GameObject("LazCaster");
                    LazCaster.transform.SetParent(transform, false);
                }

                for (int j = 0; j < stat_Number.Repeat + 1; j++)//バースト射撃
                {
                    Actor.Energy -= ammo.UseEnergyByAmmo / stat_Number.Repeat;
                    for (int i = 0; i < stat_Number.Burst + statMul.AddBurst + 1; i++)//追加発射数
                    {
                        Debug.Log("Shot");
                        //Ray Mode:
                        if (isLaser)
                        {
                            shotrotate = (shotD8pos + ammo.Shot.GetComponent<BulletStatus>().bulletunder / 2);
                            bullet = Instantiate(ammo.Shot,
                           shotrotate,
                           Quaternion.Euler(0, 0, (GunRotate + Random.Range
                           (-stat_Number.Accuracy - (stat_Number.RecoilFreq * Chargetime), stat_Number.Accuracy) + (stat_Number.RecoilFreq * Chargetime)))
                           ) as GameObject;
                            if (LazCaster && bullet)
                                bullet.transform.SetParent(LazCaster.transform, false);
                            Debug.Log("!");
                        }
                        //Bullet Mode:
                        else
                        {
                            shotrotate = this.transform.position + Quaternion.Euler(0, 0, GunRotate) *
                            (shotD8pos + ammo.Shot.GetComponent<BulletStatus>().bulletunder / 2);
                            bullet = Instantiate(ammo.Shot, shotrotate,
                            Quaternion.Euler(0, 0,( GunRotate + Random.Range
                           (-stat_Number.Accuracy - (stat_Number.RecoilFreq * Chargetime), stat_Number.Accuracy) + (stat_Number.RecoilFreq * Chargetime)))
                           ) as GameObject;
                            Debug.Log("!");
                        }
                    }
                    Instantiate(ammo.splash, this.transform.position + Quaternion.Euler(0, 0, GunRotate) * shotD8pos, Quaternion.Euler(0, 0, GunRotate), parent: transform);
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
                NextShotReady = true;
            yield return null;

            if ((Actor.Energy >= ammo.UseEnergyByAmmo) &&
                (ammo.RestBullet <= 0 && Chargetime == 0.1f) ||
                (Reload_Pressed() && ammo.RestBullet < TotalMagcap)
                )
            {
                Reloading = true;
            }
            
            yield return null;
        }
    }

    
    


    // Update is called once per frame
    void Update()
    {
        TotalMagcap
            = ammo.MagCap
            + statMul.AddMagazine;

        var m = GetComponent<MouseRotation>();
        GunRotate = m.RotfromUp;
        GunDir = GetComponent<S_Dir>().Rot2Num8Dir(m.RotfromThistoMouse + 90 * System.Convert.ToInt32(Reloading));
        GetComponent<Animator>().runtimeAnimatorController = Resources.Load("Weapon/Gun/PlayerGun/" + gameObject.name + "/Animation/Direction/" + GunDir) as RuntimeAnimatorController;
        switch (GunDir)
        {
            case 2:
            case 3:
            case 6:
            case 9:
                transform.position = new Vector3(transform.position.x, transform.position.y, -1);
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
            if ((!(ShootReadyupTime(Chargetime) > 0 || !ammo.isGunAuto || Reloading
                || ammo.RestBullet <= 0) && NextShotReady))
            {
                LazCaster.transform.DetachChildren();
                Destroy(LazCaster);
            }
        }

    }

    float ShootReadyupTime(float i)
    {
        var Joystic = Input.GetJoystickNames();
        if (((GamePad.GetButton(GamePad.Button.LeftShoulder1, GamePad.Index.Any)) ||
            (GamePad.GetButton(GamePad.Button.LeftShoulder2, GamePad.Index.Any)) ||
            (GamePad.GetButton(GamePad.Button.RightShoulder1, GamePad.Index.Any)) ||
          ((GamePad.GetButton(GamePad.Button.RightShoulder2, GamePad.Index.Any)))) || (
          Joystic.Length == 0 && Input.GetMouseButton(button: 0))
          )
        {
            if (i <= 0)
            {
                i = 0.1f;
            }
            else
            {
                i += 0.1f;
            }
        }
        else
        {
            if (i > 0)
                i = -1;
            else
                i = 0;//押して無いときはチャージしない。  
        }
        return i;
    }

    bool Reload_Pressed()
    {
        var Joystic = Input.GetJoystickNames();
        if ((
        GamePad.GetButtonDown(GamePad.Button.X, GamePad.Index.Any) ||
        GamePad.GetButtonDown(GamePad.Button.Y, GamePad.Index.Any)
        ) || (Joystic.Length == 0 && Input.GetKeyDown(KeyCode.R)))
            return true;
        else
            return false;
    }
    
}
