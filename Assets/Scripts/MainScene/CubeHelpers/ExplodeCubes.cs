using UnityEngine;

public class ExplodeCubes : MonoBehaviour
{
    private bool _collisionSet; // �������� ����� ��� ���������� ��������.

    // ������� ��� ��䳿 "ʳ���� ���".
    public delegate void GameOverEvent();
    public static event GameOverEvent OnGameOver;

    // �����, ���� ����������� ��� ������� ��'���� � �����.
    private void OnCollisionEnter(Collision collision)
    {
        // ���� �������� ��� ���� ��������� ��� ��'��� �� �� ���� "Cube", ����� � ������.
        if (_collisionSet || !collision.gameObject.CompareTag("Cube"))
            return;

        _collisionSet = true; // ���������� ���������, �� �������� ���������.

        // ������� ��'���, � ���� ���������.
        Destroy(collision.gameObject);

        // ��������� ���� "ʳ���� ���".
        OnGameOver?.Invoke();

        // �������� ���������� ���� �� ������� ����� ��� ����� ������������.
        ExplodeChildren(collision.transform);
    }

    // ����� ��� ��������� ��'���� �� ����� ���� �� ��� ����������.
    private void ExplodeChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            // ���������, �� �� ������� ��'��� ��������� "Rigidbody".
            if (!child.TryGetComponent(out Rigidbody childRigidbody))
            {
                // ���� �� ��, ������ ��������� "Rigidbody".
                childRigidbody = child.gameObject.AddComponent<Rigidbody>();
            }

            // ������ �������� ���� ��� ������� ���������� ��'����.
            childRigidbody.AddExplosionForce(70f, Vector3.up, 5f);

            // ³��������� ������� ��'��� �� ������������.
            child.SetParent(null);
        }
    }
}
