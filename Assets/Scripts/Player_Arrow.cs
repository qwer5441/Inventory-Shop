using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play_Arrow : MonoBehaviour
{
    public Transform launchPoint;//箭点
    public GameObject arrowPrefab;
    private Vector2 aimDirection = Vector2.right;

    public float shootCooldown = .5f;//冷却时间
    private float shootTimer;//计时器
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer -= Time.deltaTime;
        HandleAiming();
        if (Input.GetKeyDown(KeyCode.V)&&shootTimer<=0)
        {
            Shoot();
        }
    }
    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0||vertical!=0)
        {
            aimDirection=new Vector2(horizontal, vertical).normalized;
        }

    }
    public void Shoot()
    {
        Arrow arrow= Instantiate(arrowPrefab, launchPoint.position,Quaternion.identity).GetComponent<Arrow>();
        arrow.direction=aimDirection;
        shootTimer = shootCooldown;
    }
}
