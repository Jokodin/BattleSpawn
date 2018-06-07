using UnityEngine;
using ConstantData;

public abstract class PlayerObject : WorldObject, IKillable{

    public float maxHealth;
    public float currentHealth;
    public string team;

    new protected virtual void Start()
    {
        base.Start();

        //Debug.Log("PlayerObject start");

        maxHealth = 1;
        currentHealth = maxHealth;
    }

    public virtual void TakeDamage(PlayerObject source, float damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        manager.removeFromTeam(GetComponent<PlayerObject>());

        Destroy(gameObject);
    }

}
