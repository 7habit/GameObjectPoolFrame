using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GameObjectPoolFrame
{
    public class Singleton<T> where T:class
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    //ͨ�����䴴��˽�й���Ķ���
                    _instance = Activator.CreateInstance(typeof(T), true) as T;
                }
                return _instance;
            }
        }

    }
}

