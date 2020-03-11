using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            ApplyEffect();
        }

        if (collision.tag == "Paddle" || collision.tag == "DeadWall")
        {
            Destroy(gameObject);
        }
    }

    protected abstract void ApplyEffect();
}
