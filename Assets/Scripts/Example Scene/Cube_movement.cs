using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cube_movement : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 1.5f;

    public static Cube_movement CurrentCube1 { get; private set; }

    public static Cube_movement LastCube1 { get; set; }

    public static Cube_movement SecondToLastCube { get; set; }

    public static int LastCubeCount { get; set; } = 0;
    public MoveDirection MoveDirection { get; set; }

    public static List<Vector3> cubePositions = new List<Vector3>();



    //public MoveDirection1 MoveDirection1 {  get; set; }
    private void OnEnable()
    {
        if (LastCube1 == null)
        {
            LastCube1 = GameObject.Find("Start").GetComponent<Cube_movement>();
            LastCubeCount = 1; // Set the initial count to 1 for the "Start" cube
            
        }
        else
        {
            
            LastCubeCount++; // Increment the count for each new cube
            SecondToLastCube = LastCube1;
            


        }
        if (LastCubeCount > 6)
        {
            LastCubeCount = 1;
        }
        CurrentCube1 = this;

        GetComponent<Renderer>().material.color = GetRandomColor();

        transform.localScale = new Vector3(LastCube1.transform.localScale.x, transform.localScale.y, LastCube1.transform.localScale.z);

        //cubePositions.Add(transform.position);
    }

    private Color GetRandomColor()
    {
        return new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
    }

    private void Update()
    {
        if (MoveDirection == MoveDirection.Z)
        {
            transform.position += transform.forward * Time.deltaTime * moveSpeed;
            //transform.position = new Vector3(transform.position.x, transform.position.y + transform.localScale.y, transform.position.z);
        }
        else
        {
            transform.position += -transform.forward * Time.deltaTime * moveSpeed;
        }
    }

    internal void Stop()
    {
        moveSpeed = 0;
        float hangover = GetPrevLocation();
        
        Debug.Log(hangover);

        float max = MoveDirection == MoveDirection.Z ? LastCube1.transform.localScale.z : LastCube1.transform.localScale.z;
        if (Mathf.Abs(hangover) >= max)
        {
            LastCube1 = null;
            CurrentCube1 = null;
            SecondToLastCube = null;
            SceneManager.LoadScene(1);
        }

        float direction = hangover > 0 ? 1f : -1f;

        if (MoveDirection == MoveDirection.Z)
        {
            SplitCubeOnZ(hangover, direction);
        }
        else
        {
            SplitCubeOnX(hangover, direction);
        }

        LastCube1 = this;
        
    }

    private float GetPrevLocation()
    {
        if (MoveDirection == MoveDirection.Z)
            return transform.position.z - LastCube1.transform.position.z;
        else
            return transform.position.z - LastCube1.transform.position.z;
    }

    private void SplitCubeOnX(float hangover, float direction)
    {
        float newXSize = LastCube1.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newXSize;

        float newXPosition = LastCube1.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newXSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newXPosition);

        float cubeEdge = transform.position.z + (newXSize / 2f * direction);
        float fallingBlockXPosition = cubeEdge + fallingBlockSize / 2f * direction;



        SpawnDropCube(fallingBlockXPosition, fallingBlockSize);
    }
    private void SplitCubeOnZ(float hangover, float direction)
    {
        float newZSize = LastCube1.transform.localScale.z - Mathf.Abs(hangover);
        float fallingBlockSize = transform.localScale.z - newZSize;

        float newZPosition = LastCube1.transform.position.z + (hangover / 2);
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newZSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);

        float cubeEdge = transform.position.z + (newZSize / 2f * direction);
        float fallingBlockZPosition = cubeEdge + fallingBlockSize / 2f * direction;



        SpawnDropCube(fallingBlockZPosition, fallingBlockSize);
    }

    private void SpawnDropCube(float fallingBlockZPosition, float fallingBlockSize)
    {
        var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        if (MoveDirection == MoveDirection.Z)
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
        }
        else
        {
            cube.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, fallingBlockSize);
            cube.transform.position = new Vector3(transform.position.x, transform.position.y, fallingBlockZPosition);
            //cube.transform.localScale = new Vector3(fallingBlockSize, transform.localScale.y, transform.localScale.z);
            //cube.transform.position = new Vector3(fallingBlockZPosition, transform.position.y, transform.position.z);
        }
        cube.AddComponent<Rigidbody>();
        cube.GetComponent<Renderer>().material.color = GetComponent<Renderer>().material.color;

        Destroy(cube.gameObject, 3f);
    }
}