using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim
{
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponFireType
{
    SINGLE,
    MULTIPLE
}

public enum WeaponBulletType
{
    NONE,
    ARROW,
    BULLET
}

public class WeaponHandler : MonoBehaviour
{
    private Animator anim;

    public WeaponAim weaponAim;

    [SerializeField] private GameObject muzzleFlash;

    [SerializeField] private AudioSource shootSound, reloadSound;

    public WeaponFireType fireType;

    public WeaponBulletType bulletType;

    public GameObject attackPoint;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ShootAnimation()
    {
        // Calling animator weapon trigger
        anim.SetTrigger(AnimationTags.ShootTrigger);
    }

    public void Aim(bool canAim)
    {
        anim.SetBool(AnimationTags.AimParameter, canAim);
    }

    void TurnOnMuzzleFlash()
    {
        muzzleFlash.SetActive(true);
    }
    void TurnOffMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    void PlayShootSound()
    {
        shootSound.Play();
    }

    void PlayReloadSound()
    {
        reloadSound.Play();
    }

    void TurnOnAttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void TurnOffAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
