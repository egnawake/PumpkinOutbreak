using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    private void OnTriggerEnter(Collider collider)
    {
        SceneManager.LoadScene("Village");
    }
}
