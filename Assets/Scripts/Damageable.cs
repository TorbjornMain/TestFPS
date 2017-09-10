using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;

public enum DamageType
{
    Slash,
    Pierce,
    Blunt,
    Fire,
    True
}

public class Damageable : NetworkBehaviour {

    public Dictionary<DamageType, float> susceptability;
    public FloatEvent OnDeath;
    public FloatEvent OnHurt;

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            if ((Application.isPlaying && isServer) || !Application.isPlaying)
            {
                _maxHealth = value;
                Health = Health;
            }
        }
    }

    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            if ((Application.isPlaying && isServer) || !Application.isPlaying)
            {
                _health = Mathf.Min(value, MaxHealth);

                if (_health < 0 && Application.isPlaying)
                {
                    if (OnDeath != null)
                    {
                        OnDeath.Invoke(_health.ToString());
                        RpcDeathEvent(_health);
                    }
                }
            }
        }
    }

    private float _health, _maxHealth;

    public void DealDamage(Dictionary<DamageType, float> damage)
    {
        if (!isServer)
            return;
        float totalDamage = 0;
        for(int i = 0; i <= (int)DamageType.True; i++)
        {
            totalDamage += damage[(DamageType)i] * susceptability[(DamageType)i];
        }
        if (totalDamage > 0 && OnHurt != null)
        {
            OnHurt.Invoke(totalDamage.ToString());
            RpcHurtEvent(totalDamage);
        }
        Health -= totalDamage;       
    }

    [ClientRpc]
    void RpcHurtEvent(float totalDamage)
    {
        OnHurt.Invoke(totalDamage.ToString());
    }

    [ClientRpc]
    void RpcDeathEvent(float curHealth)
    {
        OnDeath.Invoke(curHealth.ToString());
    }

}
