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
    }

    public void Refresh()
    {
        text.text = UpgradeManager.instance.GetUpgradeText(type);

        gameObject.GetComponent<Button>().interactable = true;
    }

    public void OnClick()
    {
        SoundManager.instance.PlaySFX(SFXType.UIClick);

        text.text += "\n 선택 완료";

        UpgradeManager.instance.SetUpgradeStat(type);

        StartCoroutine(UIManager.instance.SelectComplete());
    }
}