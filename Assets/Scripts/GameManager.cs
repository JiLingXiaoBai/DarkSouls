using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    private DataBase weaponDB;
    private WeaponFactory weaponFact;

    void Awake()
    {
        CheckGameObject();
        CheckSingle();
    }

    void Start()
    {
        InitWeaponDB();
        InitWeaponFactory();

        weaponFact.CreateWeapon("Falchion", transform);
    }

    private void InitWeaponDB()
    {
        weaponDB = new DataBase();
    }

    private void InitWeaponFactory()
    {
        weaponFact = new WeaponFactory(weaponDB);
    }

    private void CheckGameObject()
    {
        if (tag == "GM")
        {
            return;
        }
        Destroy(this);
    }

    private void CheckSingle()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        Destroy(this);
    }
}