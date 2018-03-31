using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 네개 모서리에 발사하는 패턴
public class Pattern_Boss1_3 : PatternSequencer
{
    public Boss_1 owner;

    public Bullet bulletPrefab;

    protected override void Awake()
    {
        owner = GetComponent<Boss_1>();

        if (owner == null)
            Destroy(this);

        for (int i = 0; i < 4; ++i)
        {
            switch (i)
            {
                case 0:
//                    owner.parts[i].transform.position = 
                    break;
                case 1:

                    break;
                case 2:

                    break;
                case 3:

                    break;
            }
        }
    }

    protected override IEnumerator PatternFramework()
    {


        return base.PatternFramework();


    }
}
