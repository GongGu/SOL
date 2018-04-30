using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternCircle : Pattern
{
    public Bullet bulletPrefab;
    public List<Bullet> spawnedBulletList = new List<Bullet>();

    public Vector2 position = new Vector2(0f, 0f);

    public int count = 1;
    public float angle = 360f;
    public float direction  ;
    public float directionRatio = 0f;

    public float bulletSpeed = 3f;

    public float distance = 0f;
    public float delay = 0f;

    public override IEnumerator PatternFramework(Pattern prevPattern)
    {
        spawnedBulletList.Clear();

        for (int i = 0; i < count; ++i)
        {
            float targetDirection = (float)i / ((float)count) * angle + direction - angle * 0.5f;
            targetDirection += (float)1 / (float)count * angle * directionRatio;

            Bullet bullet = MonoBehaviour.Instantiate(bulletPrefab);
            spawnedBulletList.Add(bullet);

            bullet.direction = targetDirection;

            bullet.bulletSpeed = bulletSpeed;

            Vector2 deltaPosition = MyMath.DirectionToVector2(targetDirection) * distance;

            position = owner.transform.position + (Vector3)deltaPosition;

            bullet.transform.position = position;

            if (delay > 0f) //delay가 0의 값을 가지더라도 WaitForSesconds는 최소한 한 프레임을 기다리기때문에 딜레이가 0보다 클때만 기다린다
                yield return new WaitForSeconds(delay);
        }
    }
}
