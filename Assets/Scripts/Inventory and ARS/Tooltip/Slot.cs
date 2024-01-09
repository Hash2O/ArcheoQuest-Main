using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scripts References")]

    [SerializeField]
    private ItemInteractionsSystem itemInteractionsSystem;

    [Header("Slot Panel References")]

    public ItemData item;
    public Image itemVisual;
    public TextMeshProUGUI countText;

    //Affichage du tooltip au survol de la souris sur le slot
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null) { TooltipSystem.instance.ShowTooltip(item.description, item.name); }
        
    }

    //Disparition du tooltip quand la souris quitte le slot
    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipSystem.instance.HideTooltip();
    }

    //Affichage de l'action panel (dynamique) si clic sur le slot, en fct du type d'item
    public void ClickOnSlot()
    {
        itemInteractionsSystem.OpenActionPanel(item, transform.position);
    }

}
