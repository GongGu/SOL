using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGenerator
{
    public float direction;
    public Vector2 position;
    public int count;
    public float distance;
    public float scale;
    public bool laserShotting;

    public float preDelay;

    public LaserBodyBullet laserPrefab;
    public LaserBodyBullet laserRootPrefab;
    public LaserBodyBullet laserWaitPrefab;

    public IEnumerator GenerateLaser()
    {
        laserShotting = true;

        yield return new WaitForSeconds(preDelay);

        // wait 생성
        for (int i = 0; i< count; ++i)
        {
            Vector2 targetPos = position + MyMath.DirectionToVector2(direction) * 0.32f * scale * (i + distance);
            LaserBodyBullet laser = UnityEngine.MonoBehaviour.Instantiate(laserWaitPrefab);

            
            laser.isWait = true;

            laser.transform.localScale = new Vector3(scale , scale, scale);
            laser.transform.position = targetPos;
            laser.transform.eulerAngles = new Vector3(0, 0, direction);
        }

        yield return new WaitForSeconds(0.5f);

        for(int i = 0; i < count; ++i)
        {
            Vector2 targetPos = position + MyMath.DirectionToVector2(direction) * 0.32f * scale * (i + distance);
            LaserBodyBullet laser = null;

            if (i == 0)
                laser = UnityEngine.MonoBehaviour.Instantiate(laserRootPrefab);
            else
                laser = UnityEngine.MonoBehaviour.Instantiate(laserPrefab);


            laser.isWait = false;

            laser.transform.localScale = new Vector3(scale * 0.5f, scale * 0.5f, scale * 0.5f);
            laser.transform.position = targetPos;
            laser.transform.eulerAngles = new Vector3(0, 0, direction + 90f);
        }

        yield return new WaitForSeconds(0.2f);

        laserShotting = false;

    }
}
