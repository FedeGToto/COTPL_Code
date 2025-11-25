using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TransitionUI transition;
    [field: SerializeField] public InputManager Input { get; private set; }
    [field:SerializeField] public PlayerManager Player {  get; private set; }
    [field: SerializeField] public GameObject Board { get; private set; }
    [field: SerializeReference] public GameMode GameMode { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        GameMode?.StartGame();
    }

    private AsyncOperationHandle<SceneInstance> loadHandle;
    public AsyncOperationHandle<SceneInstance> LoadAdditiveScene(AssetReference sceneToLoad, Action onHalf = default, Action onCompleted = default)
    {
        transition.FadeOut(() =>
        {
            onHalf?.Invoke();
            loadHandle = Addressables.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
            loadHandle.Completed += (x) =>
            {
                transition.FadeIn(() =>
                {
                    onCompleted?.Invoke();
                });
            };
        });
        
        return loadHandle;
    }

    public void UnloadAdditiveScene(Action onHalf = default, Action onCompleted = default)
    {
        transition.FadeOut(() =>
        {
            onHalf?.Invoke();
            Addressables.UnloadSceneAsync(loadHandle);
            loadHandle.Release();
            transition.FadeIn(() =>
            {
                onCompleted?.Invoke();
            });
        });
        
    }
}
