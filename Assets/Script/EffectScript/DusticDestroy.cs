using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DusticDestroy : MonoBehaviour {
    public GameObject Explod;
    public AudioClip audio;
    public float Destroytime;

    public float i = 0;
    // Use this for initialization

    IEnumerator Start ()
    {
        gameObject.AddComponent<AudioSource>().PlayOneShot(audio);
        while (true)
        {
            for (int x = 0; x < 3; x++)
            {
                GameObject Exp = Instantiate(Explod,transform);
                Rigidbody2D Rigid = Exp.GetComponent<Rigidbody2D>();
                Rigid.AddForce(new Vector2
                (Random.Range(5, -5) * 3000, Random.Range(5, -5) * 3000));
                yield return new
                WaitForSeconds(0.1f);

            }
            if (i > Destroytime)
                {
                    break;
                }
                else
                {
                    i += Time.deltaTime;
                }
        }
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
