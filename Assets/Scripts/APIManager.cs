using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;


public class APIManager : MonoBehaviour
{
    [SerializeField] string requestURL;
    string queryString;
    //Parametres pour URL
    //[SerializeField] string parameterName;
    //[SerializeField] string parameterValue;

    [SerializeField] List<string> parameterNames;
    [SerializeField] List<string> parameterValues;

    public float affichageTemperature;
    public float affichageVitesseVent;
    public float DirectionVent;
    string affichageDirectionVent;

    [SerializeField] SailingWindSimulationManager boat;

    [SerializeField] BoussoleManager boussole;

    // Start is called before the first frame update
    [System.Obsolete]
    private void Start()
    {
        CallURL();
        
    }

    [System.Obsolete]
    public void CallURL()
    {
        //StartCoroutine(GetRequest(requestURL);

        //Version simplifiée
        //queryString = parameterName + "=" + parameterValue;

        //Version avec list
        //NB : vérifier ici qu'il y autant de noms que de valeurs, sinon ça plante
        for(int i = 0; i < parameterNames.Count; i++)
        {
            queryString += parameterNames[i] + "=" + parameterValues[i] + "&";
        }

        StartCoroutine(GetRequest(requestURL + "?" + queryString)); //Format de la demande avec queryString
    }

    [System.Obsolete]
    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Envoi de la requête
            yield return webRequest.SendWebRequest();

            // Vérification des erreurs
            if (webRequest.isNetworkError)  //Obsolete. Remplacer par UnityWebRequest.result == UnityWebRequest.Result.ConnectionError mais .result génère une autre erreur
            {
                Debug.Log("Error: " + webRequest.error);
                //Changer l'affichage sur le bouton
                //buttonText.SetText("Error: " + webRequest.error);
            }
            else
            {
                // Affichage du résultat
                HandleResponse(webRequest);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void HandleResponse(UnityWebRequest webRequest)
    {
        Debug.Log("Received: " + webRequest.downloadHandler.text);
        //Changer l'affichage sur le bouton
        //buttonText.SetText("Received: " + webRequest.downloadHandler.text);

        //Traitement de données au format JSON
        //NB : Le parsing est ton ami
        //On récupère les données au format JSON et on les met au format de la classe voulue,
        ////ici APIResponse. On le stocke dans une variable
        var apiResponse = JsonUtility.FromJson<APIResponse>(webRequest.downloadHandler.text);
        Debug.Log("APIResponse, température demandée : " + apiResponse.current_weather.temperature);
        affichageTemperature = apiResponse.current_weather.temperature;
        affichageVitesseVent = apiResponse.current_weather.windspeed;
        DirectionVent = apiResponse.current_weather.winddirection;
        Debug.Log(affichageTemperature + " " + affichageVitesseVent + " " + DirectionVent);

        //On transmet au bateau le code (de 0 à 360) de la provenance du vent, fourni par l'API avec winddirection
        boat.windDirection = DirectionVent;


        //Gestion de l'affichage de la direction du vent
        if (0 < DirectionVent && DirectionVent < 22.5f)
        {
            affichageDirectionVent = "Nord";
        }
        else if ( 22.6f < DirectionVent && DirectionVent < 45f)
        {
            affichageDirectionVent = "Nord Nord Est";
        }
        else if (45.1f < DirectionVent && DirectionVent < 67.5f)
        {
            affichageDirectionVent = "Nord Est";
        }
        else if (67.6f < DirectionVent && DirectionVent < 90f)
        {
            affichageDirectionVent = "Est";
        }
        else if (90.1f < DirectionVent && DirectionVent < 112.5f)
        {
            affichageDirectionVent = "Est Sud Est";
        }
        else if (112.6f < DirectionVent && DirectionVent < 135f)
        {
            affichageDirectionVent = "Sud Est";
        }
        else if (135.1f < DirectionVent && DirectionVent < 157.5f)
        {
            affichageDirectionVent = "Sud Sud Est";
        }
        else if (157.6f < DirectionVent && DirectionVent < 180f)
        {
            affichageDirectionVent = "Sud";
        }
        else if (180.1f < DirectionVent && DirectionVent < 202.5f)
        {
            affichageDirectionVent = "Sud Sud Ouest";
        }
        else if (202.6f < DirectionVent && DirectionVent < 225f)
        {
            affichageDirectionVent = "Sud Ouest";
        }
        else if (225.1f < DirectionVent && DirectionVent < 247.5f)
        {
            affichageDirectionVent = "Ouest Sud Ouest";
        }
        else if (247.6f < DirectionVent && DirectionVent < 270f)
        {
            affichageDirectionVent = "Ouest";
        }
        else if (270.1f < DirectionVent && DirectionVent < 292.5f)
        {
            affichageDirectionVent = "Nord Ouest";
        }
        else if (292.6f < DirectionVent && DirectionVent < 315f)
        {
            affichageDirectionVent = "Nord Nord Ouest";
        }
        else if (315.1f < DirectionVent && DirectionVent < 337.5f)
        {
            affichageDirectionVent = "Nord Nord Ouest";
        }
        else if (337.6f < DirectionVent && DirectionVent < 360f)
        {
            affichageDirectionVent = "Nord";
        }

        //Récupération de la vitesse du vent pour générer un modificateur de vitesse du navire
        float windPowerModifier = affichageVitesseVent / 20f; //Créer une variable sérialisée pour gérer le bonus désiré

        //Affichage sur l'écran du navire du modificateur en cours
        boat.affichageMeteo.SetText("Température : " + affichageTemperature + " °C \n" +
                                    "Vitesse du vent : " + affichageVitesseVent + "km/h \n" +
                                    "Direction du vent : " + DirectionVent + ", soit " + affichageDirectionVent + 
                                    "\n Wind Power Modifier : " + windPowerModifier);

        //Application du modif sur la vitesse de base.
        //NB : chaque appel crée une nouvelle valeur qui s'ajoute à la précédente...
        //Remédier à ca : 
        boat.windPower *= windPowerModifier;

        //Reste à trouver la bonne formule pour appliquer cette force dans la bonne direction
    }
}
