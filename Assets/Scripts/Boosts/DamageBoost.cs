using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoost : Boost
{
    [SerializeField] private float _damageUp;

    public override void Action(Player player)
    {
        player.UpDamage(_damageUp);
    }
}
