using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Movement : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float mainThrust = 500f;
    [SerializeField] private float rotationThrust = 100f;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Debug.Log("Pressed A - Rotate Left");
            ApplyRotation(rotationThrust);
        }

        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Debug.Log("Pressed D - Rotate Right"); 
            ApplyRotation(-rotationThrust);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        //Only freeze z rotation, leave x and y rotation untouched
        _rb.freezeRotation = true;
        //freeze physics rotation constraints
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        //taking manual control
        _rb.freezeRotation = false;
        //unfreeze physics
    }
}
