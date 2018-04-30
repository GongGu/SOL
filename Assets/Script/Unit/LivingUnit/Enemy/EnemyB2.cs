using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB2 : Enemy
{
    public float rotationSpeed;
    public float maxMoveSpeed;
    private float moveSpeed;
    public float accelSpeed;

    private float turnDelay;
    private float remainTurnDelay;

    public Bullet bulletPrefab;



    private void FixedUpdate()
    {
        float targetDirection = MyMath.GetDirection(this.transform.position, PlayerScript.player.transform.position);
        float direction = this.transform.eulerAngles.z + 90f;

        if (this.transform.eulerAngles.z + 90f + 180f < targetDirection)
            targetDirection -= 360f;

        if (this.transform.eulerAngles.z + 90f - 180f > targetDirection)
            targetDirection += 360f;

        float distance = Vector2.Distance(this.transform.position, PlayerScript.player.transform.position) / 5f;

        rotationSpeed = Mathf.Abs(Mathf.Abs(targetDirection) - Mathf.Abs(direction)) + distance * 15f;

        rotationSpeed *= 0.25f;

        direction = MyMath.FixedTowordsAngle(direction, targetDirection, rotationSpeed * Time.fixedDeltaTime);

        this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);

        Vector2 deltaPosition = MyMath.DirectionToVector2(direction);

        this.transform.position = this.transform.position
            + (Vector3)(deltaPosition * moveSpeed * Time.fixedDeltaTime);


        turnDelay = 2f;
        remainTurnDelay -= Time.fixedDeltaTime;


        moveSpeed = Mathf.MoveTowards(moveSpeed, maxMoveSpeed, maxMoveSpeed * accelSpeed * Time.fixedDeltaTime);
    }


    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Wall targetUnit = collision.GetComponent<Wall>();
        if (targetUnit == null)
            return;

        // 플레이어 방향으로 회전

        float dir = 0f;

        switch(targetUnit.direction)
        {
            case Wall.Direction.UP:
                dir = 180f;
                break;
            case Wall.Direction.RIGHT:
                dir = 90f;
                break;
            case Wall.Direction.LEFT:
                dir = 270f;
                break;
        }

        for (int i = 0; i < 9; ++i)
        {
            Bullet bullet = Instantiate<Bullet>(bulletPrefab);

            float angle = (180f / 8f * (float)i) + dir;

            bullet.direction = angle;
            bullet.bulletSpeed = 5f;
            bullet.transform.position = this.transform.position;
        }

        float direction = MyMath.GetDirection(this, PlayerScript.player); // 로테이션
        this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);

        moveSpeed = 0f;
        
    }
}