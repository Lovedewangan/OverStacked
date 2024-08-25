using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField]
    private MovingCube cubePrefab;

    [SerializeField]
    private MoveDirection moveDirection;

    private float cameraYOffset = 3f; 
    private float cameraLerpSpeed = 1f; 

    public void SpawnCube()
    {
        //Debug.Log("LastCube: " + MovingCube.LastCube.gameObject.name);
        //Debug.Log("GameObject.Find('Start'): " + GameObject.Find("Start").name);

        var cube = Instantiate(cubePrefab);

        if (MovingCube.LastCube != null && MovingCube.LastCube.gameObject != GameObject.Find("Start"))
        {
            float x = moveDirection == MoveDirection.X ? transform.position.x : MovingCube.LastCube.transform.position.x;
            float z = moveDirection == MoveDirection.Z ? transform.position.z : MovingCube.LastCube.transform.position.z;

            cube.transform.position = new Vector3(x,
                MovingCube.LastCube.transform.position.y + cubePrefab.transform.localScale.y,
                z);

            LerpCameraPosition();
        }
        else
        {
            cube.transform.position = transform.position;
        }

        cube.MoveDirection = moveDirection;
    }

private void LerpCameraPosition()
{
    // Get the current camera position
    Vector3 currentPosition = Camera.main.transform.position;
    Debug.Log($"Current camera position: {currentPosition}");

    // Calculate the new target position
    Vector3 targetPosition = new Vector3(
        currentPosition.x,
        currentPosition.y + cameraYOffset,
        currentPosition.z
    );
    Debug.Log($"Target camera position: {targetPosition}");

    // Interpolate the camera position towards the target
    Camera.main.transform.position = Vector3.Lerp(currentPosition, targetPosition, cameraLerpSpeed * Time.deltaTime);
    Debug.Log($"New camera position: {Camera.main.transform.position}");
}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, cubePrefab.transform.localScale);
    }
}