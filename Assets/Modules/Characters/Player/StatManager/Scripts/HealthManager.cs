using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField]
    private float maxPlayerHealth = 100;
    [HideInInspector]
    public float playerHealth = 100;
    [SerializeField]
    private GameObject playerVisuals;
    private PlayerController _playerController;
    [SerializeField]
    private Slider healthBar;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = maxPlayerHealth;
        _playerController = GetComponent<PlayerController>();
    }

    public void OverHeal()
    {
        if (playerHealth > maxPlayerHealth)
            playerHealth = maxPlayerHealth;
    }

    public void UpdateHealthBar()
    {
        if (playerHealth / maxPlayerHealth <= 1)
            healthBar.value = playerHealth / maxPlayerHealth;
    }

    public void DeathCheck()
    {
        if (playerHealth <= 0)
        {
            playerVisuals.SetActive(false);
            _playerController.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(playerHealth);
        #region health_testing
        if (Input.GetKeyDown(KeyCode.F))
        {
            playerHealth -= 20;
            DeathCheck();
            UpdateHealthBar();
        }
        #endregion health_testing
    }
}
