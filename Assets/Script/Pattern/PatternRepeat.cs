using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternRepeat : Pattern
{
    public Bullet bulletPrefab;
    public List<Bullet> spawnedBulletList = new List<Bullet>();

    public Unit target;
    public Unit fireRoot;

    public float bulletSpeed = 3f;

    public float deltaDirection;

    public int count = 1;

    public float delay = 0f;

    public override IEnumerator PatternFramework(Pattern prevPattern)
    {
        spawnedBulletList.Clear();

        for (int i = 0; i < count; ++i)
        {
            float targetDirection = MyMath.GetDirection(fireRoot.transform.position, target.transform.position);

            Bullet bullet = MonoBehaviour.Instantiate(bulletPrefab);
            spawnedBulletList.Add(bullet);

            bullet.direction = targetDirection;

            bullet.speed = bulletSpeed;

            bullet.transform.position = fireRoot.transform.position;

            fireRoot.transform.eulerAngles = new Vector3(0f, 0f, targetDirection + deltaDirection);

            if (delay > 0f) //delay가 0의 값을 가지더라도 WaitForSesconds는 최소한 한 프레임을 기다리기때문에 딜레이가 0보다 클때만 기다린다
                yield return new WaitForSeconds(delay);
        }
    }
}
