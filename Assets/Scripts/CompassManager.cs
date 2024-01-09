using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CompassManager : MonoBehaviour
{

    private float speed;
    private float turnSpeed = 50.0f;
    private float horizontalInput;
    private float forwardInput;

    [SerializeField] TextMeshProUGUI affichageDirection;
    [SerializeField] TextMeshProUGUI affichageVitesse;

    [SerializeField]
    float eulerAngX;
    [SerializeField]
    float eulerAngY;
    [SerializeField]
    float eulerAngZ;

    float windDirection;

    // Start is called before the first frame update
    void Start()
    {
        affichageDirection.SetText("Direction du navire : ");
        affichageVitesse.SetText("Vitesse : ");
        windDirection = 182.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //eulerAngles https://forum.unity.com/threads/solved-how-to-get-rotation-value-that-is-in-the-inspector.460310/
        eulerAngX = transform.localEulerAngles.x;
        eulerAngY = transform.localEulerAngles.y;
        eulerAngZ = transform.localEulerAngles.z;

        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");

        float direction = transform.localEulerAngles.y + windDirection;
        if (direction > 360)
        {
            direction = (transform.localEulerAngles.y - 360) + windDirection;
        }
        affichageDirection.SetText("Direction du navire : " + direction);

        /*
        if(0 < direction && direction < 0.50)
        { 
            speed = 5f;
            affichageVitesse.SetText("Vitesse : " + speed);
        }
        else if (0.51f < direction && direction < 1)
        {
            speed = 10f;
            affichageVitesse.SetText("Vitesse : " + speed);
        }
        else if (- 0.51f > direction && direction > -0.99f)
        {
            speed = 9f;
            affichageVitesse.SetText("Vitesse : " + speed);
        }
        else if (-0.1f > direction && direction > -0.50f)
        {
            speed = 6f;
            affichageVitesse.SetText("Vitesse : " + speed);
        }
        */

        /*
        if (-0.15 <= direction && direction <= 0.15)
        {
            speed = 0f;
            affichageVitesse.SetText("Face au vent. \n Vitesse : " + speed);
        }
        else if (0.151f <= direction && direction <= 0.50)
        {
            speed = 5f;
            affichageVitesse.SetText("Vent de travers. Vitesse : " + speed);
        }
        else if (0.51f <= direction && direction <= 0.85f)
        {
            speed = 8f;
            affichageVitesse.SetText("Largue. Vitesse : " + speed);
        }
        else if (-0.85f < direction && direction > 0.85f)
        {
            speed = 12f;
            affichageVitesse.SetText("Vent arrière. Vitesse : " + speed);
        }
        else if (-0.51f < direction && direction <= -0.85f)
        {
            speed = 8f;
            affichageVitesse.SetText("Largue. Vitesse : " + speed);
        }
        else if (-0.15f < direction && direction <= -0.50f)
        {
            speed = 5f;
            affichageVitesse.SetText("Vent de travers. Vitesse : " + speed);
        }
        */

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
        else if (359.9f > direction && direction > 345f)
        {
            speed = 0f;
            affichageVitesse.SetText("Face au vent. \n Vitesse : " + speed);
        }


            transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);

        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }


}
