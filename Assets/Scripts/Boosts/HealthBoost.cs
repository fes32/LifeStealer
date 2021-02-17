using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : Boost
{
    [SerializeField] private float _healthValue;

    public override void Action(Player player)
    {
        player.Healing(_healthValue);
    }
}