using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AnimationWeightTransition : MonoBehaviour
{
    private float transitionTime = 2f;
    private float currentTime;
    private Animator animator;
    private int layerIndex;
    private void Awake()
    {
        currentTime = transitionTime;
    }
    private void Update()
    {
        if (currentTime > 0)
        {
            animator.SetLayerWeight(layerIndex, currentTime);
            currentTime -= Time.deltaTime;
        }
    }
    public void LayerTransition(Animator animator, int layerIndex)
    {
        this.animator = animator;
        this.layerIndex = layerIndex;
    }
}
