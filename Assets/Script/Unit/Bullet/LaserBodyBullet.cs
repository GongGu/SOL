using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBodyBullet : Bullet
{

    public bool isWait;

    private void Start()
    {
        StartCoroutine(LaserProcess());
    }

    IEnumerator LaserProcess()
    {
        Animator animator = GetComponent<Animator>();
        AnimatorStateInfo animationState = animator.GetCurrentAnimatorStateInfo(0);

        if (isWait == false)
            yield return new WaitForSeconds(animationState.length - Time.fixedDeltaTime);
        else
            yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }

    private void FixedUpdate()
    {

    }

    public override void CollisionProcess(Unit targetUnit)
    {
        if (isWait == true)
            return;
        if (targetUnit is PlayerScript == true)
        {
            PlayerScript player = targetUnit as PlayerScript;

            if (player.IsImmune == true)
                return;
            else
                player.IsImmune = true;
        }

        Shield shield = targetUnit.GetComponent<Shield>();
        if (shield != null)
        {
            if (shield.ShieldEnable == true)
            {
                shield.ShieldEnable = false;

                return;
            }
        }

        // target 의 체력 감소
        // 충돌시이펙트 추가예정
        if (targetUnit is LivingUnit)
        {
            LivingUnit livingTarget = targetUnit as LivingUnit;

            livingTarget.CurrentHP -= damage;
        }
    }
}
