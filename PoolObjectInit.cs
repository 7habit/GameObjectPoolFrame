using System;
using UnityEngine;

namespace GameObjectPoolFrame
{
    public class PoolObjectInit : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            //对象池提供资源时激活自动调用
            OnSpawn();
        }

        protected virtual void OnDisable()
        {
            //对象池收回资源时失活自动调用
            OnDead();
        }

        /// <summary>
        /// 出生时初始化函数
        /// </summary>
        public virtual void OnSpawn()
        {
        }

        /// <summary>
        /// 死亡时执行的函数
        /// </summary>
        public virtual void OnDead()
        {
        }
    }
}