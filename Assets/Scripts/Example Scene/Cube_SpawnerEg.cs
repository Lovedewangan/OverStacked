using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_SpawnerEg : MonoBehaviour
{
    [SerializeField]
    private Cube_movement cubePrefab;

    [SerializeField]
    private MoveDirection moveDirection;

    
    private float cameraYOffset = 0f;
    private float cameraLerpSpeed = 1f;


    public void SpawnCube(int spawnerIndex)
    {


        //Debug.Log("LastCube1: " + Cube_movement.LastCube1.gameObject.name);
        //Debug.Log("Start: " + GameObject.Find("Start").name);
        
        var cube = Instantiate(cubePrefab);

        Debug.Log("Last cube count: " + Cube_movement.LastCubeCount);

        Car_movement carMovement = FindObjectOfType<Car_movement>();

        
            // Set the target of the Car_movement component to the spawned cube's transform
            
        /*if (carMovement != null)
        {
           
            
                carMovement.target = Cube_movement.SecondToLastCube.transform;
            
            
        }*/


        if (carMovement != null)
        {
            // Create a new position vector with the same Y and Z components as SecondToLastCube
            Vector3 newPosition = Cube_movement.SecondToLastCube.transform.position;

            // Set the X component of newPosition to match the X position of the spawned cube
            newPosition.x = 0.55f;

            // Set the target position of the Car_movement script to newPosition
            carMovement.target.position = newPosition;
        }



        if (Cube_movement.LastCube1 != null && Cube_movement.LastCube1.gameObject != GameObject.Find("Start"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : Cube_movement.LastCube1.transform.position.x;

            float z = moveDirection == MoveDirection.Z ? transform.position.z : Cube_movement.LastCube1.transform.position.z;

            if (spawnerIndex == 1) // Spawn from left to right
            {
                z = -3f; // Start at -3 in Z direction
            }
            else if (spawnerIndex == 0) // Spawn from right to left
            {
                z = 3f; // Start at 3 in Z direction
            }

            cube.transform.position = new Vector3(x,Cube_movement.LastCube1.transform.position.y + cubePrefab.transform.localScale.y,z);


            //Debug.Log("Y");
            LerpCameraPosition();
        }
        else
        {
            cube.transform.position = transform.position;
            LerpCameraPosition();

        }

        cube.MoveDirection = moveDirection;

        
    }
    
    private void LerpCameraPosition()
    {
        // Get the current camera position
        Vector3 currentPosition = Camera.main.transform.position;
        //Debug.Log($"Current camera position: {currentPosition}");

        // Calculate the new target position
        Vector3 targetPosition = new Vector3(
            currentPosition.x,
            currentPosition.y + cameraYOffset,
            currentPosition.z
        );
        //Debug.Log($"Target camera position: {targetPosition}");

        // Interpolate the camera position towards the target
        Camera.main.transform.position = Vector3.Lerp(currentPosition, targetPosition, cameraLerpSpeed * Time.deltaTime);
        //Debug.Log($"New camera position: {Camera.main.transform.position}");
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }

    
}
