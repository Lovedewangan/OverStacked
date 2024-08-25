using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int score;
    private TextMeshProUGUI text;
    private Cube_movement cubeMovement;
    public GameObject UI;

    private void Start()
    {
        text = GetComponent<TMPro.TextMeshProUGUI>();
        GameManagerEg.OnCubeSpawned += GameManager_OnCubeSpawned;
        cubeMovement = GameObject.FindObjectOfType<Cube_movement>();

    }

    private void OnDestroy()
    {
        GameManagerEg.OnCubeSpawned -= GameManager_OnCubeSpawned;
    }

    private void GameManager_OnCubeSpawned()
    {
        score++;
        text.text = "Score: " + score;
        UpdateCameraRotation();

       
    }

    private void UpdateCameraRotation()
    {
        if (score == 10)
        {
            StartCoroutine(SmoothCameraMovement(-40f, -90f, 0f,  1f));
            Vector3 newPosition = Camera.main.transform.position;
            newPosition.y = 3.5f; // Set the desired x-axis position
            Camera.main.transform.position = newPosition;
        }
        else if (score == 15)
        {
            StartCoroutine(SmoothCameraMovement(0f, -90f, 0f, 1f));
            Vector3 newPosition = Camera.main.transform.position;
            newPosition.y = 11.5f; // Set the desired x-axis position
            Camera.main.transform.position = newPosition;
        }
    }

    private IEnumerator SmoothCameraMovement(float targetRotationX, float targetRotationY, float targetRotationZ,  float duration)
    {
        Quaternion initialRotation = Camera.main.transform.rotation;
        Vector3 initialPosition = Camera.main.transform.position;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            Camera.main.transform.rotation = Quaternion.Slerp(initialRotation, Quaternion.Euler(targetRotationX, targetRotationY, targetRotationZ), t);
            

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Camera.main.transform.rotation = Quaternion.Euler(targetRotationX, targetRotationY, targetRotationZ);
        
    }

    public void PauseMenu()
    {
        Time.timeScale = 0f;
        UI.gameObject.SetActive(true);
        
    }

    public void PlayMenu()
    {
        Time.timeScale = 1f;
    }

    public void RestartMenu()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
