using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HealthController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private float maximumInjuredLayerWeight;    //to manage the weight of the Injured Layer on the standard layer

    private float maximumHealth = 100;
    private float currentHealth;

    private Animator animator;
    private int injuredLayerIndex;
    private float layerWeightVelocity;  //to get transition between layers smoother

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maximumHealth;
        animator = GetComponent<Animator>();
        injuredLayerIndex = animator.GetLayerIndex("Injured");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            currentHealth -= maximumHealth / 10;

            if (currentHealth < 0)
            {
                currentHealth = maximumHealth;
            }

        }

        float healthPercentage = currentHealth / maximumHealth;
        healthText.text = $"Health : {healthPercentage * 100}%";

        //Layer Weight and smooth transition between the two
        float currentInjuredLayerWeight = animator.GetLayerWeight(injuredLayerIndex);
        float targetInjuredLayerWeight = (1 - healthPercentage) * maximumInjuredLayerWeight;    //Layer weight management applied here
        animator.SetLayerWeight(injuredLayerIndex,
            Mathf.SmoothDamp(currentInjuredLayerWeight, targetInjuredLayerWeight, ref layerWeightVelocity, 0.2f)
            );      
    }
}
