using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour
{
    [Header("Current Index")]
    [SerializeField] private int pageIndex = 0;

    [Header("Component")]
    [SerializeField] private ToggleGroup toggleGroup;
    [SerializeField] private List<Toggle> tabs = new();
    [SerializeField] private List<CanvasGroup> pages = new();
    [SerializeField] private List<Scrollbar> scrollBars = new();

    [Header("Events")]
    public UnityEvent<int> OnPageIndexChanged;

    private void Initialize()
    {
        toggleGroup = GetComponentInChildren<ToggleGroup>();

        tabs.Clear();
        pages.Clear();
        scrollBars.Clear();

        tabs.AddRange(GetComponentsInChildren<Toggle>());
        pages.AddRange(GetComponentsInChildren<CanvasGroup>());
        scrollBars.AddRange(GetComponentsInChildren<Scrollbar>());
    }

    private void Reset()
    {
        Initialize();
    }

    private void OnValidate()
    {
        Initialize();
        OpenPage(pageIndex);
        tabs[pageIndex].SetIsOnWithoutNotify(true);
    }

    private void Awake()
    {
        foreach (var toggle in tabs)
        {
            toggle.onValueChanged.AddListener(CheckForTab);
            toggle.group = toggleGroup;
        }
    }

    private void OnDestroy()
    {
        foreach (var toggle in tabs)
        {
            toggle.onValueChanged.RemoveListener(CheckForTab);
        }
    }

    private void OnEnable()
    {
        GameManager.Instance.Input.OnChangePage += OnChangePageInput;
    }

    private void OnDisable()
    {
        GameManager.Instance.Input.OnChangePage -= OnChangePageInput;
    }

    private void OnChangePageInput(bool advance)
    {
        int index = pageIndex;

        Debug.Log(advance);

        if (advance) index++; 
        else index--;

        JumpToPage(index);
    }

    private void CheckForTab(bool value)
    {
        for (int i = 0; i < tabs.Count; i++)
        {
            if (!tabs[i].isOn) continue;
            pageIndex = i;
        }

        OpenPage(pageIndex);
    }

    private void OpenPage(int index)
    {
        EnsureIndexIsInRange(index);

        for (int i = 0; i < pages.Count; i++)
        {
            bool isActivePage = (i == pageIndex);

            pages[i].alpha = isActivePage ? 1.0f : 0.0f;
            pages[i].interactable = isActivePage;
            pages[i].blocksRaycasts = isActivePage;
            tabs[i].SetIsOnWithoutNotify(isActivePage);

            if (isActivePage) scrollBars[i].Select();
        }

        if (Application.isPlaying)
            OnPageIndexChanged?.Invoke(pageIndex);
    }

    private void EnsureIndexIsInRange(int index)
    {
        if (tabs.Count == 0 || pages.Count == 0 || scrollBars.Count == 0)
        {
            Debug.Log("Forgot to setupè Tabs or Pages");
            return;
        }

        pageIndex = Mathf.Clamp(index, 0, pages.Count - 1);
    }

    public void JumpToPage(int page)
    {
        EnsureIndexIsInRange(page);
        tabs[pageIndex].isOn = true;
    }
}
