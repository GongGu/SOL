using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Unit {

    [System.NonSerialized]//public이어도 유니티 에디터에서 숨길 수 있음
    public float direction;

    public float bulletSpeed; // 총알스피드
        
	// Update is called once per frame
	protected virtual void FixedUpdate () {

        transform.position = (Vector2)transform.position + MyMath.DirectionToVector2(direction) * Time.fixedDeltaTime * bulletSpeed;

        // 총알의 포지션을 Vector2로 강제 형변환한 값에 이동량을 더해줘서 실제 이동시키기

    }

    protected IEnumerator DelayDestroy()
    {

        yield return new WaitForSeconds(5f); 
        //코루틴은 yield return 일단 빠져나온 후 뒤에 WaitForSeconds정해진 시간만큼 기다린 후에 오브젝트 제거 

        Destroy(gameObject);
    }


}
