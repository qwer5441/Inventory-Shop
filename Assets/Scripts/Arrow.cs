using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    public Vector2 direction = Vector2.right;
    public float lifeSpawn = 2;
    public float speed;

    public LayerMask enemyLayer;
    public LayerMask obstacleLayer;

    public SpriteRenderer sr;
    public Sprite buriedSprite;

    public int damge;
    public float knockbackForce;//击退力量
    public float knockbackTime;//击退多长时间停止
    public float stumTime;//击退结束后敌人会眩晕多久
    private void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        rb.velocity = direction*speed;
        RotateArrow();
        Destroy(gameObject, lifeSpawn);
    }
    private void RotateArrow()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((enemyLayer.value & (1 << collision.gameObject.layer))>0)
        {
            collision.gameObject.GetComponent<Enemy>().ChangeHP(-damge);
            collision.gameObject.GetComponent<Enemy>().KnockBack(transform, knockbackForce, knockbackTime, stumTime);
            AttachToTarget(collision.gameObject.transform);
        }
        else if ((obstacleLayer.value & (1 << collision.gameObject.layer)) > 0)
        {
            AttachToTarget(collision.gameObject.transform);
        }
    }

    private void AttachToTarget(Transform target)
    {
        sr.sprite = buriedSprite;
        rb.velocity=Vector2.zero;
        rb.isKinematic = true;
        transform.SetParent(target);
    }
}
