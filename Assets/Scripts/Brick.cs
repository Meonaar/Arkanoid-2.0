using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Brick : MonoBehaviour
{
    public int hitPoints = 1;
    public ParticleSystem destroyEffect;
    private SpriteRenderer sr;
    private BoxCollider2D bc;

    public static event Action<Brick> OnBrickDestruction;

    private void Awake()
    {
        this.sr = this.GetComponent<SpriteRenderer>();
        Ball.OnLightningBallEnable += OnLightningBallEnable;
        Ball.OnLightningBallDisable += OnLightningBallDisable;
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnLightningBallEnable(Ball obj)
    {
        if (this != null)
        {
            bc.isTrigger = true;
        }
    }

    private void OnLightningBallDisable(Ball obj)
    {
        if (this != null)
        {
            bc.isTrigger = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        CollisionLogic(ball);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        CollisionLogic(ball);
    }

    private void CollisionLogic(Ball ball)
    {
        this.hitPoints--;
        if (this.hitPoints <= 0 || (ball != null && ball.isLightningBall))
        {
            BrickManager.Instance.RemainingBricks.Remove(this);
            OnBrickDestruction?.Invoke(this);
            OnBrickDestroy();
            SpawnDestroyEffect();
            Destroy(this.gameObject);
        }
        else
        {
            sr.sprite = BrickManager.Instance.Sprites[this.hitPoints - 1];
        }
    }

    private void OnBrickDestroy()
    {
        float buffSpawnChance = UnityEngine.Random.Range(0, 100f);
        float deBuffSpawnChance = UnityEngine.Random.Range(0, 100f);
        bool alreadySpawned = false;
        
        if (buffSpawnChance <= CollectableManager.Instance.buffChance)
        {
            alreadySpawned = true;
            Collectable newBuff = SpawnCollectable(true);
        }

        if (deBuffSpawnChance <= CollectableManager.Instance.deBuffChance && !alreadySpawned)
        {
            Collectable newDebuff = SpawnCollectable(true);
        }
    }

    private Collectable SpawnCollectable(bool isBuff)
    {
        List<Collectable> collection;

        if (isBuff)
        {
            collection = CollectableManager.Instance.Buffs;
        }
        else
        {
            collection = CollectableManager.Instance.Debuffs;
        }

        int buffIndex = UnityEngine.Random.Range(0, collection.Count);
        Collectable prefab = collection[buffIndex];
        Collectable newCollectable = Instantiate(prefab, transform.position, Quaternion.identity);

        return newCollectable;
    }

    private void SpawnDestroyEffect()
    {
        Vector3 brickPos = gameObject.transform.position;
        Vector3 spawnEffectPos = new Vector3(brickPos.x, brickPos.y, brickPos.z - 0.2f);
        GameObject effect = Instantiate(destroyEffect.gameObject, spawnEffectPos, Quaternion.identity);

        MainModule mm = effect.GetComponent<ParticleSystem>().main;
        mm.startColor = this.sr.color;
        Destroy(effect, destroyEffect.main.startLifetime.constant);
    }

    public void Init(Transform containerTransform, Sprite sprite, Color color, int _hitPoints)

    {
        transform.SetParent(containerTransform);
        sr.sprite = sprite;
        sr.color = color;
        hitPoints = _hitPoints;
    }

    private void OnDisable()
    {
        Ball.OnLightningBallEnable -= OnLightningBallEnable;
        Ball.OnLightningBallDisable -= OnLightningBallDisable;
    }
}
