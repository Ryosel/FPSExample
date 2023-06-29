﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용자가 마우스 왼쪽 버튼을 누르면
// 총알공장에서 총알을 만들고
// 그 총알을 총구위치에 배치하고싶다.
public class PlayerFire : MonoBehaviour
{
    enum BImpactName
    {
        Floor,
        Enemy
    }

    public GameObject bulletFactory;
    public Transform firePosition;
    // 총알자국공장
    public GameObject[] bImpactFactorys;
    void Start()
    {
    }

    void Update()
    {
        UpdateFire();
        // UpdageGrenade();
    }

    private void UpdateFire()
    {
        // 0. 사용자가 마우스 왼쪽 버튼을 누르면
        if (Input.GetButtonDown("Fire1"))
        {
            // 1. 카메라에서 카메라의 앞방향으로 시선을 만들고
            Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

            int layer = (1 << LayerMask.NameToLayer("Default"));
            RaycastHit hitInfo;
            // 2. 바라보고싶다.
            if (Physics.Raycast(ray, out hitInfo, float.MaxValue, layer))
            {
                // 3. 시선이 닿은 곳에 총알자국공장에서 총알자국을 만들어서 배치하고싶다.
                GameObject bulletImpact = Instantiate(bImpactFactorys[(int)BImpactName.Floor]);
                bulletImpact.transform.position = hitInfo.point;
                // 방향을 회전하고싶다. 튀는방향(forward을 부딪힌 면의 Normal방향으로
                bulletImpact.transform.forward = hitInfo.normal;
            }
            else
            {
                // 허공
            }
        }
    }

    private void UpdageGrenade()
    {
        // 만약 사용자가 G키를 누르면 폭탄을 던지고 싶다.
        if (Input.GetKeyDown(KeyCode.G))
        {
            // 총알공장에서 총알을 만들고
            GameObject bullet = Instantiate(bulletFactory);
            // 그 총알을 총구위치에 배치하고싶다.
            bullet.transform.position = firePosition.position;
            bullet.transform.forward = firePosition.forward;
            // 총알의 speed를 바꾸고싶다.
            Bullet bulletComp = bullet.GetComponent<Bullet>();
            bulletComp.speed = 20;
        }
    }


}
