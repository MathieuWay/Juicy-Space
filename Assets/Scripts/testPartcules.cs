using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testPartcules : MonoBehaviour
{
    private bool done;
    public Color colorRight; 
    public Color colorLeft;  
    private float r;
    private float g;
    private float b;
    private float posR;
    private float posG;
    private float posB;
    void Start()
    {
        //Debug.Log("right : "+ colorRight);
        //Debug.Log("left : " + colorLeft);
        if (colorRight.r < colorLeft.r)
        {
            r = colorRight.r;
        }
        else
        {
            r = colorLeft.r;
        }
        if (colorRight.g < colorLeft.g)
        {
            g = colorRight.g;
        }
        else
        {
            g = colorLeft.g;
        }
        if (colorRight.b < colorLeft.b)
        {
            b = colorRight.b;
        }
        else
        {
            b = colorLeft.b;
        }
        gameObject.GetComponent<SpriteRenderer>().color = new Color((colorLeft.r+colorRight.r)/2, (colorLeft.g + colorRight.g) / 2, (colorLeft.b + colorRight.b) / 2);
    }

    void Update()
    {
        gameObject.GetComponent<SpriteRenderer>().color = transformPositionToColor(Player.instance.transform.position.x);
        if (Input.GetKeyDown(KeyCode.A) && !done)
        {
            done = true;
            StartCoroutine(boom());
        }
    }

    IEnumerator boom()
    {
        transform.Translate(new Vector3(0.3f, 0, 0));
        yield return new WaitForSeconds(0.2f);
        transform.Translate(new Vector3(- 0.3f,0, 0));
        done = false;
    }

    private Color transformPositionToColor(float pos)
    {
        
        pos += Player.instance.range / 2;
        pos = pos / Player.instance.range*100;
        if (colorRight.r < colorLeft.r)
        {
            posR = 100-pos;
        }
        else
        {
            posR = pos;
        }
        if (colorRight.g < colorLeft.g)
        {
            posG = 100-pos;
        }
        else
        {
            posG = pos;
        }
        if (colorRight.b < colorLeft.b)
        {
            posB = 100-pos;
        }
        else
        {
            posB = pos;
        }
        //Debug.Log("position : " + pos);
        Color col = new Color(r+(Mathf.Abs(colorLeft.r - colorRight.r) / 100 * posR), g+ (Mathf.Abs(colorLeft.g - colorRight.g) / 100 * posG), b + (Mathf.Abs(colorLeft.b - colorRight.b) / 100 * posB));
        col.a = 1;
        //Debug.Log(col);
        return col;

    }
}
