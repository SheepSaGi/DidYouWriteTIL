using UnityEngine;

public class DestroyOnDeath : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Rigidbody2D rigidbody;
    private GameManager gameManager;
    
    private AudioSource enemyDeath;

    private void Start()
    {
        healthSystem = GetComponent<HealthSystem>();
        rigidbody = GetComponent<Rigidbody2D>();
        enemyDeath = GetComponent<AudioSource>();
        // ���� ���� ��ü�� healthSystem��
        healthSystem.OnDeath += OnDeath;
        gameManager = FindObjectOfType<GameManager>();


    }

    void OnDeath()
    {
        // ���ߵ��� ����
        rigidbody.velocity = Vector3.zero;

        // �ణ �������� �������� ����
        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        // ȿ�����߻�
        if (gameObject.CompareTag("Enemy"))
        {
            Debug.Log("ȿ�����߻�");
            enemyDeath.Play();
        }

        // ��ũ��Ʈ ���̻� �۵� ���ϵ��� ��
        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }


        if (gameObject.CompareTag("Player"))
        {
            Debug.Log("��");
            gameManager.EndGame();

        }
        // 2�ʵڿ� �ı�
        Destroy(gameObject, 2f);
    }
}