using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllySubBullet1 : AllyBullet
{
    public bool isCharge = true;
    public float growSpeed = 3f;
    private float fDamage;

    public Unit owner;
    private void Awake()
    {
        fDamage = damage;
    }

    new private void FixedUpdate()
    {
        if (isCharge == true)
        {
            if(damage < 3)
            {
                fDamage += Time.fixedDeltaTime;
                damage = (int)Mathf.Round(fDamage); // 두줄로 인해 1초마다 데미지 1증가 mathf.round 반올림 
                //if(damage > 2)
                //{
                //    this.GetComponent<SpriteRenderer>().color = ()
                //} 데미지에 따라 총알의 스프라이트가 다르겐

                transform.localScale += new Vector3(growSpeed, growSpeed) * Time.fixedDeltaTime; // 커진다
            }
            
            transform.position = owner.transform.position; // 이 스킬총알을 실시간으로 플레이어에게 위치시킴
            transform.eulerAngles = owner.transform.eulerAngles; // 실시간으로 총알의 방향도 플레이어랑 같게
        }
        else
        {
            direction = transform.eulerAngles.z + 90f;
            bulletSpeed = 10f;

            transform.position = (Vector2)transform.position + MyMath.DirectionToVector2(direction) * Time.fixedDeltaTime * bulletSpeed;
        }
        
    }

}
