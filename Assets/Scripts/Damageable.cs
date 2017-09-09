using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum DamageType
{
    Slash,
    Pierce,
    Blunt,
    Fire,
    True
}

public class Damageable : MonoBehaviour {

    public Dictionary<DamageType, float> susceptability;
    public UnityEvent OnDeath;

    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
            Health = Health;
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
            _health = Mathf.Min(value, MaxHealth);

            if (_health < 0 && Application.isPlaying)
            {
                if (OnDeath != null)
                    OnDeath.Invoke();
            }
        }
    }

    private float _health, _maxHealth;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
