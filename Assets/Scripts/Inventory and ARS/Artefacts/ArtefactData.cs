using UnityEngine;

[CreateAssetMenu(fileName = "Artefact", menuName = "Artefacts/New Artefact")]
public class ArtefactData : ScriptableObject
{
    public ItemData craftableItem;
    public ItemInInventory[] requiredItems;
}
