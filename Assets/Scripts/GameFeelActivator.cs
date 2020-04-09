using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFeelActivator : MonoBehaviour
{
    public static GameFeelActivator instance;
    public bool BeatBarre;
    public bool FreqPosPass;
    public bool ColorPos;
    public bool ParticuleFlag;
    public bool ShootQuality;
    public bool PlayerAnimShoot;
    public bool InvaderDeadParticule;
    public bool PostProc;

    public GameObject[] Particules;


    private void Awake()
    {
        instance = this;
        SetStateParticules(ParticuleFlag);
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

        if (Input.GetKeyDown(KeyCode.Alpha1)) PostProc = !PostProc;
        if (Input.GetKeyDown(KeyCode.Alpha2)) ParticuleFlag = !ParticuleFlag;
        if (Input.GetKeyDown(KeyCode.Alpha3)) ColorPos = !ColorPos;
        if (Input.GetKeyDown(KeyCode.Alpha4)) FreqPosPass = !FreqPosPass;
        if (Input.GetKeyDown(KeyCode.Alpha5)) ShootQuality = !ShootQuality;
        if (Input.GetKeyDown(KeyCode.Alpha6)) PlayerAnimShoot = !PlayerAnimShoot;
        if (Input.GetKeyDown(KeyCode.Alpha7)) InvaderDeadParticule = !InvaderDeadParticule;
        if (Input.GetKeyDown(KeyCode.Alpha8)) BeatBarre = !BeatBarre;
        if (Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);
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
