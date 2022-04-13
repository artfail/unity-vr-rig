using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoad : MonoBehaviour
{
    public string level;

    private void Start()
    {
        if (level == "")
        {
            level = SceneManager.GetActiveScene().name; //default to the current level
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(level);
        }
    }
}
