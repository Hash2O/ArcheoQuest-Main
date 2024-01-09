using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//[ExecuteInEditMode]
public class Tooltip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI headerField;
    [SerializeField]
    private TextMeshProUGUI contentField;

    [SerializeField]
    private LayoutElement layoutElement;

    [SerializeField]
    private int maxCharacter;

    /*
    [SerializeField]
    private RectTransform rectTransform;
    */

    public void SetText(string content, string header = "")
    {
        if(header == "")
        {
            headerField.gameObject.SetActive(false);
        }
        else
        {
            headerField.gameObject.SetActive(true);
            headerField.text = header;
        }

        contentField.text = content;

        int headerLength = headerField.text.Length;
        int contentLength = contentField.text.Length;

        layoutElement.enabled = (headerLength > maxCharacter || contentLength > maxCharacter) ? true : false;
    }

    //Si le tooltip s'affiche en dehors de l'écran ou de la zone de vision en VR
    /*
    private void Update()
    {
        Vector2 mousePosition = Input.mousePosition;

        float pivotX = mousePosition.x / Screen.width;
        float pivotY = mousePosition.y / Screen.height;

        rectTransform.pivot = new Vector2 (pivotX, pivotY);

        transform.position = mousePosition;
    }
    */
}
