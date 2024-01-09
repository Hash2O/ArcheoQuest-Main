using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Pour travailler sur les boutons

public class ButtonManager : MonoBehaviour
{

    //Première enum ! (on définit les valeurs qu'elle peut prendre)
    private enum buttonFunctionnality
    {
        Previous,
        Next,
        Restart, 
        ExitGame
    }

    [SerializeField] private buttonFunctionnality _functionnality;

    private GameManager gm;

    private Button myButton;
    
    // Start is called before the first frame update
    void Start()
    {
        //Pour trouver la version instanciée dans la hiérarchie
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        myButton = gameObject.GetComponent<Button>();

        if(_functionnality == buttonFunctionnality.Next){
            myButton.onClick.AddListener(gm.LoadNextScene);
        }
        else if(_functionnality == buttonFunctionnality.Previous){
            myButton.onClick.AddListener(gm.LoadPreviousScene);
        } 
        else if(_functionnality == buttonFunctionnality.Restart){
            myButton.onClick.AddListener(gm.ReloadScene);
        }
        else 
        {
            myButton.onClick.AddListener(gm.ExitGame);
        }

    }

}
