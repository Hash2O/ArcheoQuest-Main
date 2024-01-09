using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;


public class Inventory : MonoBehaviour
{
    [Header("Scripts References")]

    [SerializeField]
    private Equipment equipment;

    [SerializeField]
    private ArtefactReconstructionSystem reconstructionSystem;

    [SerializeField]
    private ItemInteractionsSystem itemInteractionsSystem;

    [Header("Inventory Panel References")]

    //Base de l'inventaire, sous forme de liste
    [SerializeField]
    private List<ItemInInventory> content = new List<ItemInInventory>();

    //Déterminer le.s bouton.s pour afficher l'inventaire
    [SerializeField]
    private InputActionProperty selectInputSource;

    //Support d'affichage de l'inventaire
    [SerializeField]
    private GameObject inventoryPanel;

    [SerializeField]
    private Transform inventorySlotParent;

    private bool isSelectButtonPressed;

    //Constante pour le nombre max de slots dans l'inventaire
    const int InventorySize = 24;

    private bool inventoryIsOpen = false;

    //Texture transparente pour slot vide
    public Sprite emptySlotVisual;

    //Singleton pour meilleure accessibilité
    public static Inventory instance;

    //Initialisation du singleton Inventory
    private void Awake()
    {
        instance = this;
    }

    //Rafraichissement du contenu de l'inventaire au démarrage
    private void Start()
    {
        //CloseInventory();
        RefreshContent();
    }

    //Ajout de contenu et rafraichissement de l'affichage
    public void AddItem(ItemData item)
    {
        //Requête via System.Linq pour voir si un item de même nature existe déjà dans l'inventaire ou non
        ItemInInventory[] itemInInventory = content.Where(elem => elem.itemData == item).ToArray();

        //Garder une trace de l'item qu'on veut stacker dans l'inventaire
        bool itemAdded = false;

        //Si présent et stackable
        if(itemInInventory.Length > 0 && item.stackable)
        {
            for (int i = 0; i < itemInInventory.Length; i++)
            {
                //Si slot avec de la place, on stocke
                if (itemInInventory[i].count < item.maxStack)
                {
                    itemAdded = true;
                    itemInInventory[i].count++;
                    //On sort de la boucle
                    break;
                }
            }

            //Si pas de slots permettant de stacker l'item
            //On stocke dans un nouvel emplacement
            if(!itemAdded)
            {
                content.Add(
                new ItemInInventory
                    {
                        itemData = item,
                        count = 1
                    }
                );
            }
        }
        else
        {
            //Sinon on ajoute simplement dans un nouveau slot
            content.Add(
                new ItemInInventory
                {
                    itemData = item,
                    count = 1
                }
            );
        }

        RefreshContent();
    }

    //Suppression de contenu et rafraichissement de l'affichage
    public void RemoveItem(ItemData item)
    {
        //Requête via System.Linq pour voir si un item de même nature existe déjà dans l'inventaire ou non
        //Amélio possible : utiliser LastOrDefault() éventuellement, pour piocher dans les derniers slots plutot
        //que les premiers (meilleure gestion visuelle de l'inventaire ?)
        ItemInInventory itemInInventory = content.Where(elem => elem.itemData == item).FirstOrDefault();

        //Test si plus que quantité requise et stackable
        if (itemInInventory != null && itemInInventory.count > 1)
        {
            //Si oui, on enlève la quantité requise
            itemInInventory.count --;
        }
        else
        {
            //On supprime l'item restant
            content.Remove(itemInInventory);
        }

        RefreshContent();
    }

    //Lire le contenu de l'inventaire
    public List<ItemInInventory> GetInventoryContent()
    {
        return content;
    }

    //Activer ou désactiver l'inventaire avec le touche concernée (I, ou Primary Button du Touch)
    private void Update()
    {
        if(isSelectButtonPressed || Input.GetKeyDown(KeyCode.I))
        {
            //Méthode de base pour switcher l'ouverture/fermeture de l'inventaire
            //Obsolète avec les action panel et tooltip
            //inventoryPanel.SetActive(!inventoryPanel.activeSelf);

            if(inventoryIsOpen)
            {
                CloseInventory();
            }
            else
            {
                OpenInventory();
            }
        }
    }

    //Test pour savoir si la touche a été activée
    private void FixedUpdate()
    {
        isSelectButtonPressed = selectInputSource.action.WasPressedThisFrame();
    }

    private void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        inventoryIsOpen = true;
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        itemInteractionsSystem.actionPanel.SetActive(false);
        TooltipSystem.instance.HideTooltip();
        inventoryIsOpen = false;
    }

    //Fonction qui boucle dans l'inventaire et rafraichit le contenu et l'affichage des items
    public void RefreshContent()
    {
        //Vidage des slots et visuels associés
        for (int i = 0; i < inventorySlotParent.childCount; i++)
        {
            Slot currentSlot = inventorySlotParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = null;
            currentSlot.itemVisual.sprite = emptySlotVisual;
            currentSlot.countText.enabled = false;
        }

        //Ré initialisation des visuels des slots selon le contenu réel de l'inventaire
        for (int i = 0; i < content.Count; i++)
        {
            Slot currentSlot = inventorySlotParent.GetChild(i).GetComponent<Slot>();

            currentSlot.item = content[i].itemData;
            currentSlot.itemVisual.sprite = content[i].itemData.visual;

            //Vérification de stackalabilité et affichage si nécessaire
            if(currentSlot.item.stackable)
            {
                currentSlot.countText.enabled = true;
                currentSlot.countText.text = content[i].count.ToString();
            }
        }

        //Affichage ou non des boutons pour enlever l'équipement
        equipment.UpdateRemoveEquipmentButtons();
        //
        reconstructionSystem.UpdateDisplayedArtefacts();
    }

    //Test pour savoir si l'inventaire est plein ou non
    public bool IsFull()
    {
        return InventorySize == content.Count;
    }

    public void LoadData(List<ItemInInventory> savedData)
    {
        content = savedData;
        RefreshContent();
    }

    public void ClearInventoryContent()
    {
        content.Clear();
    }

}

[System.Serializable]
public class ItemInInventory
{
    public ItemData itemData;
    public int count;
}
