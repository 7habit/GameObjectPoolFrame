using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;
using Quaternion = UnityEngine.Quaternion;

namespace GameObjectPoolFrame
{
    public class GmaeObjectPool : Singleton<GmaeObjectPool>
    {
        //字典存储对象池
        private Dictionary<string, List<GameObject>> _poolDic;
        
        //私有构造
        private GmaeObjectPool()
        {
            _poolDic = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 通过资源路径获取对象
        /// </summary>
        /// <param name="path">资源路径</param>
        /// <param name="pos">初始位置</param>
        /// <param name="rotation">初始Quaternion旋转</param>
        /// <param name="parent">父对象</param>
        /// <returns></returns>
        public GameObject GetGameObject(string path, Vector3 pos, Quaternion rotation, Transform parent)
        {
            GameObject obj;
            //没有这个资源的池子 或 有池子没对象
            if (!_poolDic.ContainsKey(path) || _poolDic[path].Count == 0)
            {
                //拿到预设体
                Object prefab = AssetsManager.Instance.GetAssets(path);
                if (prefab == null)
                {
                    Debug.LogError("资源不存在，无法创建对象池");
                }
                //把预设体给对象
                obj = Object.Instantiate(prefab) as GameObject;
                obj.name = prefab.name;
            }
            else
            {
                //取出对象
                obj = _poolDic[path][0];
                //对象移出池子
                _poolDic[path].RemoveAt(0);
                //激活对象
                obj.SetActive(true);
            }

            //初始化对象
            obj.transform.position = pos;
            obj.transform.rotation = rotation;
            obj.transform.parent = parent;

            return obj;
        }

        /// <summary>
        /// 回收对象池对象
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        public void ReleaseGameObject(string path, GameObject obj)
        {
            //失活
            obj.SetActive(false);
            //如果存在池子
            if (_poolDic.ContainsKey(path))
            {
                //放入池子
                _poolDic[path].Add(obj);
            }
            else
            {
                //创建池子并放入
                _poolDic.Add(path, new List<GameObject>(){obj});
            }
        }

        /// <summary>
        /// 销毁对象池
        /// </summary>
        /// <param name="path">对象的预制体路径</param>
        public void DestoryObject(string path)
        {
            //如果存在此对象池
            if (_poolDic.ContainsKey(path))
            {
                for (int i = 0; i < _poolDic[path].Count; i++)
                {
                    //销毁对象
                    Object.Destroy(_poolDic[path][i]);
                }
                //清空列表
                _poolDic[path].Clear();
            }
            else
            {
                Debug.LogWarning("对象池不存在，请检查");
            }
        }
        
        
        
    }
}