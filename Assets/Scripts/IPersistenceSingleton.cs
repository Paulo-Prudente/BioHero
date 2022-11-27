using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPersistenceSingleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _uniqueInstace;
    private static bool _IsApplicationQuitting = false;

    public static T Instance
    {
        get
        {
            if(_uniqueInstace== null && !_IsApplicationQuitting)
            {
                _uniqueInstace =  FindObjectOfType<T>();

                if(_uniqueInstace == null)
                {
                    GameObject Prefab = Resources.Load<GameObject>(typeof(T).Name);
                    
                    if(Prefab != null)
                    {
                        GameObject PrefabObject = Instantiate<GameObject>(Prefab);
                        
                        if(PrefabObject != null)
                        {
                            _uniqueInstace = PrefabObject.GetComponent<T>();

                        }

                        if(_uniqueInstace == null)
                        {
                            _uniqueInstace = new GameObject(typeof(T).Name).AddComponent<T>();
                        }
                    }
                }
            }



            return _uniqueInstace;
        }

        private set
        {
            if(_uniqueInstace==null)
            {
                _uniqueInstace = value;
                DontDestroyOnLoad(_uniqueInstace.gameObject);
            }
            else
            {
                DestroyImmediate(value.gameObject);
            }
        }

    }

    protected virtual void Awake()
    {
        Instance = this as T;
    }


    protected virtual void OnDestroy()
    {
        if (_uniqueInstace == this)
            _uniqueInstace = null;
    }

    protected virtual void OnApplicationQuit()
    {
        _IsApplicationQuitting = true;
    }

}
