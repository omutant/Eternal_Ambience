using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    //attach spells
    //readonly spellbook spellbook;

    //movement

    [SerializeField]
    private int p_Health = 50;
    [SerializeField]
    private int p_Magic = 50;
    [SerializeField]
    private int p_MaxHealth = 50;
    [SerializeField]
    private int p_MaxMagic = 50;

    [SerializeField]
    private int spellcost = 0;

    private float mtimer = 0f;
    [SerializeField]
    private float cooldowntimer = 0f;
    [SerializeField]
    private float magicRegenSpeed = 0.1f; // how many seconds it takes to regen one magic point


    private bool isCast = false;
    //private bool isRunning =false;

    [SerializeField]
    private string spellButtonOne = "f";
    [SerializeField]
    private string spellButtonTwo = "g";
    [SerializeField]
    private string spellButtonThree = "h";

    [SerializeField]
    private bool hasSpellForceBlast = false; // 10 magic cost. basic meduim range magic bullet
    [SerializeField]
    private bool hasSpellDisperse = false; // magic explosion around hero

    void MagicAttackOne()
    {
        if (p_Magic >= 10)
        {
            p_Magic -= 10;
            //shoot magic
        }
    }




    void Update()
    {
        //magic regeneration over time
        if (p_Magic < p_MaxMagic)
        { //if magic is less than maximum
            mtimer += Time.deltaTime; //start a timer
            if (mtimer >= magicRegenSpeed)
            {  //whenever the timer ticks a certain number
                mtimer = 0f;        //reset timer
                p_Magic++;          //restore one point of magic
            }
        }


        //Spell attacks buttons
        //spell attack timer 
        if (cooldowntimer > 0)
        {
            isCast = true;
            cooldowntimer -= Time.deltaTime;
        }
        else
        {
            cooldowntimer = 0f;
            isCast = false;
        }

        //if buttonx is pressed
        if (Input.GetKeyDown(spellButtonOne) && (isCast == false)) { MagicAttackOne(); }


        if (Input.GetKeyDown(spellButtonThree) && (isCast == false))
        {
            //get magic from spellbook and run object
            spellcost = GetComponent<SpellTemplate>().cost;

            if (p_Magic >= spellcost)
            {
                p_Magic -= spellcost;
                cooldowntimer = GetComponent<SpellTemplate>().castingTime;
            }
        }
    }
}
