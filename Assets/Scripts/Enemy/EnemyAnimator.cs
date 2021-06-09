using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Walk(bool walk)
    {
        anim.SetBool(AnimationTags.WalkParameter, walk);
    }

    public void Run(bool run)
    {
        anim.SetBool(AnimationTags.RunParameter, run);
    }

    public void Attack()
    {
        anim.SetTrigger(AnimationTags.AttackTrigger);
    }

    public void Dead()
    {
        anim.SetTrigger(AnimationTags.DeadTrigger);
    }
}
