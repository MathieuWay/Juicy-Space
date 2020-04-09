using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessActivator : MonoBehaviour
{
    private PostProcessLayer postProcessLayer;
    private void Start()
    {
        postProcessLayer = GetComponent<PostProcessLayer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (GameFeelActivator.instance.PostProc != postProcessLayer.enabled)
        {
            postProcessLayer.enabled = GameFeelActivator.instance.PostProc;
        }
    }
}
