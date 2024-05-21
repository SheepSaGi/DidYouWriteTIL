using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HealthUIManager : MonoBehaviour
{
    [SerializeField] private HealthSystem characterHealth;
    [SerializeField] private GameObject healthtPrefab; // ü�� UI ������
    [SerializeField] private Transform healthsParent; // ü�� UI�� �θ� ��ü

    private List<GameObject> healths = new List<GameObject>(); // ü�� UI�� ������ ����Ʈ

    private int lastIndex;

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
        if (lastIndex >= 0)
        {
            GameObject health = healths[lastIndex--];
            health.SetActive(false);
        }
    }

    // ȸ�� �̺�Ʈ �ڵ鷯
    private void OnHealReceived()
    {
        // ȸ���� ������ ���� �����ʿ� ��Ȱ��ȭ�� ü��UI Ȱ��ȭ
        
        if (lastIndex >= 0)
        {
            GameObject health = healths[lastIndex++];
            health.SetActive(true);
        }
    }

    // �÷��̾��� �ִ� ü�¿� ���� UI�� ���� �� ���� ü�¸�ŭ Ȱ��ȭ
    private void CreateUI()
    {
        for (int i = 0; i < characterHealth.MaxHealth; i++)
        {
            GameObject health = Instantiate(healthtPrefab, healthsParent);
            healths.Add(health);
            healths[i].SetActive(true);
        }
        lastIndex = healths.Count - 1;
    }
}