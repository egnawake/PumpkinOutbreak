using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToEndScreen : MonoBehaviour
{
    public void EndScreen()
    {
        SceneManager.LoadScene("LoseScreen");
    }
}
