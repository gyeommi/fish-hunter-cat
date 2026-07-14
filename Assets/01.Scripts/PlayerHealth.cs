using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] Image hpImg;
    [SerializeField] Image[] lifeImg;

    private void OnEnable()
    {
        if (hpImg == null)
            hpImg = GetComponent<Image>();

        hpImg.fillAmount = 1f;

        for (int i = 0; i < lifeImg.Length; i++)
        {
            lifeImg[i].enabled = true;
        }
    }

    public void SetHPGauge(float gauge)
    {
        hpImg.fillAmount = gauge;
    }

    public void SetLifeGauge(int index)
    {
        lifeImg[index - 1].enabled = false;
    }
}
