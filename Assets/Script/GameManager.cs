using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public AudioSource theMusic;
    public bool startPlaying;
    public BeatScroller theBS;
    public static GameManager instance;
    public int currentScore;
    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;
    public Text scoreText;
    public Text multiText;
    public int currentMultiplier;
    public int MultiplierTracker;
    public int[] multiplierThresholds;

    public float totalNotes;
    public float normalHits;
    public float goodHits;
    public float perfectHits;
    public float missedHits;

    public bool HIt = false;
    public bool MIss = false;
    public Animator animator;
    public GameObject jugador;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        //animator= GetComponent<Animator>();
        animator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                theBS.hasStarted = true;
                MIss=false;
                animator.SetBool("HIts", !MIss);
                

                theMusic.Play();
            }
        }
    }

    public void NoteHit()
    {
        Debug.Log("Hit on Time");

        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            MultiplierTracker++;


            if (multiplierThresholds[currentMultiplier - 1] <= MultiplierTracker)
            {
                MultiplierTracker = 0;
                currentMultiplier++;
            }
        }

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
        multiText.text = "Multiplier: x" + currentMultiplier;
        
    }

    public void NormalHit() 
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
        MIss = false;
        animator.SetBool("HIts", !MIss);
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
        MIss = false;
        animator.SetBool("HIts", !MIss);
    }

    public void PerfectHit() 
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
        MIss = false;
        animator.SetBool("HIts", !MIss);
    }

    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        currentMultiplier = 1;
        MultiplierTracker = 0;
        multiText.text = "Multiplier: x" + currentMultiplier;
        HIt = false;
        animator.SetBool("HIts", HIt);
    }

}
