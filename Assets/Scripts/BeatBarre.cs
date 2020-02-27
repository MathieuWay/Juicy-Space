using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatBarre : MonoBehaviour
{
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        ConductorCustom.beat += OnBeat;
    }

    private void OnDestroy()
    {
        ConductorCustom.beat -= OnBeat;
    }

    public void OnBeat(int beatCount, float crochet)
    {
        if(beatCount % 2 == 1)
            StartCoroutine(delayBeat(crochet));
    }

    IEnumerator delayBeat(float crochet)
    {
        yield return new WaitForSeconds(crochet - 5/60);
        anim.Play("boom", 0, 0);
    }
}
