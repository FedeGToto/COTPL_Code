using UnityEngine;
using UnityEngine.UI;

public class TarotSelectorUI : MonoBehaviour
{
    [SerializeField] private TarotsManager tarotManager;

    [SerializeField] private TarotSelectUI tarotSelectorPrefab;
    [SerializeField] private Transform selectorParent;

    public void OpenShop(TarotSO[] selectedTarots)
    {
        EventManager.Instance.AddListener<PickedTarotEvent>(OnPickedTarot);

        GameManager.Instance.Input.ChangeContext(InputManager.InputContext.UserInterface);

        foreach (var tarot in selectedTarots)
        {
            TarotSelectUI instancedTarot = Instantiate(tarotSelectorPrefab, selectorParent);
            instancedTarot.UpdateCard(tarotManager, tarot);
        }

        selectorParent.GetChild(0).GetComponent<Button>().Select();

        gameObject.SetActive(true);
    }

    private void OnPickedTarot(PickedTarotEvent e)
    {
        for (int i = 0; i < selectorParent.childCount; i++)
        {
            Destroy(selectorParent.GetChild(i).gameObject);
        }

        GameManager.Instance.Input.ChangeContext(InputManager.InputContext.Default);

        gameObject.SetActive(false);

        EventManager.Instance.RemoveListener<PickedTarotEvent>(OnPickedTarot);
    }
}
