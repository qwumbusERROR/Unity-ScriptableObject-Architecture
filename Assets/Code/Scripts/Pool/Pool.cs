using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private T _prefab;
    private List<T> _objects;
    
    public Pool(T prefab, int prewarmObject, bool startInAwake = false)
    {
        _prefab = prefab;
        _objects = new List<T>();
        
        for(int objectCount = 0; objectCount < prewarmObject; objectCount ++)
        {
            var newObject = GameObject.Instantiate(_prefab);
            newObject.gameObject.SetActive(startInAwake);
            
            _objects.Add(newObject);
        }
    }
    
    public T GetObject(Vector3 position, Quaternion rotation)
    {
        var objectPool = _objects.FirstOrDefault(x => !x.isActiveAndEnabled);
        
        if(objectPool == null)
        {
            objectPool = Create();
        }
        
        objectPool.transform.SetPositionAndRotation(position, rotation);
        
        objectPool.gameObject.SetActive(true);
        return objectPool;
    }
    
    public void Released(T objectToRelease)
    {
        objectToRelease.gameObject.SetActive(false);
    }
    
    public T Create()
    {
        var newObject = GameObject.Instantiate(_prefab);
        _objects.Add(newObject);
        return newObject;
    }
}
