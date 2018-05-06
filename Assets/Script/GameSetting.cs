using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    public static GameSetting gameSetting;

    private void Awake()
    {
        if (gameSetting == null)
            gameSetting = this;

        DontDestroyOnLoad(gameSetting);
    }

    public enum PlayerPlane
    {
        A,
        B,
    }

    public PlayerPlane playerPlane;

    public enum SubWeapon
    {
        A,
        B,
    }

    public SubWeapon subWeapon;

    public enum DefenceModule
    {
        A,
        B,
    }

    public DefenceModule defenceModule;

}
