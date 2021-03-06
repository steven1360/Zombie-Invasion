﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieFists : DamageSource
{
    [SerializeField] private float damage;
    private Animator anim;
    private bool attacking;
    private bool isTouchingDamageable;

    public bool ReadyForAttack { get; private set; }

    void Start()
    {
        anim = transform.root.GetComponent<Animator>();
        attacking = false;
        DamageValue = damage;
        isTouchingDamageable = false;
    }


    void OnTriggerStay2D(Collider2D col)
    {
        isTouchingDamageable = true;
        if (!attacking && transform.tag != col.transform.tag && col.transform.tag == "Player")
        {
            anim.SetTrigger("Attack");
            attacking = true;
            StartCoroutine(AttackAfterSeconds(GetAnimationClip("zombie_attack").length , col));
        }

    }

    void OnTriggerExit2D(Collider2D col)
    {
        isTouchingDamageable = false;
    }

    IEnumerator AttackAfterSeconds(float seconds, Collider2D col)
    {
        yield return new WaitForSeconds(seconds);
        if ( col != null)
        {
            Damageable damageable = col.GetComponent<Damageable>();
            if (damageable != null && isTouchingDamageable)
            {
                damageable.EnqueueDamageSource(this);
            }
            attacking = false;
        }

    }


    AnimationClip GetAnimationClip(string name)
    {
        AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }

        return null;
    }
}
