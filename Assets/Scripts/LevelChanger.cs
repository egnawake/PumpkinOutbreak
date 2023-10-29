using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    FMOD.Studio.EventInstance mainSong;

    public LevelChanger(EventInstance mainSong)
    {
        this.mainSong = FMODUnity.RuntimeManager.CreateInstance("event:/Main");
    }

    void Start()
    {
        mainSong.start();
        mainSong.release();
    }

    private void OnTriggerEnter(Collider player)
    {
        mainSong.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        SceneManager.LoadScene("Village");
    }
}
