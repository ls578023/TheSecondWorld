using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{



    /// <summary>
    /// 对象池词典
    /// </summary>
    private Dictionary<string, List<GameObject>> pool;
    /// <summary>
    /// 预制体词典
    /// </summary>
    private Dictionary<string, GameObject> prefabs;

    private GameObjectPool()
    {
        pool = new Dictionary<string, List<GameObject>>();
        prefabs = new Dictionary<string, GameObject>();
    }

    private static GameObjectPool instance;

    public static GameObjectPool GetInstance()//外部入口
    {
        if (instance == null)
        {
            instance = new GameObjectPool();
        }
        return instance;

    }





    /// <summary>
    /// 从对象池中获取对象
    /// </summary>
    /// <param name="objName">试图获取的对象名</param>
    /// <returns>返回对象</returns>
    public GameObject GetObj(string objName, Vector3 objweizhi,string rootPath)
    {
        GameObject result = null;//最终返回的结果
        GameObject prefab = null;//预制体加载

        //情况1: 池中已存在objName且池中存在待使用的GameObject

        if (pool.ContainsKey(objName))
        {
            if (pool[objName].Count > 0)//如果池中存在GameObject
            {
                result = pool[objName][0];//取出GameObject
                pool[objName].Remove(result);//删除池中的记录
                result.SetActive(true);//激活GameObject
                result.transform.position = objweizhi;
                return result;//返回结果
            }
        }

        //情况2: 池中不存在objName或池中不存在待使用的GameObject
        if (prefabs.ContainsKey(objName))//已加载过对应的预制体
        {
            prefab = prefabs[objName];
        }
        else                             //未加载过该预设体
        {
            //加载预设体
            prefab = Resources.Load<GameObject>(rootPath + objName);
            //更新字典
            prefabs.Add(objName, prefab);
        }

        result = Object.Instantiate(prefab, objweizhi, Quaternion.identity) as GameObject;//生成新的预制体克隆
        result.name = objName;//改名（去除 Clone）
        return result;//返回
    }





    /// <summary>
    /// 回收对象到对象池，在物体身上设置自动回收到对象池
    /// </summary>
    /// <param name="objName"></param>
    public void RecycleObj(GameObject obj)
    {
        //设置为非激活
        obj.SetActive(false);
        //判断是否有该对象的对象池
        if (pool.ContainsKey(obj.name))
        {
            //放置到该对象池
            pool[obj.name].Add(obj);
        }
        else
        {
            //创建该类型的池子，并将对象放入
            pool.Add(obj.name, new List<GameObject>() { obj });

        }
    }




}