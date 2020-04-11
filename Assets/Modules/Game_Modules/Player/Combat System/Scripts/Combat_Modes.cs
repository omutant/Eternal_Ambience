using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat_Modes : MonoBehaviour
{
    [SerializeField]
    public MonoBehaviour CurrentMode;
    // Start is called before the first frame update
    void Start()
    {
        CurrentMode = GetComponent<Telekinesis_Grab>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMode(string mode)
    {
        switch (mode)
        {
            case "Normal":
                break;
            case "Fire":
                break;
            case "Thunder":
                break;
            case "Ice":
                break;
            default:
                Debug.Log($"Invalid mode {mode} requested by Combat_Modes");
                break;
        }
    }
}
