using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Set the interaction layer of an interactor
/// </summary>
public class SetInteractionLayer : MonoBehaviour
{
    [Tooltip("The layer that's switched to")]
    public LayerMask targetLayer = 0;

    private XRBaseInteractor interactor = null;
    private LayerMask originalLayer = 0;

    [System.Obsolete]
    private void Awake()
    {
        interactor = GetComponent<XRBaseInteractor>();
        originalLayer = interactor.interactionLayerMask;
    }

    [System.Obsolete]
    public void SetTargetLayer()
    {
        interactor.interactionLayerMask = targetLayer;
    }

    [System.Obsolete]
    public void SetOriginalLayer()
    {
        interactor.interactionLayerMask = originalLayer;
    }

    [System.Obsolete]
    public void ToggleTargetLayer(bool value)
    {
        if (value)
        {
            SetTargetLayer();
        }
        else
        {
            SetOriginalLayer();
        }
    }

}
