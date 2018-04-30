using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyA1 : Enemy
{
    public Bullet bulletPrefab;
    public float fireDeley;
    private float remainFireDeley;

    private void Update()
    {
        //EnemyMove();
        EnemyRotate();
        EnemyFire();
    }

    //void EnemyMove()
    //{
    //    float enemySpeed = 3f; // 기본 잡몹 티어1 스피드
    //    this.transform.position = this.transform.position + (new Vector3(0, -1, 0) * enemySpeed * Time.deltaTime);
    //    // 밑으로만 내려가게 y축 -1
    //}

    void EnemyRotate()
    {
        float enemy_Dir = MyMath.GetDirection(this, PlayerScript.player); // 플레이어 방향으로 쳐다보게 회전
        this.transform.eulerAngles = new Vector3(0, 0, enemy_Dir - 90f); // 회전값 받아서 회전

    }

    void EnemyFire()
    {
        fireDeley = 3f; // 적이 총알 쏘는 시간
        remainFireDeley += Time.deltaTime; // Fire 시간까지 남은 시간

        if (fireDeley < remainFireDeley) // 
        {
            Bullet bullet = Instantiate<Bullet>(bulletPrefab); // 총알 생성

            //if (playerScript == null)
            //    playerScript = FindObjectOfType<PlayerScript>();// <>속의 타입에 해당하는 컴포넌트나 게임오브젝트등을 찾아서 리턴

            bullet.direction = MyMath.GetDirection(this.transform.position, PlayerScript.player.transform.position); // 방향 정규화
            bullet.transform.position = this.transform.position; // 총알 좌표를 enemy1 좌표로 이동
            bullet.transform.eulerAngles = this.transform.eulerAngles; // 총알 회전값 enemy1 변경
            bullet.bulletSpeed = 20f; // enemy1이 쏘는 총알 스피드

            remainFireDeley = 0; // Fire시간 초기화
        }


    }

}



