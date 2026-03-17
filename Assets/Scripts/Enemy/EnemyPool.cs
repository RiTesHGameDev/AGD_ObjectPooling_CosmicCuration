using System.Collections.Generic;

namespace CosmicCuration.Enemy
{
    public class EnemyPool
    {
        private EnemyView enemyPrefab;
        private EnemyData enemyData;
        private List<PooledEnemy> pooledEnemies = new List<PooledEnemy>();
        public EnemyPool(EnemyView _enemyPrefab,EnemyData _enemyData) 
        {
            this.enemyPrefab = _enemyPrefab;
            this.enemyData = _enemyData;
        }

        public EnemyController GetEnemy()
        {
            if(pooledEnemies.Count > 0)
            {
                PooledEnemy enemy = pooledEnemies.Find(item => !item.isUsed);
                if(enemy != null)
                {
                    enemy.isUsed = true;
                    return enemy.enemy;
                }
            }
            return CreateNewPooledEnemy();
        }

        public void ReturnToEnemyPool(EnemyController returnEnemy)
        {
            PooledEnemy pooledEnemy = pooledEnemies.Find(item => item.enemy.Equals(returnEnemy));
            pooledEnemy.isUsed = false;
        }

        public EnemyController CreateNewPooledEnemy()
        {
            PooledEnemy pooledEnemy = new PooledEnemy();
            pooledEnemy.enemy = new EnemyController(enemyPrefab,enemyData);
            pooledEnemy.isUsed = true;
            pooledEnemies.Add(pooledEnemy);

            return pooledEnemy.enemy ;
        }
        public class PooledEnemy
        {
            public EnemyController enemy;
            public bool isUsed;
        }
    }
}
