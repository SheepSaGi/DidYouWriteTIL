using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private HealthSystem characterHealth;
    [SerializeField] private GameObject heartPrefab; // ��Ʈ UI ������
    [SerializeField] private Transform heartsParent; // ��Ʈ UI�� �θ� ��ü

    private List<GameObject> hearts = new List<GameObject>(); // ��Ʈ UI�� ������ ����Ʈ

    private void Start()
    {
        // ���ؿ� ȸ�� �̺�Ʈ�� ���� ������ ���
        characterHealth.OnDamage += OnDamageTaken;
        characterHealth.OnHeal += OnHealReceived;

        CreateUI();
    }

    private void OnDestroy()
    {
        // ������ ����
        characterHealth.OnDamage -= OnDamageTaken;
        characterHealth.OnHeal -= OnHealReceived;
    }

    // ���� �̺�Ʈ �ڵ鷯
    private void OnDamageTaken()
    {
        // ���ظ� ������ ���� �����ʿ� �ִ� ü��UI ��Ȱ��ȭ
        int lastIndex = hearts.Count - 1;
        if (lastIndex >= 0)
        {
            GameObject heart = hearts[lastIndex];
            heart.SetActive(false);
        }
    }

    // ȸ�� �̺�Ʈ �ڵ鷯
    private void OnHealReceived()
    {
        // ȸ���� ������ ���� �����ʿ� ��Ȱ��ȭ�� ü��UI Ȱ��ȭ
        int lastIndex = hearts.Count - 1;
        if (lastIndex >= 0)
        {
            GameObject heart = hearts[lastIndex];
            heart.SetActive(true);
        }
    }

    // �÷��̾��� �ִ� ü�¿� ���� UI�� ���� �� ���� ü�¸�ŭ Ȱ��ȭ
    private void CreateUI()
    {       
        int currentHealth = characterHealth.CurrentHealth;
        for (int i = 0; i < characterHealth.MaxHealth; i++)
        {
            GameObject heart = Instantiate(heartPrefab, heartsParent);
            hearts.Add(heart);
            hearts[i].SetActive(i < currentHealth);
        }
    }
}
