using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitToMainMenu : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
