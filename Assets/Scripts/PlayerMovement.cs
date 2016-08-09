using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    public float inputDel = 0.1f;
    public float ForwVel = 12;
    public float rotatVel = 100;

    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput, turnInput;

    Quaternion TargetRotation
    {

        get { return targetRotation; }

    }

    void Start()
    {

        targetRotation = transform.rotation;
        if (GetComponent<Rigidbody>())
            rBody = GetComponent<Rigidbody>();
        else
            Debug.LogError("Char needs RIGIDBODY");


    }


    void GetInput()
    {
        forwardInput = Input.GetAxis("Vertical");
        turnInput = Input.GetAxis("Horiziontal");

    }

 

    void Update()
    {
        GetInput();
        Turn(); 



    }

    void FixedUpdate()
    {


        Run(); 
    }


    void Run()

    {
        //Acceleration delay
        if (Mathf.Abs(forwardInput) > inputDel)
        {

            //move 
            rBody.velocity = transform.forward * forwardInput * ForwVel;


        }

        else
            rBody.velocity = Vector3.zero; 

    }


    void Turn()
    {
        //Turning (HAS ERROR>?)
        targetRotation *= Quaternion.AngleAxis(rotatVel * turnInput * Time.deltaTime, Vector3.up);
        transform.rotation = targetRotation;


    }

}
