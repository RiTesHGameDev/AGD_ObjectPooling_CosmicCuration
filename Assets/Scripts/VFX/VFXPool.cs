using CosmicCuration.Utilities;

namespace CosmicCuration.VFX
{
	public class VFXPool : GenericObjectPool<VFXController>
	{
		private VFXView prefab;

		public VFXPool(VFXView prefab) => this.prefab = prefab;

		public VFXController GetVFX() => GetItem<VFXController>();

        protected override VFXController CreateItem<T>() => new VFXController(prefab);
	}
}
