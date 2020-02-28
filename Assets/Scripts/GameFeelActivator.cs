using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeelActivator : MonoBehaviour
{
    public static GameFeelActivator instance;
    public bool BeatBarre;
    public bool FreqPosPass;
    public bool ColorPos;
    public bool ParticuleFlag;
    public GameObject[] Particules;


    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Particules.Length > 0)
        {
            if (ParticuleFlag != Particules[0].activeSelf)
            {
                SetStateParticules(ParticuleFlag);
            }
        }
    }

    private void SetStateParticules(bool state)
    {
        foreach (GameObject particuleGameObject in Particules)
        {
            particuleGameObject.SetActive(state);
        }
    }
}
