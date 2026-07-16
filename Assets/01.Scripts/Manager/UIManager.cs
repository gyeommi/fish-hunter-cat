using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI gemTxt;
    private float gemCnt = 0f;

    [SerializeField] UpgradeButton[] upgradeButtons;
    [SerializeField] GameObject upgradePopup;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        RefreshUpgradeUI();
    }

    public void SetGemText()
    {
        gemCnt++;
        gemTxt.text = $": {gemCnt}";

        if (gemCnt >= 4)
        {
            gemCnt -= 3;
            gemTxt.text = $": {gemCnt}";

            RefreshUpgradeUI();

            upgradePopup.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void RefreshUpgradeUI()
    {
        foreach (var button in upgradeButtons)
        {
            button.Refresh();
        }
    }

    public IEnumerator SelectComplete()
    {
        foreach (UpgradeButton button in upgradeButtons)
            button.GetComponent<Button>().interactable = false;

        yield return new WaitForSecondsRealtime(2f);

        upgradePopup.SetActive(false);

        Time.timeScale = 1f;
    }
}
