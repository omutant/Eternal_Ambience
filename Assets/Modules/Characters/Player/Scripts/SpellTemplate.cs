using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTemplate : MonoBehaviour
{

    public PlayerController playerController;
    //public Light hornlight;
    public int cost = 25;
    public int castingTime = 2;

    [SerializeField]
    private bool isAim = false;
    [SerializeField]
    private string spellName = "spell name";
    [SerializeField]
    private int spellPower = 5;


    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        SetHornLight();
    }

    void SetHornLight()
    {
        isAim = playerController.isAiming;
        if (isAim == true)
        {
            /*
            if (hornlight.intensity < .6)
            {
                hornlight.intensity += 0.2f;

            }
            if (Input.GetButton("Fire1"))
            {
                hornlight.intensity = 1f;
            }
            else if (hornlight.intensity > .6)
            {
                hornlight.intensity -= 0.2f;
            }

            */

        }
        else
        {
            //if (hornlight.intensity > 0)
            //{
            //    hornlight.intensity -= 0.2f;
            //}
        }
    }
}