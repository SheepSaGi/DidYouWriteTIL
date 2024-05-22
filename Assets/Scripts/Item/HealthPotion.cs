﻿using Unity.VisualScripting;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    [SerializeField] private int healAamount = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            collision.GetComponent<HealthSystem>().ChangeHealth(healAamount);
            gameObject.SetActive(false);
        }
    }
}