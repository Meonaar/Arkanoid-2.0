using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    #region Singleton

    private static CollectableManager _instance;
    public static CollectableManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public List<Collectable> Buffs;
    public List<Collectable> Debuffs;

    [Range(0,100)]
    public float buffChance;
    [Range(0, 100)]
    public float deBuffChance;
}
