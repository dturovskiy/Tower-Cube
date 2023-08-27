using UnityEngine;

public class ExplodeCubes : MonoBehaviour
{
    private bool _collisionSet; // Приватна змінна для відстеження зіткнення.

    // Делегат для події "Кінець гри".
    public delegate void GameOverEvent();
    public static event GameOverEvent OnGameOver;

    // Метод, який викликається при зіткненні об'єкту з іншим.
    private void OnCollisionEnter(Collision collision)
    {
        // Якщо зіткнення вже було оброблено або об'єкт не має тегу "Cube", вийти з методу.
        if (_collisionSet || !collision.gameObject.CompareTag("Cube"))
            return;

        _collisionSet = true; // Встановити прапорець, що зіткнення оброблено.

        // Знищити об'єкт, з яким зіткнулися.
        Destroy(collision.gameObject);

        // Викликати подію "Кінець гри".
        OnGameOver?.Invoke();

        // Розділити розсипання кубів на окремий метод для кращої читабельності.
        ExplodeChildren(collision.transform);
    }

    // Метод для розділення об'єктів на окремі куби та їхнє розсипання.
    private void ExplodeChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // Перевірити, чи має дочірній об'єкт компонент "Rigidbody".
            if (!child.TryGetComponent(out Rigidbody childRigidbody))
            {
                // Якщо не має, додати компонент "Rigidbody".
                childRigidbody = child.gameObject.AddComponent<Rigidbody>();
            }

            // Додати вибухову силу для кожного дочірнього об'єкту.
            childRigidbody.AddExplosionForce(70f, Vector3.up, 5f);

            // Відокремити дочірній об'єкт від батьківського.
            child.SetParent(null);
        }
    }
}
