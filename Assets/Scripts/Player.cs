using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int facingDirection = 1;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isKnockback = false;

    //public float attackRange;
    [Header("UI")]
    public TMP_Text txt_HP;

    public Animator anim_txt_HP;

    public Transform attackPoint;



    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Attack();

    }
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Attack", true);
        }
    }
    public void AttackDamageEvent()
    {

        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPoint.position,PlayerManager.instance.attackRange, 1<<LayerMask.NameToLayer("Enemy"));

        if (colliders.Length > 0)
        {
            colliders[0].gameObject.GetComponent<Enemy>().ChangeHP(PlayerManager.instance.atk);
            colliders[0].gameObject.GetComponent<Enemy>().KnockBack(transform, PlayerManager.instance.knockBackForce, PlayerManager.instance.knockBackTime, PlayerManager.instance.stunTime);
        }

    }
    public void AttackFinishEvent()
    {
        animator.SetBool("Attack", false);
    }

    private void FixedUpdate()
    {
        Movement();
    }


    /// <summary>
    /// 移动控制
    /// </summary>
    void Movement()
    {
        if (!isKnockback)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0 && transform.localScale.x < 0 ||
                horizontal < 0 && transform.localScale.x > 0)
            {
                facingDirection = -1;
                transform.localScale = new Vector2(transform.localScale.x * facingDirection, transform.localScale.y);
            }

            animator.SetFloat("Horizontal", Mathf.Abs(horizontal));
            animator.SetFloat("Vertical", Mathf.Abs(vertical));


            rb.velocity = new Vector2(horizontal, vertical) * PlayerManager.instance.speed;
        }
       
    }


    /// <summary
    /// 改变血量
    /// </summary>
    /// <param name="amount"></param>
    public void ChangeHealth(int amount)
    {
        anim_txt_HP.Play("text_HP");
        PlayerManager.instance.currentHealth += amount;
        txt_HP.text = "HP:" + PlayerManager.instance.currentHealth + "/" + PlayerManager.instance.MaxHealth;
        if (PlayerManager.instance.currentHealth <= 0)
        {
            PlayerManager.instance.currentHealth = 0;
            gameObject.SetActive(false);
        }
    }




    /// <summary>
    /// 被击退
    /// </summary>
    /// <param name="enemy"></param>
    /// <param name="force"></param>
    /// <param name="stunTime"></param>
    public void KnockBack(Transform enemy,float force,float stunTime)
    {
        isKnockback=true;
        Vector2 direction=(transform.position-enemy.position).normalized;
        rb.velocity=direction*force;
        StartCoroutine(KnocbackCounter(stunTime));
    }
    IEnumerator KnocbackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.velocity=Vector2.zero;
        isKnockback = false;
    }
    
    
    
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, PlayerManager.instance.attackRange);
    }
}
