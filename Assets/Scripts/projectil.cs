using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectil : MonoBehaviour
{
    private float totalCount;
    public float lifeTime;
    private float count;
    public float delta;

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        totalCount += Time.deltaTime;
        if (count > delta)
        {
            count = 0;
            transform.Translate(new Vector3(0, -0.1f, 0));
        }
        if (totalCount > lifeTime)
        {
            Destroy(this);
        }
    }
}
