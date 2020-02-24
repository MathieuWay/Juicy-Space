using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invaders : MonoBehaviour
{
    private GameObject[][] invadersArray;
    public int column;
    public int raw;
    public GameObject invader;
    public GameObject projectil;
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
                        invadersArray[i][j].transform.position = new Vector3(invadersArray[i][j].transform.position.x + (1 * sens), invadersArray[i][j].transform.position.y, invadersArray[i][j].transform.position.z);
                        if ((invadersArray[i][j].transform.position.x == 6 && sens == 1) || (invadersArray[i][j].transform.position.x == -6 && sens == -1))
                        {
                            end = true;
                        }
                    }
                }
            }
            else
            {
                ChangeLine();
            }
        }
        if (countTir > deltaTir)
        {
            countTir = 0;
            int random=Random.Range(0, shooters.Count - 1);
            tir(shooters[random].transform.position);
            
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
        end = false;
        sens *= -1;
        for (int i = 0; i < invadersArray.Length; i++)
        {
            for (int j = 0; j < invadersArray[i].Length; j++)
            {
                invadersArray[i][j].transform.position = new Vector3(invadersArray[i][j].transform.position.x, invadersArray[i][j].transform.position.y-1, invadersArray[i][j].transform.position.z);
            }
        }
    }

    private void tir(Vector3 position)
    {
        Instantiate(projectil, position,Quaternion.identity);
    }

}
