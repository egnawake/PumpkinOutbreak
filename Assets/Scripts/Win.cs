using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Win : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        SceneManager.LoadScene("WinScreen");
    }
}
