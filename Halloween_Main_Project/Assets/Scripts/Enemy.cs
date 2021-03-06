﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject {
    public int playerDamage;
   // public Animator playerAnimator;

    private Animator animator;
    private Transform target;
    private bool skipMove;

    public AudioClip enemyAttack1;
    public AudioClip enemyAttack2;

	// Use this for initialization
	protected override void Start () {
        GameManager.instance.AddEnemyToList(this);
        animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        base.Start();
	}

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        if (skipMove)
        {
            skipMove = false;
            return;
        }
        base.AttemptMove<T>(xDir, yDir);
        skipMove = true;
    }


    //setup Enemy's movement
    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;
        //if they are in the same column
        if(Mathf.Abs(target.position.x - transform.position.x) < float.Epsilon)
        {
            yDir = target.position.y > this.transform.position.y ? 1 : -1;
        }
        else
        {
            xDir = target.position.x > this.transform.position.x ? 1 : -1;
        }
        AttemptMove<Player>(xDir, yDir);
        //playerAnimator.SetFloat("horizontal", 0.0f);
        //playerAnimator.SetFloat("vertical", 0.0f);
    }

    protected override void OnCantMove<T>(T component)
    {
        Player hitPlayer = component as Player;
        hitPlayer.LoseFood(playerDamage);
        animator.SetTrigger("enemyAttack");
        SoundManager.instance.RandomizeSfx(enemyAttack1, enemyAttack2);
    }
}
