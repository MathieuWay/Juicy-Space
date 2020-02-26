using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConductorCustom : MonoBehaviour
{

    //song completion
    public delegate void SongCompletedAction();
    public static event SongCompletedAction songCompletedEvent;

    private float songLength;

    //if the whole game is paused
    public bool paused = true;
    private bool songStarted = false;

    public static float pauseTimeStamp = -1f; //negative means not managed
    private float pausedTime = 0f;

    //private SongInfo songInfo;

    //current song position
    public static float songposition;

    //how many seconds for each beat
    public static float crotchet;

    private float dsptimesong;

    public static float BeatsShownOnScreen = 4f;

    public int moveBPM;
    public int shootBPM;

    //count down canvas
    private const int StartCountDown = 3;
    public GameObject countDownCanvas;
    public Text countDownText;

    public int bpm;
    public AudioClip clip1;
    public AudioClip clip2;


    public AudioSource audioSource1;
    public AudioSource audioSource2;

    //Get Instance
    private static ConductorCustom instance;
    public static ConductorCustom Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<ConductorCustom>();
            if (instance == null)
                Debug.Log("No tutorial found");
            return instance;
        }
    }

    void Start()
    {
        //pausedTime += (float)AudioSettings.dspTime - pauseTimeStamp;
        audioSource1 = GetComponent<AudioSource>();
        //reset static variables
        paused = true;
        pauseTimeStamp = -1f;

        //display countdown canvas
        countDownCanvas.SetActive(true);

        //get the song info from messenger
        //songInfo = SongInfoCustom.Instance.currentSong;

        //initialize fields
        crotchet = 60f / bpm;

        songLength = clip1.length;

        //initialize audioSource
        audioSource1.clip = clip1;
        audioSource2.clip = clip2;

        //start countdown
        StartCoroutine(CountDown());
    }

    void StartSong()
    {
        //get dsptime
        dsptimesong = (float)AudioSettings.dspTime;

        //play song
        audioSource1.Play();
        audioSource2.Play();

        //unpause
        paused = false;
        songStarted = true;
        Invaders.instance.started = true;
        Invaders.instance.deltaDeplacement = crotchet * moveBPM;
        Invaders.instance.deltaTir = crotchet * shootBPM;
    }

    public void pause()
    {
        paused = !paused;
    }

    public float GetPausedTime()
    {
        return pausedTime;
    }

    public float GetdspTime()
    {
        return dsptimesong;
    }

    public void SetDspTime(float time)
    {
        dsptimesong = time;
    }

    public bool GetSongStarted()
    {
        return songStarted;
    }

    public void SetSongStarted(bool state)
    {
        songStarted = state;
    }

    void Update()
    {
        //for count down
        if (!songStarted) return;

        //for pausing
        if (paused)
        {
            if (pauseTimeStamp < 0f) //not managed
            {
                pauseTimeStamp = (float)AudioSettings.dspTime;
                audioSource1.Pause();
            }

            return;
        }
        else if (pauseTimeStamp > 0f) //resume not managed
        {
            pausedTime += (float)AudioSettings.dspTime - pauseTimeStamp;
            audioSource1.Play();

            pauseTimeStamp = -1f;
        }

        //calculate songposition
        songposition = (float)(AudioSettings.dspTime - dsptimesong - pausedTime) * audioSource1.pitch;

        //check to see if the song reaches its end
        if (songposition > songLength)
        {
            songStarted = false;

            if (songCompletedEvent != null)
                songCompletedEvent();
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1f);

        for (int i = StartCountDown; i >= 1; i--)
        {
            countDownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }
        countDownCanvas.SetActive(false);

        StartSong();
    }

    public float GetSongPosition()
    {
        return (float)(AudioSettings.dspTime - dsptimesong - pausedTime) * audioSource1.pitch;
    }

    public float GetCrochet()
    {
        return crotchet;
    }

    public float GetBeatToShowOnScreen()
    {
        return BeatsShownOnScreen;
    }

    public void RestartSong()
    {
        audioSource1.time = 0f;
        dsptimesong = (float)AudioSettings.dspTime;
        audioSource1.Play();
        paused = false;
        songStarted = true;
    }
}