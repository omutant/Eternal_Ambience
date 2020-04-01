using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField]
    private HealthManager _playerHealth;
    [SerializeField]
    private float healAmount = 50f;
    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "PlayerCollider")
        {
            _playerHealth.playerHealth += healAmount;
            _playerHealth.OverHeal();
            _playerHealth.UpdateHealthBar();

            Destroy(this.gameObject);
        }
    }
}
