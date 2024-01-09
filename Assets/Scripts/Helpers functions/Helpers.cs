using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class Helpers 
{
    //#1 Camera Reference
    private static Camera _camera;
    public static Camera Camera
    {
        get
        {
            if( _camera == null ) _camera = Camera.main;
            return _camera;
        }
    }


    //#2 Non-allocating WaitForSeconds (memory saving and less garbage collector)
    private static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWait(float time)
    {
        if (WaitDictionary.TryGetValue(time, out var wait)) return wait;

        WaitDictionary[time] = new WaitForSeconds(time);
        return WaitDictionary[time];
    }


    //#3 Is pointer over UI ?
    private static PointerEventData _eventDataCurrentPosition;
    private static List<RaycastResult> _raycastResults;

    public static bool IsOverUI()
    {
        _eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        _raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(_eventDataCurrentPosition, _raycastResults);
        return _raycastResults.Count > 0;
    }


    //#4 Find world point of canvas element
    public static Vector2 GetWorldPositionOfCanvasElement(RectTransform element)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(element, element.position, Camera, out var result);
        return result;
    }


    //#5 Quickly destroy all child objects
    public static void DeleteChildren(this Transform t)
    {
        foreach(Transform child in t)
            Object.Destroy(child.gameObject);
    }

}


