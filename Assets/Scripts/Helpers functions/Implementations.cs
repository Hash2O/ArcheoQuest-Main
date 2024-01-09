using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Implementations : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text;


    private void Start()
    {
        StartCoroutine(GoodImplementation());
    }

    IEnumerator GoodImplementation()
    {
        for(int i = 0; i < 10; i++)
        {
            yield return Helpers.GetWait(0.1f);
            Debug.Log("Good new WaitForSeconds implementation");
        }
    }

    private void Update()
    {
        _text.text = Helpers.IsOverUI() ? "Over UI" : "Home free";
    }
}
