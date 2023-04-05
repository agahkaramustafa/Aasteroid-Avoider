using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] GameObject gameOverDisplay;
    [SerializeField] AsteroidSpawner asteroidSpawner;

    public void EndGame()
    {
        asteroidSpawner.enabled = false;

        gameOverDisplay.gameObject.SetActive(true);
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueButton()
    {

    }

    public void ReturnToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
