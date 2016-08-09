using UnityEngine;
using System.Collections;


public class PlayerMovement : MonoBehaviour
{
    public float inputDel = 0.1f;
    public float ForwVel = 12f;
    public float rotatVel = 100f;
    public float speed = 10f;


    Quaternion targetRotation;
    Rigidbody rBody;
    float forwardInput;

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
       

    }

 

    void Update()
    {
        GetInput();


        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up * speed * Time.deltaTime);


        if (Input.GetKey(KeyCode.D))
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);




    }

    void FixedUpdate()
    {


        Run();
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up * speed * Time.deltaTime);


        if (Input.GetKey(KeyCode.D))
            transform.Rotate(-Vector3.up * speed * Time.deltaTime);

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



}
