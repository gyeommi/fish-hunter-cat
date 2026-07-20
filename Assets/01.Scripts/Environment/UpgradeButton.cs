using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] UpgradeType type;
    [SerializeField] TextMeshProUGUI text;

    public UpgradeType Type => type;

    private void OnEnable()
    {
        Refresh();

        //gameObject.GetComponent<Button>().interactable = true;
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