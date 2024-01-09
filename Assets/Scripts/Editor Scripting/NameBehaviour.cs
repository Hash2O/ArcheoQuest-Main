using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameBehaviour : MonoBehaviour
{
    [ContextMenuItem("Randomize Name", "Randomize")]
    public string Name;

    [ContextMenu("Reset Name")]
    private void ResetName()
    {
        Name = string.Empty;
    }

    private void Randomize()
    {
        Name = "Some Random Name";
    }

}
