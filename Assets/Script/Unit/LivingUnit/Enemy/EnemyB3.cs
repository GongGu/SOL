using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyB3 : Enemy
{
    private float laserShotDelay = 5f;
    private float remainLaserShotDely;
    private bool isMoveRight;
    public float rotationSpeed;
    public float moveSpeed;

    public LaserBodyBullet laserPrefab;
    public LaserBodyBullet laserRootPrefab;
    public LaserBodyBullet laserWaitPrefab;



    LaserGenerator laser = new LaserGenerator();

    private void Start() // 처음 생성되고 나서 플레이어를 쳐다보게함
    {
        float direction = MyMath.GetDirection(this.transform.position, PlayerScript.player.transform.position);
        this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);
    }


    private void FixedUpdate()
    {
        remainLaserShotDely -= Time.fixedDeltaTime; 

        if(remainLaserShotDely <= 0)
        {
            laser.position = this.transform.position;
            laser.direction = this.transform.eulerAngles.z + 90f;
            laser.count = 50;
            laser.laserPrefab = laserPrefab;
            laser.laserRootPrefab = laserRootPrefab;
            laser.laserWaitPrefab = laserWaitPrefab;
            laser.scale = 2f;
            laser.distance = 1.3f;

            StartCoroutine(laser.GenerateLaser());

            remainLaserShotDely = laserShotDelay;
  
        }   
        else
        {
            if (laser.laserShotting == false)
            {
                float targetDirection = MyMath.GetDirection(this.transform.position, PlayerScript.player.transform.position);
                float direction = MyMath.FixedTowordsAngle(
                    this.transform.eulerAngles.z + 90f,
                    targetDirection,
                    rotationSpeed * Time.fixedDeltaTime);
               
                this.transform.eulerAngles = new Vector3(0, 0, direction - 90f);

                Vector2 deltaPosition;

                if (isMoveRight == true)
                    deltaPosition = MyMath.DirectionToVector2(direction + 90f);
                else
                    deltaPosition = MyMath.DirectionToVector2(direction - 90f);

                this.transform.position = this.transform.position + (Vector3)(deltaPosition * moveSpeed * Time.fixedDeltaTime);


            }
            else
            {   
                this.transform.eulerAngles = this.transform.eulerAngles;
            }
        }


    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        Unit.UnitType unitType = collision.GetComponent<Unit>().type;

        if (unitType != UnitType.WALL)
            return;

        isMoveRight = !isMoveRight;

    }
}
