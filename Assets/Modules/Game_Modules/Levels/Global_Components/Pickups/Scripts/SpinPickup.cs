using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPickup : MonoBehaviour
{
    [SerializeField]
    private float spinSpeed = 75;

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += new Vector3(0,spinSpeed * Time.deltaTime,0);
    }
}
