using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelUi : MonoBehaviour
{
    [SerializeField] private Image iconPrefab = default;
    [SerializeField] private Transform iconsRoot = default;

    private List<Image> icons = new List<Image>();


    public void CreateIcons(List<QuestObject> questObjects)
    {
        foreach (var questObject in questObjects)
        {
            Image icon = Instantiate(iconPrefab, iconsRoot);
            icon.sprite = questObject.Icon;
            icon.gameObject.SetActive(true);
            icons.Add(icon);
        }
    }


    public void ClearIcons()
    {
        foreach (var icon in icons)
        {
            Destroy(icon.gameObject);
        }

        icons.Clear();
    }
}
