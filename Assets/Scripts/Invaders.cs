using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    public static Invaders instance;
    private GameObject[][] invadersArray;
    public int column;
    public int raw;
    public GameObject invader;
    public GameObject projectil;
    public GameObject projectilV2;
    public int offsetI;
    public int offsetJ;
    private int invadersCount;
    private int sens;
    private bool end;
    private float count;
    private float countTir;
    public float deltaTir;
    public float deltaDeplacement;
    private List<GameObject> shooters;
    private GameObject newShooter;

    public bool started = false;

    //SOUND
    public AudioSource source;
    public AudioClip clipShoot;

    private void Awake()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        count = 0.0f;
        sens = 1;
        invadersCount = column * raw; //Changer quand touché
        Initialisation();
        shooters = new List<GameObject>();

        for (int i = 0; i < invadersArray.Length; i++)
        {
            shooters.Add(invadersArray[i][0]); //Changer quand touché!!
        }
    }


    void Update()
    {
        if (started)
        {
            count += Time.deltaTime;
            countTir += Time.deltaTime;
            if (count > deltaDeplacement)
            {
                count = 0;
                if (!end)
                {
                    for (int i = 0; i < invadersArray.Length; i++)
                    {
                        for (int j = 0; j < invadersArray[i].Length; j++)
                        {
                            if (invadersArray[i][j] != null)
                            {
                                invadersArray[i][j].transform.position = new Vector3(invadersArray[i][j].transform.position.x + (1 * sens), invadersArray[i][j].transform.position.y, invadersArray[i][j].transform.position.z);
                                if ((invadersArray[i][j].transform.position.x == 8 && sens == 1) || (invadersArray[i][j].transform.position.x == -8 && sens == -1))
                                {
                                    end = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    ChangeLine();
                }
            }
            if (countTir > deltaTir && invadersCount > 0)
            {
                countTir = 0;
                int random = Random.Range(0, shooters.Count - 1);
                tir(shooters[random].transform.position);

            }
        }
    }

    private void Initialisation()
    {
        invadersArray = new GameObject[column][];
        for (int i = 0; i < invadersArray.Length; i++)
        {
            invadersArray[i] = new GameObject[raw];
            for (int j = 0; j < invadersArray[i].Length; j++)
            {
                invadersArray[i][j] = Instantiate(invader, new Vector3(i - offsetI, j - offsetJ, 0), Quaternion.identity); ;
            }
        }
    }

    private void ChangeLine()
    {
        Debug.Log("Change Line");
        end = false;
        sens *= -1;
        for (int i = 0; i < invadersArray.Length; i++)
        {
            for (int j = 0; j < invadersArray[i].Length; j++)
            {
                Debug.Log(i + " " + j + invadersArray[i][j]);
                if (invadersArray[i][j] != null)
                {
                    invadersArray[i][j].transform.position = new Vector3(invadersArray[i][j].transform.position.x, invadersArray[i][j].transform.position.y - 1, invadersArray[i][j].transform.position.z);
                    if (invadersArray[i][j].transform.position.y <= -4)
                    {
                        Player.gameOver();
                    }
                }
            }
        }
    }

    private void tir(Vector3 position)
    {
        source.clip = clipShoot;
        source.Play();
        Instantiate(GameFeelActivator.instance.ShootQuality ? projectilV2 : projectil, position,Quaternion.identity);
    }

    public void DestroyInvader(GameObject invader)
    {
        for (int i = 0; i < invadersArray.Length; i++)
        {
            for (int j = 0; j < invadersArray[i].Length; j++)
            {
                if (invadersArray[i][j]==invader)
                {
                    invadersArray[i][j] = null;
                    if (j<invadersArray[i].Length - 1)
                    {
                        newShooter = invadersArray[i][j + 1];
                    }
                    else
                    {
                        newShooter = null;
                    }
                }
            }
        }
        for (int i = 0; i < shooters.Count; i++)
        {
            if (shooters[i] == invader)
            {
                shooters.RemoveAt(i);
            }
        }
        if (newShooter != null)
        {
            shooters.Add(newShooter);
        }

        Destroy(invader);
        invadersCount--;

    }
}
