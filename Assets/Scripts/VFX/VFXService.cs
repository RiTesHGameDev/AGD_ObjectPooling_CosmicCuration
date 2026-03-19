using System.Collections.Generic;
using UnityEngine;

namespace CosmicCuration.VFX
{
    public class VFXService
    {
        private VFXPool vfxPool;

        public VFXService(VFXView vfxPrefab) => vfxPool =  new VFXPool(vfxPrefab);

        public void PlayVFXAtPosition(VFXType type, Vector2 spawnPosition)
        {
            VFXController vfxToPlay = vfxPool.GetVFX();
            vfxToPlay.Configure(type,spawnPosition);
        }

        public void ReturnToVFXPool(VFXController vfxToReturn) => vfxPool.ReturnItem(vfxToReturn);
    } 
}