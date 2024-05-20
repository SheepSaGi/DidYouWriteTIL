using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TILAnimationController : MonoBehaviour
{
    private HealthSystem healthSystem;
    private Animator animator;
    //protected TopDownController controller;

    private static readonly int IsWalking = Animator.StringToHash("isWalking");
    private static readonly int IsHit = Animator.StringToHash("isHit");
    private static readonly int Attack = Animator.StringToHash("Attack");

    private readonly float magnituteThreshold = 0.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystem>();
    }

    void Start()
    {
        // �����ϰų� ������ �� �ִϸ��̼��� ���� �����ϵ��� ����
        //controller.OnAttackEvent += Attacking;
        //controller.OnMoveEvent += Move;

        if (healthSystem != null)
        {
            healthSystem.OnDamage += Hit;
            healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }

    // �̵�
    /*private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > magnituteThreshold);
    }*/

    // ����
    private void Attacking(AttackSO obj)
    {
        animator.SetTrigger(Attack);
    }

    // �ǰ�
    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }

    // �ǰݴ��ϴ� ������ ���� ����
    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}
