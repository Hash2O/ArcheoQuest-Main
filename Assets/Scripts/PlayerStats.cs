using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    private float currentHealth;
    [SerializeField]
    private float currentMana;
    [SerializeField]
    private float currentStamina;

    [SerializeField]
    private float maxHealth = 100f;
    [SerializeField]
    private float maxMana = 100f;
    [SerializeField]
    private float maxStamina = 100f;


    [SerializeField]
    private Image healthBarFill;
    [SerializeField]
    private Image manaBarFill;
    [SerializeField]
    private Image staminaBarFill;

    [SerializeField]
    private float healthRegenRate = 1f;
    [SerializeField] 
    private float manaRegenRate = 1f;
    [SerializeField]
    private float staminaRegenRate = 1f;

    [SerializeField]
    bool isDead;

    void Awake()
    {
        currentHealth = maxHealth;
        currentMana = maxMana;
        currentStamina = maxStamina;
        isDead = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isDead == false)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                TakeDamage(10f);
            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                SpendingMana(10f);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SpendingStamina(10f);
            }

            healthRegeneration();
            manaRegeneration();
            staminaRegeneration();
        }
        
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        UpdateHealthBarFill();
        if(currentHealth < 0f)
        {
            print("Game Over");
            isDead = true;
        }
    }

    void UpdateHealthBarFill()
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }

    void healthRegeneration()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += healthRegenRate * Time.deltaTime;
            UpdateHealthBarFill();
        }

    }

    void SpendingMana(float manaSpent)
    {
        currentMana -= manaSpent;
        if(currentMana < 0f)
        {
            currentMana = 0f;
        }
        UpdateManaBarFill();
    }

    void UpdateManaBarFill()
    {
        manaBarFill.fillAmount = currentMana / maxMana;
    }

    void manaRegeneration()
    {
        if(currentMana < maxMana)
        {
            currentMana += manaRegenRate * Time.deltaTime;
            UpdateManaBarFill();
        }
        
    }

    void SpendingStamina(float staminaSpent)
    {
        currentStamina -= staminaSpent;
        if (currentStamina < 0f)
        {
            currentStamina = 0f;
        }
        UpdateStaminaBarFill();
    }

    void UpdateStaminaBarFill()
    {
        staminaBarFill.fillAmount = currentStamina / maxStamina;
    }

    void staminaRegeneration()
    {
        if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            UpdateStaminaBarFill();
        }

    }
}
