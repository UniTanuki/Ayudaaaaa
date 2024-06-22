using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public float beatTempo;
    public float huevox;
 

    public GameObject HitEffect, goodEffect, perfectEffect, missEffect;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))

        {

            if (canBePressed)
            {
                gameObject.SetActive(false);
                Debug.Log(huevox);


                if (huevox > -2.2 && huevox < -2.04)
                {
                    Debug.Log("Perfect");
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                else if (huevox > -2.3 && huevox < -1.8)
                {
                    Debug.Log("Hit");
                    GameManager.instance.NormalHit();
                    Instantiate(HitEffect, transform.position, HitEffect.transform.rotation);
                }
                else if (huevox > -2.5 && huevox < -1.6)
                {
                    Debug.Log("Good");
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
               
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            canBePressed = true;
            huevox=other.transform.position.x+transform.position.x+12.5f;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator" && gameObject.activeSelf)
        {
            canBePressed = false;

            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, new Quaternion (0f, 0f, 0f, 0f));
           
        }
    }
}
