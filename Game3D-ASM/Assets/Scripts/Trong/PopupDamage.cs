using UnityEngine;
using TMPro;

public class PopupDamage : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private float fadeDuration = 1f;
    private TMP_Text textMesh;
    private Color textColor;
    private Camera mainCam;

    private void Awake()
    {
        textMesh = GetComponent<TMP_Text>();
        textColor = textMesh.color;
        mainCam = Camera.main;
        //MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        //meshRenderer.sortingLayerName = "UI";
        //meshRenderer.sortingOrder = -1;
    }

    private void Update()
    {
        transform.LookAt(transform.position +  mainCam.transform.rotation * Vector3.forward,
                         mainCam.transform.rotation * Vector3.up);
        transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);
        textColor.a -= Time.deltaTime / fadeDuration;
        textMesh.color = textColor;
        if (textColor.a <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void Setup(float damageAmount)
    {
        textMesh.text = damageAmount.ToString();
        textColor.a = 1f;
    }

    public void SetDamageColor(Color newColor)
    {
        textColor = newColor;
    }
}
