using System;
using System.Collections.Generic;

namespace CosmicCuration.Bullets
{
	public class BulletPool
	{
		private BulletView bulletView;
		private BulletScriptableObject bulletScriptableObject;
		private List<PooledBullet> poolBullets = new List<PooledBullet>();
		public BulletPool(BulletView _bulletView,BulletScriptableObject _bulletScriptableObject) 
		{
			this.bulletView = _bulletView;
			this.bulletScriptableObject = _bulletScriptableObject;
		}
		public class PooledBullet 
		{
			public BulletController Bullet;
			bool isUsed;
		}
	}
}
