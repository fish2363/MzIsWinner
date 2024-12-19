using UnityEngine;

public class FloatingUI : MonoBehaviour
{
    public float amplitude = 10f; 
    public float frequency = 1f;  

    private RectTransform rectTransform;
    private Vector3 initialPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            initialPosition = rectTransform.anchoredPosition;
        }
    }
    
    void Update()
    {
        if (rectTransform != null)
        {
            float offset = Mathf.Sin(Time.time * frequency) * amplitude;
            rectTransform.anchoredPosition = new Vector3(
            initialPosition.x + Mathf.Sin(Time.time * frequency) * amplitude,
            initialPosition.y + Mathf.Sin(Time.time * frequency * 1.2f) * amplitude / 2,
            initialPosition.z
        );

        }
    }
}
