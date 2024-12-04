using TMPro;
using UnityEngine;

public class ZomGameManager : MonoBehaviour
{
    public float time = 60;
    [SerializeField] private TMP_Text textTime;

    void Update()
    {
        CountdownTimer();
    }

    void CountdownTimer()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            int intTime = (int)time;
            textTime.text = intTime.ToString();
            Debug.Log($"Time remaining: {time:F2} seconds");
        }
        else
        {
            time = 0;
            int intTime = (int)time;
            textTime.text = intTime.ToString();
            textTime.gameObject.SetActive(false);
            Time.timeScale = 0;
            Debug.Log("Time's up! Fight over.");
            // Thêm logic khi hết giờ, ví dụ: dừng tấn công, kích hoạt hành động khác
        }
    }
}