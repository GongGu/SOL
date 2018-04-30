using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitType type;

    public int damage = 1;

    public Collider2D colliderForTrigger;

    public enum UnitType
    {
        NONE = 0,

        UNIT,

        LIVING_UNIT,

        ALLY,
        ENEMY,

        ALLY_LIVING_UNIT,
        ENEMY_LIVING_UNIT,

        BULLET,

        ALLY_BULLET,
        ENEMY_BULLET,

        PART,

        ALLY_PART,
        ENEYMY_PART,

        WALL,
    }

    public virtual void Init()
    {

    }

    protected virtual void OnTriggerEnter2D(Collider2D collider) // 유닛 충돌함수을 유닛스크립트로 따로 관리
    {
        Unit targetUnit = collider.GetComponent<Unit>();
        if (targetUnit == null)
            return;
        
        if (targetUnit.colliderForTrigger == null)
        {
            if (targetUnit.GetComponent<Collider2D>() == null)
                return;

            targetUnit.colliderForTrigger = targetUnit.GetComponent<Collider2D>();
        }

        if (targetUnit.colliderForTrigger != collider)
            return;

        UnitType targetType = targetUnit.type;

        if(type == UnitType.ALLY_BULLET &&
            targetType == UnitType.ENEMY_LIVING_UNIT)
        { //아군 총알이 적유닛에 충돌
            CollisionProcess(targetUnit);
        }

        if(type == UnitType.ENEMY_BULLET &&
            targetType == UnitType.ALLY_LIVING_UNIT)
        { //적 총알이 아군 유닛에 충돌
            CollisionProcess(targetUnit);
        }

        if(type == UnitType.ENEMY_LIVING_UNIT &&
            targetType == UnitType.ALLY_LIVING_UNIT)
        {//적 유닛이 아군 유닛에 충돌
            CollisionProcess(targetUnit);
        }

        if(type == UnitType.ENEMY_BULLET &&
            targetType == UnitType.ALLY_PART)
        {//적 총알이 내 파츠에 충돌
            CollisionProcess(targetUnit);
        }
    }

    public virtual void CollisionProcess(Unit targetUnit)
    {
        if (targetUnit is PlayerScript == true)
        {
            PlayerScript player = targetUnit as PlayerScript;

            if (player.IsImmune == true)
                return;
            else
                player.IsImmune = true;
        }

        Shield shield = targetUnit.GetComponent<Shield>();
        if(shield != null)
        {
            if(shield.ShieldEnable == true)
            {
                shield.ShieldEnable = false;

                if (targetUnit is PlayerScript == true)
                {
                    PlayerScript ps = targetUnit as PlayerScript;
                    ps.BeHit(this);
                }

                if (type == UnitType.ALLY_BULLET ||
                    type == UnitType.ENEMY_BULLET)
                    Destroy(gameObject);

                return;
            }
        }

        // target 의 체력 감소
        // 충돌시이펙트 추가예정
        if(targetUnit is LivingUnit)
        {
            LivingUnit livingTarget = targetUnit as LivingUnit;

            livingTarget.CurrentHP -= damage;
        }

        if (targetUnit is PlayerScript == true)
        {
            PlayerScript ps = targetUnit as PlayerScript;
            ps.BeHit(this);
        }

        if (type == UnitType.ALLY_BULLET ||
            type == UnitType.ENEMY_BULLET)
            Destroy(gameObject);
    }



}
