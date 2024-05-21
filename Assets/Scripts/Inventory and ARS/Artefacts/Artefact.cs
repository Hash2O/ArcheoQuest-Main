using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
//using static UnityEditor.Progress;

public class Artefact : MonoBehaviour
{
    private ArtefactData currentArtefact;

    [Header("Essential Instance References")]
    [SerializeField]
    private Image craftableItemImage;

    [SerializeField]
    private GameObject requiredElementPrefab;

    [SerializeField]
    private Transform requiredElementsParent;

    [SerializeField]
    private Button craftButton;

    [SerializeField]
    private Sprite canCraftIcon;

    [SerializeField]
    private Sprite cantCraftIcon;

    [SerializeField]
    private Color missingElementColor;

    [SerializeField]    
    private Color availableElementColor;

    //Gérer dynamiquement l'affichage des reconstitutions possibles 
    public void Configure(ArtefactData artefact)
    {
        currentArtefact = artefact;

        craftableItemImage.sprite = artefact.craftableItem.visual;

        //Gestion du tooltip pour l'artefact à crafter
        craftableItemImage.transform.parent.GetComponent<Slot>().item = artefact.craftableItem;

        //Par défaut, on peut tenter une reconstitution d'artefact si...
        bool canCraft = true;

        //... les éléments requis se trouvent bien dans l'inventaire
        for (int i = 0; i < artefact.requiredItems.Length; i++)
        {
            GameObject requiredItemGO = Instantiate(requiredElementPrefab, requiredElementsParent);
            Image requiredItemGOimage = requiredItemGO.GetComponent<Image>();

            //Vérification de la présence de chaque élément nécessaire dans l'inventaire
            //Si absent, conditions non remplies 
            ItemData requiredItem = artefact.requiredItems[i].itemData;

            RequiredElement requiredElement = requiredItemGO.GetComponent<RequiredElement>();    

            //Récupération des infos pour le tooltip au survol des slots 
            requiredItemGO.GetComponent<Slot>().item = requiredItem;

            //Si l'inventaire contient l'élément requis, on le retire et on passe au suivant
            ItemInInventory[] itemInInventory = Inventory.instance.GetInventoryContent().Where(elem => elem.itemData == requiredItem).ToArray();

            //Variable de stockage temporaire
            int totalRequiredItemQuantityInInventory = 0;

            //On récupère le nombre d'items utilisables dans les différents stacks de l'inventaire
            for (int y = 0; y < itemInInventory.Length; y++)
            {
                totalRequiredItemQuantityInInventory += itemInInventory[y].count;
            }

            //Si items en nombre suffisant, on peut crafter
            if (totalRequiredItemQuantityInInventory >= artefact.requiredItems[i].count)
            {
                requiredItemGOimage.color = availableElementColor;
            }
            else
            {
                requiredItemGOimage.color = missingElementColor;
                canCraft = false;
            }

            //Gestion dynamique du positionnement des visuels et quantités des éléments requis 
            requiredElement.elementImage.sprite = artefact.requiredItems[i].itemData.visual;
            requiredElement.elementCountText.text = artefact.requiredItems[i].count.ToString();
        }

        //Gestion de l'affichage du bouton en fct des conditions remplies ou non
        craftButton.image.sprite = canCraft ? canCraftIcon : cantCraftIcon;
        craftButton.enabled = canCraft;

        ResizeRequiredElementsParent();
    }

    //Forcer le canevas à prendre la bonne dimension pour afficher les éléments requis
    //(Schématiquement, OFF puis ON)
    private void ResizeRequiredElementsParent()
    {
        Canvas.ForceUpdateCanvases();
        requiredElementsParent.GetComponent<ContentSizeFitter>().enabled = false;
        requiredElementsParent.GetComponent<ContentSizeFitter>().enabled = true;
    }

    //Test
    public void CraftItem()
    {
        //Suppression de l'inventaire des éléments utilisés pour la reconstitution
        for (int i = 0; i < currentArtefact.requiredItems.Length; i++)
        {
            for (int y = 0; y < currentArtefact.requiredItems[i].count; y++)
            {
                Inventory.instance.RemoveItem(currentArtefact.requiredItems[i].itemData);
            }
        }

        //Ajout d'une nouvel artefact dans l'inventaire
        Inventory.instance.AddItem(currentArtefact.craftableItem);
    }
}
