using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPSliderBehaviour : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private float activeTime = 1.5f;
    private float originActiveTime;
    private void Awake()
    {
        originActiveTime = activeTime;
    }

    private void Update()
    {
        transform.LookAt(transform.position + mainCam.transform.rotation * Vector3.forward,
                         mainCam.transform.rotation * Vector3.up);

        activeTime -= Time.deltaTime;
        if (activeTime <= 0)
        {
            activeTime = originActiveTime;
            gameObject.SetActive(false);
        }
    }


}
