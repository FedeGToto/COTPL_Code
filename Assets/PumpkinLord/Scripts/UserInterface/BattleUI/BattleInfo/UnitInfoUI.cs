using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.Localization.SmartFormat.PersistentVariables;

public class UnitInfoUI : MonoBehaviour
{
    [SerializeField] private Unit unit;
    [SerializeField] private CinemachineCamera targetCamera;

    [Header("UI")]
    [SerializeField] private Transform textParent;
    [SerializeField] private TextMeshProUGUI characterName;
    [Header("Stats")]
    [SerializeField] private LocalizeStringEvent[] statTextes;
    [SerializeField] private string[] stats;
    [Header("Statues")]
    [SerializeField] private GameObject statusTitle;
    [SerializeField] private StatusInfoUI statusInfoPrefab;
    [Header("Attacks")]
    [SerializeField] private TextMeshProUGUI attacksText;

    List<StatusInfoUI> statusInfos = new();

    public void PopulatePanel()
    {
        statusTitle.SetActive(false);

        characterName.text = unit.UnitName;

        for (int i = 0; i < statTextes.Length; i++)
        {
            (statTextes[i].StringReference["value"] as FloatVariable).Value = unit.Character.Stats[stats[i]].Value;
        }

        attacksText.text = unit.AttacksDescription;

        // reset status infos
        if (statusInfos.Count > 0)
        {
            foreach (var statusInfo in statusInfos)
            {
                Destroy(statusInfo.gameObject);
            }

            statusInfos.Clear();
        }

        var statuses = unit.Status.GetAllStatuses();

        if (statuses.Count > 0)
        {
            statusTitle.SetActive(true);

            foreach (var status in statuses)
            {
                var infoInstance = Instantiate(statusInfoPrefab, textParent);
                statusInfos.Add(infoInstance);
                infoInstance.Init(status);
            }
        }
    }

    public void SetPanelOn()
    {
        targetCamera.Target.TrackingTarget = unit.transform;
        targetCamera.Target.LookAtTarget = unit.transform;
    }
}
