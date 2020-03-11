using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Ball : MonoBehaviour
{
    public static event Action<Ball> OnBallDeath;
    public static event Action<Ball> OnLightningBallEnable;
    public static event Action<Ball> OnLightningBallDisable;
    private SpriteRenderer sr;
    public ParticleSystem lightningEffect;

    public bool isLightningBall;
    private float duration = 10f;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
    }
    public void StartLightningBall()
    {
        if (!isLightningBall)
        {
            isLightningBall = true;
            sr.enabled = false;
            lightningEffect.gameObject.SetActive(true);
            StartCoroutine(StopLightningBallAfterTime(duration));
            OnLightningBallEnable?.Invoke(this);
        }
    }

    private IEnumerator StopLightningBallAfterTime(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StopLighningBall();
    }

    private void StopLighningBall()
    {
        if (isLightningBall)
        {
            isLightningBall = false;
            sr.enabled = true;
            lightningEffect.gameObject.SetActive(false);
            OnLightningBallDisable?.Invoke(this);
        }
    }

    public void Die()
    {
        OnBallDeath?.Invoke(this);
        Destroy(gameObject,1);
        
    }
}
