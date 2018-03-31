using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy {


    public float speed = 5f; // 에너미2 스피드
    public float direction = 0f; // 에너미2 방향
    public Vector2 movementVec2D; // 에너미2 이동벡터값

    private void Start()
    {
        if(direction == 0f)
        {
            movementVec2D = ((Vector2)(PlayerScript.player.transform.position - this.transform.position)).normalized;
            direction = MyMath.GetDirection(this, PlayerScript.player); // 방향 = MyMath의 에너미2 정보랑 플레이어 정보 가져감
            this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Unit targetUnit = collision.GetComponent<Unit>();
        if (targetUnit == null)
            return;

        UnitType targetType = targetUnit.type;

        if(targetType == UnitType.WALL)
        {
            // 플레이어 방향으로 회전

            direction = MyMath.GetDirection(this, PlayerScript.player); // 로테이션
            this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);
            movementVec2D = ((Vector2)(PlayerScript.player.transform.position - this.transform.position)).normalized;

        }
    }

    private void Update()
    {
        this.transform.position += (Vector3)(movementVec2D * speed * Time.deltaTime);
    }
}
