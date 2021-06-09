using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    public float fireRate = 15f;
    private float nextTimeToFire;
    public float damage = 10f;

    private Animator zoomCameraAnimation;
    private bool zoomed;

    private Camera mainCamera;

    private GameObject crosshair;

    private bool isAiming;

    void Awake()
    {
        weaponManager = GetComponent<WeaponManager>();

        zoomCameraAnimation = transform.Find(Tags.LookRoot).transform.Find(Tags.ZoomCamera).GetComponent<Animator>();

        crosshair = GameObject.FindWithTag(Tags.Crosshair);

        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        WeaponShoot();
        ZoomInAndOut();
    }

    void WeaponShoot()
    {
        // If the selected weapon has weapon fire type "multiple"
        if (weaponManager.GetCurrentSelectedWeapon().fireType == WeaponFireType.MULTIPLE)
        {
            // If -> If we press and hold left mouse click and if time is greater than nextTimeToFire
            if (Input.GetMouseButton(0) && Time.time > nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;

                weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                BulletFired();
            }
        }
        // If the selected weapon has weapon fire type "normal"
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (weaponManager.GetCurrentSelectedWeapon().bulletType == WeaponBulletType.BULLET)
                {
                    weaponManager.GetCurrentSelectedWeapon().ShootAnimation();

                    BulletFired();
                }
            }
        }
    }

    void ZoomInAndOut()
    {
        // Aiming with the camera on the weapon
        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.AIM)
        {
            // If -> If we press and hold right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                zoomCameraAnimation.Play(AnimationTags.ZoomInAnimation);

                crosshair.SetActive(false);
            }

            // If -> If we release the right mouse button
            if (Input.GetMouseButtonUp(1))
            {
                zoomCameraAnimation.Play(AnimationTags.ZoomOutAnimation);

                crosshair.SetActive(true);
            }
        }

        if (weaponManager.GetCurrentSelectedWeapon().weaponAim == WeaponAim.SELF_AIM)
        {
            // If -> If we press and hold right mouse button
            if (Input.GetMouseButtonDown(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(true);

                isAiming = true;
            }

            // If -> If we release the right mouse button
            if (Input.GetMouseButtonUp(1))
            {
                weaponManager.GetCurrentSelectedWeapon().Aim(false);

                isAiming = false;
            }
        }
    }

    void BulletFired()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
        {
            if (hit.transform.tag == Tags.EnemyTag)
            {
                hit.transform.GetComponent<HealthScript>().ApplyDamage(damage);
            }
        }
    }
}
