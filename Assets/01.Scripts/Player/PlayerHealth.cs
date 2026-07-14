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

        SetLife();
    }

    public void SetHPGauge(float gauge)
    {
        hpImg.fillAmount = gauge;
    }

    public void SetLife()
    {
        for (int i = 0; i < lifeImg.Length; i++)
        {
            lifeImg[i].enabled = true;
        }
    }

    public void DecreaseLife(int index)
    {
        lifeImg[index].enabled = false;
    }
}
