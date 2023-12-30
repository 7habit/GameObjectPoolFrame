using System.Collections.Generic;
using UnityEngine;

namespace GameObjectPoolFrame
{
    public class AssetsManager : Singleton<AssetsManager>
    {
        //私有构造
        private AssetsManager() 
        {
            assetsCache = new Dictionary<string, Object>();
        }

        //资源路径
        private Dictionary<string, Object> assetsCache;

        //获取资源
        //通过路径在Resources中Load资源，并存储在缓存
        public Object GetAssets(string path)
        {
            
            Object assets;
            if (!assetsCache.TryGetValue(path, out assets))
            {
                //缓存中没获取到,从Resources中Load
                assets = Resources.Load(path);
                //存储到缓存
                assetsCache.Add(path, assets);
            }
            //返回
            return assets;
        }
    }
}

