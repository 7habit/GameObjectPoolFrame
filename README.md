# GameObjectPoolFrame
Unity对象池框架
## 使用步骤
1. 引入框架using GameObjectPoolFrame;
2. 将需要纳入对象池的对象，创建预制体存储到Resources文件夹下
3. 从对象池拉取对象（若无会自动创建）：
  - `GameObject obj = GmaeObjectPool.Instance.GetGameObject("prefabs/Cube", Vector3.one, Quaternion.identity,
                CubeParent);`
  - 参数：预制体的路径（Resources文件夹下），初始位置，初始旋转，父对象transfrom
  - 返回：创建好的GameObject
4. 将对象放回对象池（会自动失活）
  - `GmaeObjectPool.Instance.ReleaseGameObject("prefabs/Cube", obj);`
  - 参数：拉取时的预制体路径，需要放回的GameObject对象
5. 若在从对象池取出/放回时存在其他初始化操作，请创建脚本-继承`PoolObjectInit`类，挂载到预制体上
  - OnSpawn()重写：初始化时操作放这里
  - OnDead()重写：回收时操作放这里
6. 若对象池使用完毕，可使用`GmaeObjectPool.Instance.DestoryObject("prefabs/Cube");`销毁对象池的对象
  - 参数：对象池对象的预制体地址（同生成）
## 整体效果
- 从对象池拉取时，会根据自动创建最大限度的可用对象
- 对象使用完毕后，可回收给对象池，以供下次使用
- 对象可在使用时或回收时特殊处理
- 对象池使用完毕后，可一键销毁对象池的对象

## 代码示例
```
using System;
using System.Collections;
using GameObjectPoolFrame;
using UnityEngine;

public class GameObjectPoolTest : MonoBehaviour
{
    private Transform CubeParent;
    [Header("对象销毁时间")]
    public float releaseTime;

    private void Awake()
    {
        CubeParent = GameObject.FindWithTag("Cube").transform;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            //创建对象
            GameObject obj = GmaeObjectPool.Instance.GetGameObject("prefabs/Cube", Vector3.one, Quaternion.identity,
                CubeParent);
            //几秒后释放资源
            StartCoroutine(StopObject(obj));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            //销魂对象池
            GmaeObjectPool.Instance.DestoryObject("prefabs/Cube");
        }
    }

    private IEnumerator StopObject(GameObject obj)
    {
        Debug.Log("开始销毁对象");
        //等待几秒
        yield return new WaitForSeconds(releaseTime);
        //释放资源
        GmaeObjectPool.Instance.ReleaseGameObject("prefabs/Cube", obj);
    }
}
```
![image](https://github.com/7habit/GameObjectPoolFrame/assets/16428251/c5587948-8b05-4447-a7f1-9fcdc908e97d)

