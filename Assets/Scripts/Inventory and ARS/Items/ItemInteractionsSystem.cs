using UnityEngine;

public class ItemInteractionsSystem : MonoBehaviour
{
    [Header("Scripts References")]

    [SerializeField]
    private Equipment equipment;

    //Gestion de l'affichage du panel des actions en fonction de l'item sélectionné
    [Header("Action Panel References")]

    public GameObject actionPanel;

    [SerializeField]
    private Transform dropPoint;

    [SerializeField]
    private GameObject useItemButton;

    [SerializeField]
    private GameObject equipItemButton;

    [SerializeField]
    private GameObject dropItemButton;

    [SerializeField]
    private GameObject destroyItemButton;

    //Variable pour donner au bouton les infos sur l'item actuellement sélectionné
    //dans l'inventaire (HideInInspector car dynamique et ne devant pas être utilisé dans l'inspecteur)
    [HideInInspector]
    public ItemData itemCurrentlySelected; 

    //Affichage dynamique des boutons de l'action panel en fonction du type d'item
    public void OpenActionPanel(ItemData item, Vector3 slotPosition)
    {
        //Initialisation de la variable pour déterminer l'item sélectionné
        itemCurrentlySelected = item;

        //Sécurité en cas de slot vide
        if (item == null)
        {
            actionPanel.SetActive(false);
            return;
        }

        switch (item.itemType)
        {
            case ItemType.Ressource:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(false);
                break;
            case ItemType.Equipment:
                useItemButton.SetActive(false);
                equipItemButton.SetActive(true);
                break;
            case ItemType.Consumable:
                useItemButton.SetActive(true);
                equipItemButton.SetActive(false);
                break;

        }
        //Positionnement de 'action panel avant ouverture
        actionPanel.transform.position = slotPosition;
        actionPanel.SetActive(true);
    }

    //Désactiver l'action panel
    public void CloseActionPanel()
    {
        actionPanel.SetActive(false);
        itemCurrentlySelected = null;
    }

    //Gestion des actions liées aux boutons de l'action panel
    public void UseActionButton()
    {
        //Fermeture de l'action panel après le clic sur le bouton
        CloseActionPanel();

    }

    public void EquipActionButton()
    {
        equipment.EquipAction();
    }

    public void DropActionButton()
    {
        //Instantiation de l'objet à dropper, en fct des données de l'item sélectionné
        GameObject instantiatedItem = Instantiate(itemCurrentlySelected.prefab);
        //Positionnement du GO instancié sur le drop point choisi
        instantiatedItem.transform.position = dropPoint.position;
        //Item retiré de la liste
        Inventory.instance.RemoveItem(itemCurrentlySelected);
        //Rafraichissement 
        Inventory.instance.RefreshContent();
        //Fermeture de l'action panel après le clic sur le bouton
        CloseActionPanel();
    }

    public void DestroyActionButton()
    {
        //Item supprimé de la liste
        Inventory.instance.RemoveItem(itemCurrentlySelected);
        //Rafraichissement 
        Inventory.instance.RefreshContent();
        //Fermeture de l'action panel après le clic sur le bouton
        CloseActionPanel();
    }

}
