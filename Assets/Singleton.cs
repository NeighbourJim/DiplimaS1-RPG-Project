using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour {

    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.Log(string.Format("Singleton Instance {0} already destroyed on application quit.", typeof(T)));
                return null;
            }

            lock (_lock)
            {
                if(_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if(FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("BIG PROBLEM WITH SINGLETON");
                        return _instance;
                    }

                    if(_instance == null)
                    {
                        GameObject singletonPrefab = null;
                        GameObject singleton = null;

                        singletonPrefab = (GameObject)Resources.Load(typeof(T).ToString(), typeof(GameObject));

                        if (singletonPrefab != null)
                        {
                            singleton = Instantiate(singletonPrefab);
                            _instance = singleton.GetComponent<T>();
                        }
                        else
                        {
                            singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                        }
                        singleton.name = "(singleton)" + typeof(T).ToString();
                        DontDestroyOnLoad(singleton);
                    }
                    else
                    {
                        Debug.Log("Singleton of type " + typeof(T).ToString() + " already exists: " + _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;

    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}
