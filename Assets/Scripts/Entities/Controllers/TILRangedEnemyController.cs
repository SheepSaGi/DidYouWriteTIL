using System.Security.Claims;
using UnityEngine;

public class TILRangedEnemyController : TILEnemyController
{
    [SerializeField] private float followRange = 9f;
    [SerializeField] private float shootRange = 7f;
    [SerializeField] private string targetTag = "Player";

    private bool isCollidingWithTarget;

    private HealthSystem healthSystem;
    private HealthSystem collidingTargetHealthSystem;
    private TILMovement collidingMovement;


    private int layerMaskLevel;
    private int layerMaskTarget;

    protected override void Start()
    {
        base.Start();
        layerMaskLevel = LayerMask.NameToLayer("Level");
        layerMaskTarget = stats.CurrentStat.attackSO.target;

        healthSystem = GetComponent<HealthSystem>();
        //healthSystem.OnDamage += OnDamage;//���� �ʿ������ ����
    }
    //private void OnDamage()//���� �ʿ������ ����
    //{
    //    followRange = 6f;
    //}

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (isCollidingWithTarget)
        {
            ApplyHealthChange();
        }

        float distanceToTarget = DistanceToTarget();
        Vector2 directionToTarget = DirectionToTarget();

        UpdateEnemyState(distanceToTarget, directionToTarget);
    }

    private void UpdateEnemyState(float distance, Vector2 direction)
    {
        IsAttacking = false; // �⺻������ ���� ���¸� false�� �����մϴ�.

        if (distance <= followRange)
        {
            CheckIfNear(distance, direction);
        }
    }

    private void CheckIfNear(float distance, Vector2 direction)
    {
        if (distance <= shootRange)
        {
            TryShootAtTarget(direction);
        }
        else
        {
            CallMoveEvent(direction); // �����Ÿ� �������� ���� ���� ���� ���� ���, Ÿ�� ������ �̵��մϴ�.
        }
    }

    private void TryShootAtTarget(Vector2 direction)
    {
        // ���� ��ġ���� direction �������� ���̸� �߻��մϴ�.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, shootRange, GetLayerMaskForRaycast());

        // ���� ������ �ƴ϶� ���� �÷��̾ �¾Ҵ��� Ȯ���մϴ�.
        if (IsTargetHit(hit))
        {
            PerformAttackAction(direction);
            Debug.Log("5");

        }
        else
        {
            PerformAttackAction(direction);
            CallMoveEvent(direction);
            Debug.Log("6");

        }
    }

    private int GetLayerMaskForRaycast()
    {
        // "Level" ���̾�� Ÿ�� ���̾� ��θ� �����ϴ� LayerMask�� ��ȯ�մϴ�.
        return (1 << layerMaskLevel) | layerMaskTarget;
    }

    private bool IsTargetHit(RaycastHit2D hit)
    {
        // RaycastHit2D ����� �������� ���� Ÿ���� �����ߴ��� Ȯ���մϴ�.
        return hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer));
    }

    private void PerformAttackAction(Vector2 direction)
    {
        // Ÿ���� ��Ȯ�� �������� ����� �ൿ�� �����մϴ�.
        CallLookEvent(direction);
        CallMoveEvent(Vector2.zero); // ���� �߿��� �̵��� ����ϴ�.
        IsAttacking = true;
        Debug.Log("������");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject receiver = collision.gameObject;

        if (!receiver.CompareTag(targetTag))
        {
            return;
        }

        collidingTargetHealthSystem = receiver.GetComponent<HealthSystem>();
        if (collidingTargetHealthSystem != null)
        {
            isCollidingWithTarget = true;
        }

        collidingMovement = receiver.GetComponent<TILMovement>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(targetTag))
        {
            return;
        }

        isCollidingWithTarget = false;
    }

    private void ApplyHealthChange()
    {
        Debug.Log("�ʴ�");
        AttackSO attackSO = stats.CurrentStat.attackSO;
        bool hasBeenChanged = collidingTargetHealthSystem.ChangeHealth(-(int)attackSO.power);
    }
}