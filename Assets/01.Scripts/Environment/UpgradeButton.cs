using System.Collections;
using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] UpgradeType type;
    [SerializeField] TextMeshProUGUI text;

    public UpgradeType Type => type;

    private void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        text.text = UpgradeManager.instance.GetUpgradeText(type);
    }

    public void OnClick()
    {
        text.text += "\n 선택 완료";

        UpgradeManager.instance.SetUpgradeStat(type);

        StartCoroutine(UIManager.instance.SelectComplete());
    }
}