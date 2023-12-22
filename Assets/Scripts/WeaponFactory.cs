using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFactory
{
    private DataBase weaponDB;

    public WeaponFactory(DataBase _weaponDB)
    {
        weaponDB = _weaponDB;
    }

    public GameObject CreateWeapon(string weaponName, Vector3 pos, Quaternion rot)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject obj = GameObject.Instantiate(prefab, pos, rot);
        WeaponData wdata = obj.AddComponent<WeaponData>();
        wdata.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].floatValue;
        return obj;
    }

    public GameObject CreateWeapon(string weaponName, Transform parent)
    {
        GameObject prefab = Resources.Load(weaponName) as GameObject;
        GameObject obj = GameObject.Instantiate(prefab);
        obj.transform.parent = parent;
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        WeaponData wdata = obj.AddComponent<WeaponData>();
        wdata.ATK = weaponDB.weaponDataBase[weaponName]["ATK"].floatValue;
        return obj;
    }
    
}
