using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingMusic : MonoBehaviour
{
    public AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
        audioSource.Play();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "ClientAndServer") Destroy();
        //audioSource.mute = true;
        else
            audioSource.mute = false;
    }

    void Destroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}