using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerEg : MonoBehaviour
{
    public static event Action OnCubeSpawned = delegate { };

    private Cube_SpawnerEg[] spawners;
    private int spawnerIndex;
    private Cube_SpawnerEg currentSpawner;

    

    void Start()
    {
        
            


    }
    private void Awake()
    {
        spawners = FindObjectsOfType<Cube_SpawnerEg>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Cube_movement.CurrentCube1 != null)
                Cube_movement.CurrentCube1.Stop();
                this.GetComponent<AudioSource>().Play();


            spawnerIndex = spawnerIndex == 0 ? 1 : 0;
            currentSpawner = spawners[spawnerIndex];

            
            currentSpawner.SpawnCube(spawnerIndex);
            OnCubeSpawned();

            Car_movement carMovement = FindObjectOfType<Car_movement>();

            
            if (Cube_movement.LastCubeCount > 5)
            {
                carMovement.SetFirstCubeSpawned();
                

            }
            else
            {
                carMovement.SetFirstCubeSpawnedFalse();
            }

        }
    }
}
