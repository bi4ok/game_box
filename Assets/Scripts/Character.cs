using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : IDamageAble, IDamageDealer<Character>
{
    private PlayerStat _currentHealthStat;
    private PlayerStat _currentEnergyStat;
    private PlayerStat _currentEnergyPowerStat;
    private PlayerStat _currentDamageStat;
    private PlayerStat _currentAttackRangeStat;
    private PlayerStat _currentAttackSpeedStat;
    private PlayerStat _currentMovementSpeedStat;

    private float _currentHealth;
    private float _currentEnergy;
    private float _currentDamage;
    private float _currentEneryRegen;
    private float _currentEnergyPower;
    private float _currentHealtRegen;

    public float health { get { return _currentHealth; } }
    public float energy { get { return _currentEnergy; } }
    public float damage { get { return _currentDamage; } }

    private string _name;

    public string stateOut { get { return _stateIn; } }
    public Dictionary<string, PlayerStat> statsOut { get; }

    private string _stateIn="alive";

    public Character(
        float healthValue, float damageValue, 
        float energyValue, float shieldPower,
        float attackRange, float attackSpeed,
        float movementSpeed, string name)
    {
        //stats
        _currentHealthStat = new PlayerStat(healthValue, "health");
        _currentEnergyStat = new PlayerStat(energyValue, "energy");
        _currentEnergyPowerStat = new PlayerStat(shieldPower, "energyPower");
        _currentDamageStat = new PlayerStat(damageValue, "damage");
        _currentAttackRangeStat = new PlayerStat(attackRange, "range");
        _currentAttackSpeedStat = new PlayerStat(attackSpeed, "attackSpeed");
        _currentMovementSpeedStat = new PlayerStat(movementSpeed, "movementSpeed");
        statsOut = new Dictionary<string, PlayerStat>() {
            {"health", _currentHealthStat},
            {"energy", _currentEnergyStat},
            {"energyPower", _currentEnergyPowerStat},
            {"damage", _currentDamageStat},
            {"range", _currentAttackRangeStat},
            {"attackSpeed", _currentAttackSpeedStat},
            {"movementSpeed", _currentMovementSpeedStat}
        };

        //values
        _currentHealth = healthValue;
        _currentEnergy = energyValue;
        _currentDamage = damageValue;
        _currentEneryRegen = energyValue / 10;
        _currentHealtRegen = healthValue / 10;
        _currentEnergyPower = shieldPower;
        _name = name;

    }

    public void Attack(Character target)
    {
        target.TakeDamage(_currentDamage, _name);
    }

    public void TakeDamage(float damageAmount, string damageFrom)
    {
        _currentHealth -= damageAmount;
        _currentHealth = _currentHealth > 0 ? _currentHealth : 0;
        Debug.Log("Alive? " + DiedByDamage());
    }

    public void ChangingStatsInTime()
    {
        Debug.Log("Refresh ENERGY from " + _currentEnergy);
        if (_currentEnergy < _currentEnergyStat.Value)
        {
            float lackOfEnergy = _currentEnergyStat.Value - _currentEnergy;
            if (lackOfEnergy >= _currentEneryRegen)
            {
                _currentEnergy += _currentEneryRegen;
            }
            else
            {
                _currentEnergy = _currentEnergyStat.Value;
            }
            Debug.Log("to " + _currentEnergy);
        }


        if (_currentHealth > _currentHealthStat.Value)
        {
            float lackOfHealth = _currentHealth - _currentHealthStat.Value;
            _currentHealth -= _currentHealtRegen > lackOfHealth ? lackOfHealth : _currentHealtRegen;
        }

        Debug.Log("HP: " + _currentHealth + " of " + _currentHealthStat.Value + " SHIELD: " + _currentEnergy + " of " + _currentEnergyStat.Value);

    }

    public void TakeHeal(float healAmount)
    {
        _currentHealth += healAmount;
    }

    public bool DiedByDamage()
    {
        _stateIn = "dead";
        return _currentHealth <= 0;
    }

    public float ShieldThatDamage(float damageAmount)
    {
        
        float shieldPower = Mathf.Min(_currentEnergyPower, 1);
        Debug.Log($"energy power: {_currentEnergyPower}, enegry value: {_currentEnergy}, Incoming damage: {damageAmount}");
        float partDamageAmount = damageAmount * shieldPower;
        float neededEnergyToShield = partDamageAmount * (1 - shieldPower);
        if (_currentEnergy >= neededEnergyToShield)
        {
            _currentEnergy -= neededEnergyToShield;
            partDamageAmount = 0;
        }
        else if (_currentEnergy > 0)
        {
            _currentEnergy = 0;
            partDamageAmount -= partDamageAmount * _currentEnergy/neededEnergyToShield;
        }
        Debug.Log($"outcoming damage {(damageAmount * (1 - shieldPower))} + part damage after shield {partDamageAmount} = {(damageAmount * (1 - shieldPower)) + partDamageAmount}");
        return (damageAmount * (1 - shieldPower)) + partDamageAmount;
    }
}
