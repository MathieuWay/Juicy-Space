using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public int direction;
    public bool isPlayerShot;
    private void FixedUpdate()
    {
        Vector3 dir = Vector3.zero;
        if (direction > 0)
            dir = Vector3.up;
        else
            dir = Vector3.down;

        transform.Translate(dir * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        if(collision.gameObject.tag == "Player")
        {
            //-1 life
            Debug.Log("Une vie perdu");
            Player.instance.HitPlayer();
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "invader" && isPlayerShot)
        {
            //Invaders.instance.DestroyInvader(collision.gameObject);
            collision.gameObject.GetComponent<Invader>().DestroyInvader();
            Debug.Log("destroy");
            Destroy(gameObject);
        }
    }
}
