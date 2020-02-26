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

    public delegate void OnGameOverByPlayerDead();
    public static OnGameOverByPlayerDead gameOver;

    private void Awake()
    {
        instance = this;
        gameOver += GameOver;
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefabs, transform.position + transform.up * offset, Quaternion.identity);
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
