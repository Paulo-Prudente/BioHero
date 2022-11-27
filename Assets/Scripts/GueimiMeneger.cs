using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GueimiMeneger : IPersistenceSingleton<GueimiMeneger>
{
    public string _currentScene;
    private System.Action<string> OnSceneStart;
    private System.Action<string> OnSceneEnd;
    private int value;

    public PlayerState playerState;

    public void LoadScene(string sceneName)
    {
        OnSceneEnd.Invoke(_currentScene);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        asyncOperation.completed += OnSceneLoaded;
        _currentScene = sceneName;
    }

    private void OnSceneLoaded(AsyncOperation asyncOperation)
    {
        OnSceneStart.Invoke(_currentScene);
    }

    public void RegisterSceneLoaded(System.Action<string> sceneEvent)
    {
        OnSceneStart += sceneEvent;
    }

    public void UnRegisterSceneLoaded(System.Action<string> sceneEvent)
    {
        OnSceneStart -= sceneEvent;
    }

    void Start()
    {
        playerState.Start();
    }

    void Update()
    {
        playerState.Update();
    }



}
