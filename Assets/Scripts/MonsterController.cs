using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IDamageAble, IDamageDealer<GameObject>
{
    [SerializeField]
    private float startHealth;
    [SerializeField]
    private float damageValue;
    [SerializeField]
    private float energyValue;
    [SerializeField, Range(0, 1)]
    private float shieldPower;
    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float attackRange=1f;
    [SerializeField]
    private float attackSpeed = 1f;
    [SerializeField]
    private float scoreForKill;
    [SerializeField]
    private bool endAfterDie;

    [SerializeField]
    private Rigidbody2D targetToAttack;
    [SerializeField]
    private GameObject deathEffect;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private GameObject bonusHandler;

    private GameHandler gameHandler;

    private Character _monsterInside;
    private Rigidbody2D _monsterRigidBody;
    private Vector2 _vectorToTarget;
    private float _attackCoolDownTimer;
    private SpriteRenderer _monsterSprite;
    private Animator _monsterAnimator;
    private string _lastAttacker;

    public void OnCreate(Rigidbody2D target, GameObject bonus)
    {
        _monsterRigidBody = GetComponent<Rigidbody2D>();
        _monsterSprite = GetComponent<SpriteRenderer>();
        _monsterAnimator = GetComponent<Animator>();
        bonusHandler = bonus;
        gameHandler = bonusHandler.GetComponent<GameHandler>();
        _monsterAnimator.speed = 2;
        targetToAttack = target;

        _monsterInside = new Character(startHealth, damageValue, energyValue, shieldPower, attackRange, attackSpeed, movementSpeed, tag);
        _vectorToTarget = targetToAttack.position - _monsterRigidBody.position;
        _attackCoolDownTimer = 0;
    }

    private void FlipSprite()
    {
        if (targetToAttack.position.x < this.transform.position.x)
        {
            _monsterSprite.flipX = true;
        }
        else if (targetToAttack.position.x > this.transform.position.x)
        {
            _monsterSprite.flipX = false;
        }
    }

    private void FixedUpdate()
    {

        var vectorToTarget = CalculateMovementVector();

        FlipSprite();

        if (_vectorToTarget.magnitude > _monsterInside.statsOut["range"].Value)
        {
            _monsterAnimator.SetFloat("Speed", _monsterInside.statsOut["movementSpeed"].Value);
            _monsterRigidBody.MovePosition(vectorToTarget);
        }
        else
        {
            _monsterAnimator.SetFloat("Speed", 0);
            Attack(targetToAttack.gameObject);
        }

    }

    private Vector2 CalculateMovementVector()
    {
        _vectorToTarget = targetToAttack.position - _monsterRigidBody.position;
        return _monsterRigidBody.position + _vectorToTarget.normalized * _monsterInside.statsOut["movementSpeed"].Value * Time.fixedDeltaTime;
    }

    public void Attack(GameObject target)
    {
        if (_attackCoolDownTimer <= 0)
        {
            // Запускаем анимацию атаки
            _monsterAnimator.SetTrigger("Attack");

            // Ищем цель и наносим урон
            if (bulletPrefab)
            {
                RangeAttack(target);
            }
            else
            {
                MeeleeAttack(target);
            }
            
            _attackCoolDownTimer = attackSpeed;
        }
        else
        {
            _attackCoolDownTimer -= Time.fixedDeltaTime;
        }

    }

    private void MeeleeAttack(GameObject target)
    {
        var targetType = target.GetComponent<IDamageAble>();
        if (targetType == null) return;
        targetType.TakeDamage(_monsterInside.statsOut["damage"].Value, tag);
    }

    private void RangeAttack(GameObject target)
    {
        Vector3 deltaDirection = (target.transform.position - transform.position).normalized;
        Debug.DrawLine(transform.position + (deltaDirection * 2), target.transform.position);
        Vector3 pointOfAttack = transform.position + deltaDirection;

        GameObject bullet = Instantiate(bulletPrefab, pointOfAttack, transform.rotation);
        Rigidbody2D bulletBody = bullet.GetComponent<Rigidbody2D>();
        Bullet bulletInside = bullet.GetComponent<Bullet>();
        bulletInside.damage = _monsterInside.statsOut["damage"].Value;
        bulletInside.ChooseAttacker(tag);
        bulletBody.AddForce(deltaDirection * _monsterInside.statsOut["attackSpeed"].Value * _monsterInside.statsOut["movementSpeed"].Value, ForceMode2D.Impulse);
    }

    public void TakeDamage(float damageAmount, string damageFrom)
    {
        _monsterAnimator.SetTrigger("Hitted");
        _monsterInside.TakeDamage(damageAmount, damageFrom);
        _lastAttacker = damageFrom;
        DiedByDamage();

    }

    public bool DiedByDamage()
    {
        if (_monsterInside.DiedByDamage())
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            SpriteRenderer effectSprite = effect.GetComponent<SpriteRenderer>();
            effectSprite.flipX = _monsterSprite.flipX;
            BonusController bonusScript = bonusHandler.GetComponent<BonusController>();
            // создание ошмётка
            bonusScript.PlayerScoresUp(scoreForKill, _lastAttacker);
            Destroy(effect, 3f);
            if (endAfterDie)
            {
                gameHandler.EndGame();
            }
            Destroy(gameObject);
        }
        return true;
    }
}
