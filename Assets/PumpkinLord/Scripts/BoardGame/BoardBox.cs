using UnityEngine;
using UnityEngine.Events;

public class BoardBox : MonoBehaviour
{
    public bool IsInitialized { get; private set; }
    public UnityEvent OnLanded;

    [field: SerializeField] public Transform LandingPoint { get; private set; }

    public BoxEffect Effect { get; private set; }

    public void Initialize(BoxSO boxSO)
    {
        Effect = Instantiate(boxSO.Effect, transform);
        OnLanded.AddListener(() => Effect.ApplyBoxEffect());

        IsInitialized = true;
    }
}
