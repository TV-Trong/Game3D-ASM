using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[SerializeField]
public class Mainmenu : MonoBehaviour
{
    public CanvasGroup screenBeforeTransition; // Màn hình hiện tại (CanvasGroup)
    public CanvasGroup screenAfterTransition; // Màn hình mới (CanvasGroup)
    public float transitionDuration = 1f; // Thời gian làm mờ

    // Hàm chuyển đổi
    public void StartTransition()
    {
        StartCoroutine(Transition());
    }

    private IEnumerator Transition()
    {
        // Làm mờ dần màn hình hiện tại
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / transitionDuration);
            screenBeforeTransition.alpha = alpha;
            yield return null;
        }
        screenBeforeTransition.gameObject.SetActive(false); // Ẩn màn hình trước

        // Hiển thị màn hình mới
        screenAfterTransition.gameObject.SetActive(true);
        elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, elapsedTime / transitionDuration);
            screenAfterTransition.alpha = alpha;
            yield return null;
        }
    }
}
