using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.UI;

public class StaminaWheel : MonoBehaviour
{
    private float stamina;
    [SerializeField] private float maxStamina = 100f;

    [SerializeField] private Image greenWheel;
    [SerializeField] private Image redWheel;

    private bool staminaExhausted;

    void Start()
    {
        //At start, stamina initialization
        stamina = maxStamina;
    }

    void Update()
    {
        //When run key is hit and condition is matched
        if(Input.GetKey(KeyCode.LeftShift) && !staminaExhausted)
        {
            if(stamina > 0)
            {
                //Stamina is drained (amount could be a variable if needed)
                stamina -= 30 * Time.deltaTime;
            }
            else
            {
                //If exhausted, green wheel disappear
                greenWheel.enabled = false;
                //Bool is set to true, disabling player's use of stamina until gauge is filled again
                staminaExhausted = true;
            }

            redWheel.fillAmount = (stamina / maxStamina + 0.07f);
        }
        else
        {
            if (stamina < maxStamina)
            {
                stamina += 30 * Time.deltaTime;
            }
            else
            {
                //Stamina is restored, player can use it again
                greenWheel.enabled = true;
                staminaExhausted = false;
            }

            redWheel.fillAmount = (stamina / maxStamina);
        }

        greenWheel.fillAmount = (stamina / maxStamina);

    }
}
