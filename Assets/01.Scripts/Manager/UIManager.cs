using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] TextMeshProUGUI gemTxt;
    private float gemCnt = 0f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void SetGemText()
    {
        gemCnt++;
        gemTxt.text = $": {gemCnt}";
    }
}
