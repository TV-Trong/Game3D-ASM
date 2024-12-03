using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem slashEffect;
    [SerializeField] private ParticleSystem slashEffect2;
    [SerializeField] private ParticleSystem slashEffect3;
    [SerializeField] private ParticleSystem slashEffect4;
    [SerializeField] private GameObject slashObject;
    [SerializeField] private GameObject slashObject2;
    [SerializeField] private GameObject slashObject3;
    [SerializeField] private GameObject slashObject4;
    private Transform originTransform;
    private Transform originTransform2;
    private Transform originTransform3;
    private Transform originTransform4;
    [SerializeField] private Transform targetTransform;

    private void Awake()
    {
        originTransform = slashObject.transform;
        originTransform2 = slashObject2.transform;
        originTransform3 = slashObject3.transform;

        originTransform4 = slashObject4.transform;
    }
    public void PlaySlash1()
    {
        slashObject.transform.position = targetTransform.position;
        slashObject.transform.rotation = transform.rotation * originTransform.rotation;
        slashEffect.Play();
    }
    public void PlaySlash2()
    {
        slashObject2.transform.position = targetTransform.position;
        slashObject2.transform.rotation = transform.rotation * originTransform2.rotation;
        slashEffect2.Play();
    }
    public void PlaySlash3()
    {
        slashObject3.transform.position = targetTransform.position;
        slashObject3.transform.rotation = transform.rotation * originTransform3.rotation;
        slashEffect3.Play();
    }
    public void PlaySlash4()
    {
        slashObject4.transform.position = targetTransform.position;
        slashObject4.transform.rotation = transform.rotation * originTransform4.rotation;
        slashEffect4.Play();
    }
    public void ReturnToOrigin()
    {
        slashObject.transform.position = originTransform.position;
    }
}
