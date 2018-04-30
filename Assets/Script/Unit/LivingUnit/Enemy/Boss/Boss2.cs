using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : Enemy
{
    // 파트 프리팹
    public Part_Boss_2 partRightPrefab;
    public Part_Boss_2 partLeftPrefab;

    public Part_Boss_2 rightPart;
    public Part_Boss_2 leftPart;

    // 왼쪽 오른쪽 발사 확인
    public bool rightFire;
    public Bullet bulletPrefab;

    public float partFireDelay;
    private float remainPartFireDelay;

    // 패턴2 온오프
    public bool pattern2On;
    public bool pattern3On;

    //이동
    public float rotationSpeed;

    public float maxMoveSpeed;
    public float originMoveSpeed;
    public float currentMoveSpeed;
    private float targetMoveSpeed;

    public float accelMoveSpeed;

    // 로테이션 
    public float rotationDistansFactor;
    public float rotationAngleFactor;

    //패턴 3
    public float rotationSpeed3;
    public float maxMoveSpeed3;
    private float moveSpeed3;


    protected override void Awake() 
    {
        //보스 2가 생성될때 오른쪽 왼쪽 파트를 생성할꺼라 프리팹 선언해서 넣어줄꺼임
        rightPart = Instantiate<Part_Boss_2>(partRightPrefab);
        leftPart = Instantiate<Part_Boss_2>(partLeftPrefab);
    }

    private void FixedUpdate()
    {
        PartPositonMove();

        if (pattern2On == false) //패턴 2에서는 작동을 못하게
        {
            if (pattern3On == false)
            {
                PartFireBullet();
            }
            MovementFramework();
        }

        
    }

    private void PartPositonMove()
    {
        //오른쪽 파트 밑과 동일
        rightPart.transform.position = new Vector2(transform.position.x, transform.position.y) +
            MyMath.GetRotatedPosition(transform.eulerAngles.z, new Vector3(1.5f, 0.5f, 0));

        rightPart.transform.eulerAngles = this.transform.eulerAngles;

        //왼쪽 파트 실시간으로 보스에 따라다님
        leftPart.transform.position = new Vector2(transform.position.x, transform.position.y) +
            MyMath.GetRotatedPosition(transform.eulerAngles.z, new Vector3(-1.5f, 0.5f, 0));

        leftPart.transform.eulerAngles = this.transform.eulerAngles;

        if(pattern3On == false)
        {
             float alpha = GetComponent<SpriteRenderer>().color.a;
             rightPart.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
             leftPart.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, alpha);
        }
        else
        {
            rightPart.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            leftPart.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
  

    }

    private void PartFireBullet() 
    {
        partFireDelay = 0.5f;
        remainPartFireDelay -= Time.fixedDeltaTime;

        if (remainPartFireDelay < 0f)
        {
            Bullet partBullet = Instantiate<Bullet>(bulletPrefab);

            if (rightFire == true)
            {//-0.2 , 0.78

                Vector3 deltaPos = MyMath.GetRotatedPosition(rightPart.transform.eulerAngles.z, new Vector3(-0.2f, 0.78f, 0f));
                partBullet.transform.position = rightPart.transform.position + deltaPos;
                partBullet.transform.eulerAngles = rightPart.transform.eulerAngles;
                partBullet.direction = partBullet.transform.eulerAngles.z + 90f;
                partBullet.bulletSpeed = 17.5f;
            }

            else
            {
                Vector3 deltaPos = MyMath.GetRotatedPosition(rightPart.transform.eulerAngles.z, new Vector3(+0.2f, 0.78f, 0f));
                partBullet.transform.position = leftPart.transform.position + deltaPos;
                partBullet.transform.eulerAngles = leftPart.transform.eulerAngles;
                partBullet.direction = partBullet.transform.eulerAngles.z + 90f;
                partBullet.bulletSpeed = 17.5f;

            }

            rightFire = !rightFire;

            remainPartFireDelay = partFireDelay;
        }
    }

    private void MovementFramework()
    {
        float targetDirection = MyMath.GetDirection(this.transform.position, PlayerScript.player.transform.position);
        float direction = this.transform.eulerAngles.z + 90f;

        if (this.transform.eulerAngles.z + 90f + 180f < targetDirection)
            targetDirection -= 360f;

        if (this.transform.eulerAngles.z + 90f - 180f > targetDirection)
            targetDirection += 360f;

        float distance = Vector2.Distance(this.transform.position, PlayerScript.player.transform.position);

        rotationSpeed = Mathf.Abs(Mathf.Abs(targetDirection) - Mathf.Abs(direction)) * rotationAngleFactor + distance * rotationDistansFactor;

        direction = MyMath.FixedTowordsAngle(direction, targetDirection, rotationSpeed * Time.fixedDeltaTime);

        this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);

        Vector2 deltaPosition = MyMath.DirectionToVector2(direction);

        targetMoveSpeed = originMoveSpeed * 200f / (Mathf.Abs(Mathf.Abs(targetDirection) - Mathf.Abs(direction)) + 200f);
        targetMoveSpeed = Mathf.Min(maxMoveSpeed, targetMoveSpeed);

        currentMoveSpeed += Mathf.Sign(targetMoveSpeed - currentMoveSpeed) * accelMoveSpeed * Time.fixedDeltaTime;

        this.transform.position = this.transform.position
            + (Vector3)(deltaPosition * currentMoveSpeed * Time.fixedDeltaTime);

    }

    //protected override void OnTriggerEnter2D(Collider2D collision)
    //{
    //    base.OnTriggerEnter2D(collision);

    //    Wall targetUnit = collision.GetComponent<Wall>();
    //    if (targetUnit == null)
    //        return;

    //    // 플레이어 방향으로 회전

    //    float direction = MyMath.GetDirection(this, PlayerScript.player); // 로테이션
    //    this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);

    //    moveSpeed3 = 0f;

    //}

}

