using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpinner : MonoBehaviour
{

    [SerializeField]
    private float _spinSpeedX = 0.1f;

    [SerializeField]
    private float _spinSpeedY = 0.1f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(_spinSpeedX, _spinSpeedY, 0));
    }
}
