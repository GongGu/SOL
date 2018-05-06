using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorySecenEvent : MonoBehaviour
{
    public bool isPanel;

    public List<GameObject> planeList = new List<GameObject>();
    public List<GameObject> defenceModuleLIst = new List<GameObject>();
    public List<GameObject> subWeaponList = new List<GameObject>();

    private void Awake()
    {
        if(isPanel == true)
        {
            ArmorySecenEvent[] events = FindObjectsOfType<ArmorySecenEvent>();

            foreach(var e in events)
            {
                e.planeList = planeList;
                e.subWeaponList = subWeaponList;
                e.defenceModuleLIst = defenceModuleLIst;
            }
        }

        UpdateImage();
    }

    public enum EquipmentType
    {
        PlayerPlane,
        SubWeapon,
        DefenceModule,
    }

    public EquipmentType type;

    //    public GameSetting.PlayerPlane

    public void UpdateEquipment(EquipmentType _type, int idx)
    {
        switch (_type)
        {
            case EquipmentType.PlayerPlane:
                GameSetting.gameSetting.playerPlane = (GameSetting.PlayerPlane)idx;
                break;
            case EquipmentType.SubWeapon:
                GameSetting.gameSetting.subWeapon = (GameSetting.SubWeapon)idx;
                break;
            case EquipmentType.DefenceModule:
                GameSetting.gameSetting.defenceModule = (GameSetting.DefenceModule)idx;
                break;
        }

        UpdateImage();
    }

    public void UpdateImage()
    {
        int idx = 0;

        foreach(var _type in (EquipmentType[])System.Enum.GetValues(typeof(EquipmentType)))
        {
            if(_type == EquipmentType.PlayerPlane)
            {
                idx = (int)GameSetting.gameSetting.playerPlane;
                for (int i = 0; i < planeList.Count; ++i)
                {
                    if (i != idx)
                        planeList[i].GetComponent<Button>().interactable = true;
                    else
                        planeList[i].GetComponent<Button>().interactable = false;
                }
            }
            else if(_type == EquipmentType.SubWeapon)
            {
                idx = (int)GameSetting.gameSetting.subWeapon;
                for (int i = 0; i < subWeaponList.Count; ++i)
                {
                    if (i != idx)
                        subWeaponList[i].GetComponent<Button>().interactable = true;
                    else
                        subWeaponList[i].GetComponent<Button>().interactable = false;
                }
            }
            else if(_type == EquipmentType.DefenceModule)
            {
                idx = (int)GameSetting.gameSetting.defenceModule;
                for (int i = 0; i < defenceModuleLIst.Count; ++i)
                {
                    if (i != idx)
                        defenceModuleLIst[i].GetComponent<Button>().interactable = true;
                    else
                        defenceModuleLIst[i].GetComponent<Button>().interactable = false;
                }
            }
        }
    }

    public void OnClickPlayerChangeA()
    {
        UpdateEquipment(EquipmentType.PlayerPlane, 0);
    }

    public void OnClickPlayerChangeB()
    {
        UpdateEquipment(EquipmentType.PlayerPlane, 1);
    }


    public void OnClickSubWeaponChangeA()
    {
        UpdateEquipment(EquipmentType.SubWeapon, 0);
    }
    public void OnClickSubWeaponChangeB()
    {
        UpdateEquipment(EquipmentType.SubWeapon, 1);
    }


    public void OnClickDefenceChangeA()
    {
        UpdateEquipment(EquipmentType.DefenceModule, 0);
    }
    public void OnClickDefenceChangeB()
    {
        UpdateEquipment(EquipmentType.DefenceModule, 1);
    }


    public void OnClickToMain()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }
}
