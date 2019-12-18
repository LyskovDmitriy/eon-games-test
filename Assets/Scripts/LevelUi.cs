using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelUi : MonoBehaviour
{
    [SerializeField] private Image iconPrefab = default;
    [SerializeField] private Transform iconsRoot = default;


    public void CreateIcons(List<QuestObject> questObjects)
    {
        foreach (var questObject in questObjects)
        {
            Image icon = Instantiate(iconPrefab, iconsRoot);
            icon.sprite = questObject.Icon;
            icon.gameObject.SetActive(true);
        }
    }
}
