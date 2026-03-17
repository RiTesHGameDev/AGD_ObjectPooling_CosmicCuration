using System;
using System.Collections.Generic;
using UnityEngine.Analytics;

namespace CosmicCuration.Bullets
{
	public class BulletPool
	{
		private BulletView bulletView;
		private BulletScriptableObject bulletScriptableObject;
		private List<PooledBullet> pooledBullets = new List<PooledBullet>();
		public BulletPool(BulletView _bulletView,BulletScriptableObject _bulletScriptableObject) 
		{
			this.bulletView = _bulletView;
			this.bulletScriptableObject = _bulletScriptableObject;
		}

		public BulletController GetBullet()
		{
			if(pooledBullets.Count > 0)
			{
				PooledBullet pooledBullet = pooledBullets.Find(item => !item.isUsed);
				if (pooledBullet != null)
				{
					pooledBullet.isUsed = true;
					return pooledBullet.bullet;
				}
			}
			return CreateNewPulledBullet();
		} 

		public void ReturnToBulletPool(BulletController returnBullet)
		{
			PooledBullet pooledBullet = pooledBullets.Find(item => item.bullet.Equals(returnBullet));
			pooledBullet.isUsed = false;
		}

		private BulletController CreateNewPulledBullet()
		{
			PooledBullet pooledBullet = new PooledBullet();
			pooledBullet.bullet = new BulletController(bulletView,bulletScriptableObject);
			pooledBullet.isUsed = true;
			pooledBullets.Add(pooledBullet);

			return pooledBullet.bullet;
		}
		public class PooledBullet 
		{
			public BulletController bullet;
			public bool isUsed;
		}
	}
}
