using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour {

#pragma warning disable IDE0052 // Remove unread private members
    Animator anim;
#pragma warning restore IDE0052 // Remove unread private members
    void Start()
    {
        anim = GetComponent<Animator>();
    }
}