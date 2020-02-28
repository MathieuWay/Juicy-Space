using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public float speed;
    public float range;
    public float offset;
    public GameObject bulletPrefabs;
    public int lifeLeft;
    public float posNorm;
    public int freqPos;

    private AudioSource source;
    private AudioLowPassFilter lowpass;
    private AudioHighPassFilter highpass;
    public int freqRange = 5000;

    public AudioClip clip;

    public delegate void OnGameOverByPlayerDead();
    public static OnGameOverByPlayerDead gameOver;

    private void Awake()
    {
        instance = this;
        gameOver += GameOver;
        source = GetComponent<AudioSource>();

        highpass = GetComponent<AudioHighPassFilter>();
        lowpass = GetComponent<AudioLowPassFilter>();

        source.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameFeelActivator.instance.FreqPosPass)
        {
            posNorm = Mathf.Abs(-range / 2 + transform.position.x) / range;
            freqPos = (int)(22000 * posNorm) + 10;
            int highpassPos = freqPos - freqRange;
            if (highpassPos < 10)
                highpassPos = 10;
            highpass.cutoffFrequency = highpassPos;

            int lowpassPos = freqPos + freqRange;
            if (lowpassPos > 22000)
                lowpassPos = 22000;
            lowpass.cutoffFrequency = lowpassPos;
        }

        //GO RIGHT
        if (Input.GetKey(KeyCode.RightArrow))
        {
            float nextPos = transform.position.x + speed * Time.deltaTime;
            if(nextPos < range/2)
                transform.Translate(transform.right * speed * Time.deltaTime);
            else if(transform.position.x > range / 2)
            {
                Vector3 pos = transform.position;
                pos.x = range / 2;
                transform.position = pos;
            }
        }

        //GO LEFT
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            float nextPos = transform.position.x - speed * Time.deltaTime;
            if (nextPos > -range / 2)
                transform.Translate(-transform.right * speed * Time.deltaTime);
            else if (transform.position.x < -range / 2)
            {
                Vector3 pos = transform.position;
                pos.x = -range / 2;
                transform.position = pos;
            }
        }

        //Shoot
        if (Input.GetKeyDown(KeyCode.Space) && Invaders.instance.started)
        {
            source.Play();
            Instantiate(bulletPrefabs, transform.position + Vector3.up * offset, Quaternion.identity);
        }
    }

    public void HitPlayer()
    {
        lifeLeft--;
        if(lifeLeft <= 0)
        {
            if(gameOver != null)
                gameOver();
        }
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
    }
}
