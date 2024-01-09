using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArtefactReconstructionSystem : MonoBehaviour
{
    [SerializeField]
    private ArtefactData[] availableArtefacts;

    [SerializeField]
    private GameObject artefactUIPrefab;

    [SerializeField]
    private Transform artefactsParent;

    [SerializeField]
    private KeyCode openCraftPanelInput;

    [SerializeField]
    private GameObject craftPanel;

    [Header("VR Input System")]
    [SerializeField]
    private InputActionProperty selectInputSource;

    private bool isSelectButtonPressed;

    void Start()
    {
        UpdateDisplayedArtefacts();
    }

    private void Update()
    {
        if(Input.GetKeyDown(openCraftPanelInput) || isSelectButtonPressed)
        {
            craftPanel.SetActive(!craftPanel.activeSelf);
            UpdateDisplayedArtefacts();
        }
    }

    private void FixedUpdate()
    {
        isSelectButtonPressed = selectInputSource.action.WasPressedThisFrame();
    }

    //Version locale du RefreshContent pour le tableau des artefacts accessibles à
    //la reconstitution
    public void UpdateDisplayedArtefacts()
    {
        //On efface...
        foreach(Transform child in artefactsParent)
        {
            Destroy(child.gameObject);
        }

        //... pour repeupler
        for (int i = 0; i < availableArtefacts.Length; i++)
        {
            GameObject currentArtefact = Instantiate(artefactUIPrefab, artefactsParent);
            currentArtefact.GetComponent<Artefact>().Configure(availableArtefacts[i]);
        }
    }

 
}
