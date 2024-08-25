using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Car_movement : MonoBehaviour
{

    

    public float speed = 5f;
    public Transform target;

    private bool isFirstCubeSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    void Update()
    {
        if (isFirstCubeSpawned)
        {


            /*Vector3 direction = target.position - transform.position;
            direction.y = 0f; // Ensure the car does not tilt up or down
            

            // Rotate the car to face the target's position
            transform.rotation = Quaternion.LookRotation(direction);
*//*
            Vector3 eulerRotation = transform.rotation.eulerAngles;
            eulerRotation.z = f;
            transform.rotation = Quaternion.Euler(eulerRotation);*/

            // Move the car towards the target's position
            
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            
            // Move the car towards the target's position
            //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        
        
    }

    
    public void SetFirstCubeSpawned()
    {
        this.GetComponent<AudioSource>().Play();
        isFirstCubeSpawned = true;
    }
    public void SetFirstCubeSpawnedFalse()
    {
        isFirstCubeSpawned = false;
    }
    public void Car_Movement()
    {
        

    }
}

    /*public Vector3 TargetPosition
    {
        set { targetPosition = value; }
    }*/

