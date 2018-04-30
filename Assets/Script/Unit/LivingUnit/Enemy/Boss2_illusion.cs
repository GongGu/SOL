using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_illusion : Enemy
{
    public LaserBodyBullet laserPrefab;
    public LaserBodyBullet laserRootPrefab;
    public LaserBodyBullet laserWaitPrefab;
    LaserGenerator laser = new LaserGenerator(); // 레이저


    private void Start()
    {
        StartCoroutine(DeleyedFire());
    }

    IEnumerator DeleyedFire()
    {
        StartCoroutine(AlpahBlend(1f));
        yield return new WaitForSeconds(1f);

        laser.position = this.transform.position;
        laser.direction = this.transform.eulerAngles.z + 90f;
        laser.count = 50;
        laser.laserPrefab = laserPrefab;
        laser.laserRootPrefab = laserRootPrefab;
        laser.laserWaitPrefab = laserWaitPrefab;
        laser.scale = 2f;
        laser.distance = -1.5f;

        StartCoroutine(laser.GenerateLaser());

        yield return new WaitForSeconds(1f);

        Destroy(gameObject);

    }

    IEnumerator AlpahBlend(float duration)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        float remainDuration = duration;
        float alpha = 0f;

        while (remainDuration > 0f)
        {
            remainDuration -= Time.fixedDeltaTime;

            alpha += Time.fixedDeltaTime / duration;

            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            yield return new WaitForFixedUpdate();
        }

        remainDuration = duration;

        while (remainDuration > 0f)
        {
            remainDuration -= Time.fixedDeltaTime;

            alpha -= Time.fixedDeltaTime / duration;

            spriteRenderer.color = new Color(1f, 1f, 1f, alpha);

            yield return new WaitForFixedUpdate();
        }
    }
}
