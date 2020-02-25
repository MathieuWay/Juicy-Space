using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float range;
    public float offset;
    public GameObject bulletPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletPrefabs, transform.position + transform.up * offset, Quaternion.identity);
        }
    }
}
