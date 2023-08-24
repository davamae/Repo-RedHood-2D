using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    Animator animator;

    [SerializeField]
    private float _maxHealth = 100;

    public float MaxHealth {
        get {
            return _maxHealth;
        }
        set {
            _maxHealth = value;
        }
    }

    [SerializeField]
    private float _health = 100;

    public float Health {
        get {
            return _health;
        }
        set {
            _health = value;

            // if health drops below 0, character dies
            if (_health <= 0) {
                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive {
        get {
            return _isAlive;
        }
        set {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
        }
    }

    private void Awake() {
        animator = GetComponent<Animator>();
    }


}
