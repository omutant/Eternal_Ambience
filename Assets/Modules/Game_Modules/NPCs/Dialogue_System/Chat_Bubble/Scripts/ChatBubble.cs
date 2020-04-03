using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatBubble : MonoBehaviour
{
    [SerializeField]
    private GameObject visuals;
    [SerializeField]
    private GameObject ChatUI;
    private void Update()
    {
        this.transform.LookAt(Camera.main.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            visuals.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Interact"))
            ChatUI.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        visuals.SetActive(false);
    }
}
