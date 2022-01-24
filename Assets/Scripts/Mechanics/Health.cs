using System;
using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    

    /// <summary>
    /// Represebts the current vital statistics of some game entity.
    /// </summary>
    public class Health : MonoBehaviour
    {
        /// <summary>
        /// The maximum hit points for the entity.
        /// </summary>
        public int maxHP = 1;
        public int collectiblePoints = 0;
        
        public int maxCollectiblePoints = 20;

        public float healthDecreaseFactorOverTime = 0.02f;
        //0.02f works
        float timer = 0;

        float totalHealth = 0.5f;

        float HEALTH_TIMER_MAX = 1f;
        /// <summary>
        /// Indicates if the entity should be considered 'alive'.
        /// </summary>
        public bool IsAlive => currentHP > 0;

        int currentHP;

        bool updateHealth = false;

        /// <summary>
        /// Increment the HP of the entity.
        /// </summary>
        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
        }

        public void Collected()
        {
            collectiblePoints += 1;

            // Increase total health by a fraction of the collectible count
            totalHealth += GetCollectibleFraction();
        }

        //TODO: delete
        public float GetCollectibleFraction()
        {
            return Mathf.Clamp((float)collectiblePoints / (float)maxCollectiblePoints, 0, 1f);
        }

        /// <summary>
        /// Decrement the HP of the entity. Will trigger a HealthIsZero event when
        /// current HP reaches 0.
        /// </summary>
        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            if (currentHP == 0)
            {
                var ev = Schedule<HealthIsZero>();
                ev.health = this;
            }
        }

        public void ReSpawned()
        {
            updateHealth = true;
            timer = 0;
        }
        /// <summary>
        /// Decrement the HP of the entitiy until HP reaches 0.
        /// </summary>
        public void Die()
        {
            while (currentHP > 0) Decrement();
            updateHealth = false;
        }

        void Awake()
        {
            currentHP = maxHP;
            updateHealth = true;
        }

        public float GetHealth()
        {
            return totalHealth;
        }

        public void UpdateHealth()
        {
            if (updateHealth)
            {
                timer += Time.deltaTime;
                if (timer >= HEALTH_TIMER_MAX)
                {
                    timer = 0f;

                    // Decrease health by a small amount every second
                    totalHealth -= healthDecreaseFactorOverTime;
                    totalHealth = Mathf.Clamp(totalHealth, 0, 1f);

                    if (totalHealth <= 0)
                    {
                        LevelManager levelManager = FindObjectOfType<LevelManager>();
                        levelManager.EndGame();
                    }
                }
            }
        }

        public void ReduceHealth(float amount)
        {
            // Reduce total health and keep it between 0 an 1
            totalHealth = Mathf.Clamp(totalHealth - amount, 0, 1f);
        }
    }
}
