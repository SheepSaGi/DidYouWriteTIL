using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class DIsplayHealthCount : MonoBehaviour
{
    [SerializeField] private HealthSystem characterHealth;
    private ObjectPool objectPool;
    private Stack<GameObject> healthUIs;

    private void Awake()
    {
        //3�� characterHealth.CurrentHealth ���� ����
        //foreach (var healthUI in healthUIs)
        //{
        //    healthUI.SetActive(false);
        //}
    }

    private void Update()
    {
        //characterHealth.OnDamage += DisableHealthCount;
    }

    private void DisableHealthCount()
    {

    }
}
