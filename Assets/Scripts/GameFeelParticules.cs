using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeelParticules : MonoBehaviour
{
    private ParticleSystem particle;
    private bool localFlag;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();    
    }
    private void Update()
    {
        if (GameFeelActivator.instance.ParticuleFlag && !localFlag)
        {
            //particle.Play();
        }
        else
        {
            particle.Stop();
        }
        localFlag = GameFeelActivator.instance.ParticuleFlag;
    }
}
