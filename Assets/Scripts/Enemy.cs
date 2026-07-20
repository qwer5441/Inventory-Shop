using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    public int speed = 3;
    public float attackRange = 2f;
    public float attackTime = 2;
    public float nowTime;
    public Transform attackPoint;
    public float weaponRange;
    public float knockbackForce;
    public float sunTime;
    public int MaxHP;
    public int currentHP;
    public float ChaseRange;
    public int expReward =2;
    public delegate void MonsterDefeated(int exp);
    public static event MonsterDefeated OnMonsterDefeated;
    [SerializeField]
    private int atkDamage = 10;

    private Transform player;
    private Rigidbody2D rb;
    private bool isChasing;
    private Animator animator;
    private int facingDirection = -1;
    private bool isKnockBack = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHP = MaxHP;
    }

    private void Update()
    {
        UpdateEnemyLogic();
        CheckChaseRange();
    }

    private void UpdateEnemyLogic()
    {
        // 被击退时直接退出
        if (isKnockBack)  
            return;
        
        // 没有追击目标 → 直接待机
        if (!isChasing || player == null)
        {
            Idle();
            return;
        }

        // 判断是否在攻击范围
        if (CheckInAttackRange())
        {
            AttackState();
        }
        else
        {
            ChaseState();
        }
    }

    // 待机
    private void Idle()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("Move", false);
        animator.SetBool("Attack", false);
    }

    // 追击状态
    private void ChaseState()
    {
        animator.SetBool("Move", true);
        animator.SetBool("Attack", false);
        movement();
    }

    // 攻击状态
    private void AttackState()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("Move", false);

        if (Time.time - nowTime > attackTime)
        {
            nowTime = Time.time;
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    // 攻击动画事件
    public void AttackEvent()
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, weaponRange, 1 << LayerMask.NameToLayer("Player"));
        if (collider != null)
        {
            collider.gameObject.GetComponent<Player>().ChangeHealth(-atkDamage);
            collider.gameObject.GetComponent<Player>().KnockBack(transform, knockbackForce, sunTime);
        }
    }

    // 攻击范围检测
    private bool CheckInAttackRange()
    {
        if (player == null) return false;
        return Vector2.Distance(player.position, transform.position) < attackRange;
    }

    // 移动逻辑
    void movement()
    {
        if (isChasing && !CheckInAttackRange()&&!isKnockBack)
        {
            if (player.position.x > transform.position.x && transform.localScale.x == -1 ||
                player.position.x < transform.position.x && transform.localScale.x == 1)
            {
                transform.localScale = new Vector2(transform.localScale.x * facingDirection, transform.localScale.y);
            }
            Vector2 dir = (player.position - transform.position).normalized;
            rb.velocity = dir * speed;
        }
    }
    /// <summary>
    /// 改变血量
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHP(int amount)
    {
        currentHP -= amount;
        if(currentHP <= 0)
        {
            currentHP = 0;
            OnMonsterDefeated?.Invoke(expReward);
            Destroy(gameObject);
        }
    }

    public void KnockBack(Transform forceTransform,float force, float lockTime,float stunTime)
    {
        isKnockBack = true;
        animator.SetBool("Move", false);
        animator.SetBool("Attack", false);
        Vector2 dir=(transform.position- forceTransform.position).normalized;
        rb.velocity= dir * force;
        StartCoroutine(KnockCounter(lockTime,stunTime));
    }
    IEnumerator KnockCounter(float lockTime,float stunTime)
    {
        //第一段：击退锁定（不能动）
        yield return new WaitForSeconds(lockTime);
        rb.velocity = Vector2.zero;

        // 第二段：晕眩（继续不能动）
        yield return new WaitForSeconds(stunTime);
        isKnockBack = false;
    }
    // 触发器进入
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        if (player == null)
    //        {
    //            player = collision.transform;
    //        }
    //        isChasing = true;
    //    }
    //}
    private void CheckChaseRange()
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, ChaseRange, 1 << LayerMask.NameToLayer("Player"));
        if(collider != null)
        {
            player = collider.transform;
            isChasing = true;

        }
        else
        {
            player =null;
            isChasing = false;
        }
    }
    // 触发器退出
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        isChasing = false;
    //        player = null;
    //    }
    //}
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(attackPoint.position, ChaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, weaponRange);
    }

}