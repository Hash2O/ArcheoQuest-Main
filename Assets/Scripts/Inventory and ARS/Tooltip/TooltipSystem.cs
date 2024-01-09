using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    //Singleton pour accessibilité partout dans le jeu
    public static TooltipSystem instance;

    [SerializeField]
    private Tooltip tooltip;

    private void Awake()
    {
        instance = this;
    }

    public void ShowTooltip(string content, string header = "")
    {
        tooltip.SetText(content, header);
        tooltip.gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltip.gameObject.SetActive(false);
    }

}
