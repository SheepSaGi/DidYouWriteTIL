using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // ���� �ε����� �� ������ ���̾�
    [SerializeField] private LayerMask levelCollisionLayer;

    private AttackSO attackData;
    private float currentDuration;
    private Vector2 direction;
    private bool isReady;

    private Rigidbody2D rigidbody;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;

    public bool fxOnDestory = true;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (!isReady)
        {
            return;
        }

        currentDuration += Time.deltaTime;

        if (currentDuration > attackData.duration)
        {
            DestroyBullet(transform.position, false);
        }

        rigidbody.velocity = direction * attackData.speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // levelCollisionLayer�� ���ԵǴ� ���̾����� Ȯ���մϴ�.
        if (IsLayerMatched(levelCollisionLayer.value, collision.gameObject.layer))
        {
            // �������� �浹�� �������κ��� �ణ �� �ʿ��� �߻�ü�� �ı��մϴ�.
            Vector2 destroyPosition = collision.ClosestPoint(transform.position) - direction * .2f;
            DestroyBullet(destroyPosition, fxOnDestory);
        }
        // _attackData.target�� ���ԵǴ� ���̾����� Ȯ���մϴ�.
        else if (IsLayerMatched(attackData.target.value, collision.gameObject.layer))
        {
            // �ƾ�! �ǰ� �������� �߰� ����
            // �浹�� �������� �߻�ü�� �ı��մϴ�.
            DestroyBullet(collision.ClosestPoint(transform.position), fxOnDestory);
        }
    }

    // ���̾ ��ġ�ϴ��� Ȯ���ϴ� �޼ҵ��Դϴ�.
    private bool IsLayerMatched(int layerMask, int objectLayer)
    {
        return layerMask == (layerMask | (1 << objectLayer));
    }

    public void InitializeAttack(Vector2 direction, AttackSO attackData)
    {
        this.attackData = attackData;
        this.direction = direction;

        trailRenderer.Clear();
        currentDuration = 0;

        transform.right = this.direction;

        isReady = true;
    }

    private void DestroyBullet(Vector3 position, bool createFx)
    {
        if (createFx)
        {
            // TODO : ParticleSystem�� ���ؼ� ����, ���� NameTag�� �ش��ϴ� FX��������
        }
        gameObject.SetActive(false);
    }
}
