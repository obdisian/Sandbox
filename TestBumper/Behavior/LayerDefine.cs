using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wavy
{
    /// <summary>
    /// ここの定義を追加または変更した場合はエンジンにも反映させる
    /// </summary>
	public static class LayerDefine
	{
        public enum Layer
        {
            Default,
            TransparentFX,
            IgnoreRaycast,
            Dummy_3,
            Water,
            UI,
            Dummy_6,
            Dummy_7,
            PostProcessing,

            // ユーザー定義
            Ground,
        }

        public static int GetLayerIndex(Layer layer)
        {
            return (int)layer;
        }
        public static int ToInt(this Layer layer)
        {
            return (int)layer;
        }
        public static int GetBits(params Layer[] layers)
        {
            int bits = 0;
            for (int i = 0; i < layers.Length; i++)
            {
                bits |= 1 << (int)layers[i];
            }
            return bits;
        }
    }
}
