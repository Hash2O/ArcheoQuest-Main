using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SailingWindSimulationManager : MonoBehaviour
{
    private float speed;
    private float turnSpeed = 50.0f;
    private float horizontalInput;
    private float forwardInput;

    [SerializeField] TextMeshProUGUI affichageDirection;
    [SerializeField] TextMeshProUGUI affichageVitesse;

    public float windPower;

    public float windDirection;

    public bool isWindSailing;

    public float direction;

    public TextMeshProUGUI affichageMeteo;
    // Start is called before the first frame update
    void Start()
    {
        //Initialisation des variables
        affichageDirection.SetText("Direction du navire : ");
        affichageVitesse.SetText("Vitesse : ");

        //Par défaut, si aucune donnée n'est reçue de l'API, on fixe la provenance du vent au Nord
        windDirection = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        //Inputs pour diriger le navire
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        //Utilisation d'un booleen pour des/activer la navigation à la voile
        //La valeur fournie ici permet de simuler un déplacement du navire avec son moteur
        if (isWindSailing == false)
        {
            //Simulation de la vitesse fournie par le moteur du navire (sans vent donc)
            speed = 5f;
        }
        else
        {
            //Ici, on prend en compte la présence, et donc l'orientation du vent
            //Cette variable va calculer en fonction de la donnée winddirection de l'API
            float modificateurDirection;

            //Calcul de la variable qui détermine la provenance du vent
            if(0 <= transform.localEulerAngles.y && transform.localEulerAngles.y < windDirection)
            {
                modificateurDirection = 360 - windDirection;
                direction = transform.localEulerAngles.y + modificateurDirection;
                affichageDirection.SetText("Direction du navire : " + direction);
            }
            else
            {
                modificateurDirection = windDirection;
                direction = transform.localEulerAngles.y - modificateurDirection;
                affichageDirection.SetText("Direction du navire : " + direction);
            }

 
            //Tester l'orientation du navire pour déterminer sa vitesse, selon qu'il soit plus ou moins face au vent
            if (0 < direction && direction < 15f)
            {
                speed = 0f;
                affichageVitesse.SetText("Face au vent. \n Vitesse : " + speed);
            }
            else if (15f <= direction && direction <= 90f)
            {
                speed = 5f;
                affichageVitesse.SetText("Au près. Vitesse : " + speed);
            }
            else if (90f < direction && direction <= 135f)
            {
                speed = 8f;
                affichageVitesse.SetText("Vent de travers. Vitesse : " + speed);
            }
            else if (135f < direction && direction < 165f)
            {
                speed = 12f;
                affichageVitesse.SetText("Grand largue. Vitesse : " + speed);
            }
            else if (165f <= direction && direction <= 195f)
            {
                speed = 15f;
                affichageVitesse.SetText("Vent arrière. Vitesse : " + speed);
            }
            else if (195f < direction && direction <= 235f)
            {
                speed = 12f;
                affichageVitesse.SetText("Grand largue. Vitesse : " + speed);
            }
            else if (235f < direction && direction <= 270f)
            {
                speed = 8f;
                affichageVitesse.SetText("Vent de travers. Vitesse : " + speed);
            }
            else if (270f < direction && direction <= 345f)
            {
                speed = 5f;
                affichageVitesse.SetText("Au près. Vitesse : " + speed);
            }
            else if (direction > 345f && direction <= 0)
            {
                speed = 0f;
                affichageVitesse.SetText("Face au vent. \n Vitesse : " + speed);
            }
        }
         
            //Faire avancer le navire
            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

            //Faire tourner le navire
            transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
   
    }

    //Méthodes pour activer/désactiver le mode voile ou moteur pour faire avancer le bateau
    public void OnWindSailing()
    {
        isWindSailing = true;
    }

    public void OnMotorBoat()
    {
        isWindSailing = false;
    }
}
