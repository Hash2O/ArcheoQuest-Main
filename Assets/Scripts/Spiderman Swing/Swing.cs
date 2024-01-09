using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Swing : MonoBehaviour
{
    public Transform startSwingHand;
    public float maxDistance = 50;
    public LayerMask swingableLayer;

    public InputActionProperty swingAction;
    public InputActionProperty pullAction;

    public float pullingStrength = 10000;

    public LineRenderer webRenderer;

    public Rigidbody playerRb;
    private SpringJoint joint;

    public Transform predictionPoint;
    private Vector3 swingPoint;

    private bool hasHit;

    private AudioSource audioSource;
    [SerializeField] private List<AudioClip> clipList;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        GetSwingPoint();

        if (swingAction.action.WasPressedThisFrame())
        {
            StartSwing();
        }
        else if (swingAction.action.WasReleasedThisFrame())
        {
            StopSwing();
        }

        PullWebLine();

        DrawWebLine();
    }

    public void GetSwingPoint()
    {
        //Check before raycast, if joint already exists
        if (joint)
        {
            //Disable the prediction point while swinging
            predictionPoint.gameObject.SetActive(false);
            return;
        }
            

        RaycastHit hit;

        hasHit = Physics.Raycast(startSwingHand.position, startSwingHand.forward, out hit, maxDistance, swingableLayer);

        if(hasHit)
        {
            swingPoint = hit.point;
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = swingPoint;
        }
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }
    }

    public void StartSwing()
    {
        if(hasHit)
        {
            joint = playerRb.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = swingPoint;

            float distance = Vector3.Distance(playerRb.position, swingPoint);
            joint.maxDistance = distance;

            joint.spring = 4.5f;
            joint.damper = 7;
            joint.massScale = 4.5f;
        }
    }

    public void StopSwing()
    {
            Destroy(joint);
    }

    public void DrawWebLine()
    {
        if(!joint)
        {
            webRenderer.enabled = false;
        }
        else
        {
            //StartCoroutine("SwingSound", 0.05f);

            webRenderer.enabled = true;
            webRenderer.positionCount = 2;
            webRenderer.SetPosition(0, startSwingHand.position);
            webRenderer.SetPosition(1, swingPoint); 
        }
    }

    public void PullWebLine()
    {
        if (!joint)
            return;

        if (pullAction.action.IsPressed())
        {
            //StartCoroutine("PullSound", 0.05f);

            Vector3 direction = (swingPoint - startSwingHand.position).normalized;  
            playerRb.AddForce(direction * pullingStrength * Time.deltaTime);

            float distance = Vector3.Distance(playerRb.position, swingPoint);
            joint.maxDistance = distance; 
        }

    }

    IEnumerator SwingSound(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.PlayOneShot(clipList[0]);
    }

    IEnumerator PullSound(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.PlayOneShot(clipList[1]);
    }
}
