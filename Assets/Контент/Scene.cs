using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public void Menu()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Game1()
    {
        SceneManager.LoadScene("Game1");
    }

    public void Game2()
    {
        SceneManager.LoadScene("Game2");
    }
}
