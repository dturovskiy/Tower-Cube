using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Transform camTransform;
    private float shakeDuration = 0.9f, shakeAmount = 0.04f, decreaseFactor = 1.5f;

    private Vector3 originalPos;

    private void Start()
    {
        camTransform = GetComponent<Transform>();
        originalPos = camTransform.localPosition;
    }

    private void Update()
    {
        if (shakeDuration > 0)
        {
            // Застосовуємо трясіння тільки коли shakeDuration > 0
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            // Зменшуємо тривалість трясіння з часом
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            // Закінчуємо трясіння і повертаємо камеру на початкову позицію
            shakeDuration = 0;
            camTransform.localPosition = originalPos;
        }
    }

    // Метод для запуску трясіння з заданою тривалістю
    public void ShakeCamera(float duration, float intensity)
    {
        shakeDuration = duration;
        shakeAmount = intensity;
        originalPos = camTransform.localPosition; // Зберігаємо початкову позицію камери
    }
}
