using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public AudioClip diyngClip;
    private AudioSource source;
    private Animator anim;
    public GameObject particles;

    private void Start()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
        source.clip = diyngClip;
    }
     
    public void DestroyInvader()
    {
        if (GameFeelActivator.instance.InvaderDeadParticule)
        {
            GameObject particle = Instantiate(particles, transform.position, Quaternion.identity);
        }
        Invaders.instance.DestroyInvader(gameObject);
        //Destroy(gameObject);
        //StartCoroutine("Die");
    } 
    public IEnumerator Die()
    {
        source.Play();
        anim.SetTrigger("die");
        //foreach(var clipinfo in anim.GetCurrentAnimatorClipInfo(0)) Debug.Log(clipinfo.clip.name);
        yield return new WaitForSeconds(1);
        Invaders.instance.DestroyInvader(gameObject);
    }
}
