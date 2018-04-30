using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB1 : Enemy
{
    public float rotationSpeed;
    public float moveSpeed;

    private float turnDelay;
    private float remainTurnDelay;


    private void FixedUpdate()
    {

        MovementFramework();

    }

    private void MovementFramework()
    {
        float targetDirection = MyMath.GetDirection(this.transform.position, PlayerScript.player.transform.position);
        float direction = this.transform.eulerAngles.z + 90f;

        if (this.transform.eulerAngles.z + 90f + 180f < targetDirection)
            targetDirection -= 360f;

        if (this.transform.eulerAngles.z + 90f - 180f > targetDirection)
            targetDirection += 360f;

        float distance = Vector2.Distance(this.transform.position, PlayerScript.player.transform.position) / 5f;

        rotationSpeed = Mathf.Abs(Mathf.Abs(targetDirection) - Mathf.Abs(direction)) + distance * 150f;

        direction = MyMath.FixedTowordsAngle(direction, targetDirection, rotationSpeed * Time.fixedDeltaTime);

        this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);

        Vector2 deltaPosition = MyMath.DirectionToVector2(direction);

        this.transform.position = this.transform.position
            + (Vector3)(deltaPosition * moveSpeed * Time.fixedDeltaTime);


        turnDelay = 2f;
        remainTurnDelay -= Time.fixedDeltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);


        Unit.UnitType unitType = collision.GetComponent<Unit>().type;

       switch(unitType)
        {
            case UnitType.ENEMY_LIVING_UNIT:
            case UnitType.ENEMY_BULLET:
            case UnitType.ENEYMY_PART:
            case UnitType.ALLY_LIVING_UNIT:
                return;
        }

        if (remainTurnDelay <= 0f)
        {
            this.transform.eulerAngles = new Vector3(0f, 0f, this.transform.eulerAngles.z + 180f);
            remainTurnDelay = turnDelay;
        }

    }

}
