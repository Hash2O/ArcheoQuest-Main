using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    [SerializeField] private RectTransform _followTarget;

    // Update is called once per frame
    void Update()
    {
        transform.position = Helpers.GetWorldPositionOfCanvasElement(_followTarget);
    }
}
