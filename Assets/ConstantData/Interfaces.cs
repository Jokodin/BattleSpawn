using UnityEngine;

public interface IKillable
{
    void TakeDamage(PlayerObject source, float damage);
    void Die();
}

public interface IAttack
{
    void Attack(PlayerObject source, float damage);
}

public interface ISpawnBehavior
{
    void Spawn(GameObject unitToSpawn, Vector3 spawnLocation);
}
