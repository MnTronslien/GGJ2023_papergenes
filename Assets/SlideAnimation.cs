using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAnimation : MonoBehaviour
{
    public AnimationClip clip;
    [Range(0,1)] public float percent;

    // Update is called once per frame
    void Update()
    {
        clip.SampleAnimation(gameObject, clip.length * percent);
    }
    void OnValidate()
    {
        Update();
    }
}
