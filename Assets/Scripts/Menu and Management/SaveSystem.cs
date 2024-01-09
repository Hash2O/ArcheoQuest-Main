using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TUTO Unity FR (Jeu de survie avec Unity : Systeme de sauvegarde)
//URL : https://youtu.be/X-X7b8BXBF4

public class SaveSystem : MonoBehaviour
{
    [Header("Needed Scripts")]
    [SerializeField]
    private Equipment equipment;

    [Header("References")]
    [SerializeField]
    private Transform playerTransform;

    [SerializeField]
    private Transform newPlayerTransform;

    private void Start()
    {
        if(MainMenu.loadSavedData)
        {
            LoadData();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.F5))
        {
            SaveData();
        }

        if (Input.GetKeyUp(KeyCode.F9))
        {
            LoadData();
        }
    }

    private void SaveData()
    {
        SavedData savedData = new SavedData
        {
            playerPositions = playerTransform.position,
            inventoryContent = Inventory.instance.GetInventoryContent(),
            equipedHeadItem = equipment.equipedHeadItem,
            equipedChestItem = equipment.equipedChestItem,
            equipedHandsItem = equipment.equipedHandsItem,
            equipedLegsItem = equipment.equipedLegsItem,
            equipedFeetItem = equipment.equipedFeetItem,
        };


        //Sauver au format JSON
        string jsonData = JsonUtility.ToJson(savedData);
        //Création du file path vers le répertoire de sauvegarde
        string filePath = Application.persistentDataPath + "/SavedData.json";
        Debug.Log(filePath);

        //Création du fichier de sauvegarde
        System.IO.File.WriteAllText(filePath, jsonData);
        Debug.Log("Sauvegarde effectuée");

    }

    private void LoadData()
    {
        //Lecture des données existantes à l'emplacement créé lors de la sauvegarde
        string filePath = Application.persistentDataPath + "/SavedData.json";
        string jsonData = System.IO.File.ReadAllText(filePath);

        //Transformation des infos en objet
        SavedData savedData = JsonUtility.FromJson<SavedData>(jsonData);

        //Chargement des données
        // 1 - Position
        newPlayerTransform.position = savedData.playerPositions;

        // 2 - Chargement de l'équipement (A faire avant celui de l'inventaire)
        equipment.LoadEquipment(new ItemData[]
            {
                savedData.equipedHeadItem,
                savedData.equipedChestItem,
                savedData.equipedHandsItem,
                savedData.equipedLegsItem,
                savedData.equipedFeetItem
            });

        // 3 - Inventaire (via le singleton)
        Inventory.instance.LoadData(savedData.inventoryContent);

        //Message de confirmation
        Debug.Log("Chargement terminé");
    }
}

public class SavedData
{
    public Vector3 playerPositions;
    public List<ItemInInventory> inventoryContent;
    public ItemData equipedHeadItem;
    public ItemData equipedChestItem;
    public ItemData equipedHandsItem;
    public ItemData equipedLegsItem;
    public ItemData equipedFeetItem;
}
