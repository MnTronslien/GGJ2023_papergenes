using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideAnimation : MonoBehaviour
{
    public AnimationClip clip;
    [Range(0,1)] public float precent;

    // Update is called once per frame
    void Update()
    {
        clip.SampleAnimation(gameObject, clip.length * precent);
    }
    void OnValidate()
    {
        Update();
    }
}
