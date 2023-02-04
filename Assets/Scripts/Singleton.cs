//This is a singleton that can be used as a base class for managers
using System;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static T _instance;

    /// <summary> Singleton instance. </summary>

    public static T Instance
    {
        get
        {
//If not in play mode return null
            if (!Application.isPlaying)
                return null;


            if (_instance != null) return _instance;

            //if private instance is null, try to find a match in the scene
            _instance = (T)FindObjectOfType(typeof(T));
            if (_instance != null)
            {
                //Send to console what happened
                Debug.Log($"[Singleton] Using instance already in scene: {_instance}");
                return _instance;
            }

            //If instance is still null, try to find a match in the resource folder and instantiate it   
            GameObject prefab = Resources.Load($"managers/{typeof(T).ToString()}") as GameObject;
            if (prefab != null)
            {
                _instance = Instantiate(prefab).GetComponent<T>();
                _instance.name = typeof(T).ToString();
                OrganizeManager(_instance);
                //Send to console what happened
                //[Singleton] An instance of ConstructionManager is needed in the scene. A suitable prefab was found in resources and 'ConstructionManager(Clone) (ConstructionManager)' was instansiated.
                Debug.Log($"[Singleton] An instance of {typeof(T)} is needed in the scene. A suitable prefab was found in resources and '{_instance.name}' was instansiated.");
                return _instance;
            }

            //If no prefab is found, create a new instance from scratch
            GameObject singleton = new GameObject();
            _instance = singleton.AddComponent<T>();
            singleton.name = typeof(T).ToString();
            OrganizeManager(_instance);
            //Send to console what happened
            Debug.Log($"[Singleton] An instance of {typeof(T)} is needed in the scene. No suitable prefab in resources found so '{_instance.gameObject.name}' was created.");
            return _instance;

        }
    }

    private static void OrganizeManager(T instance)
    {
        //Find object in scene called "Singleton Managers" and attach this child to that transform
        //If there is no such object, create it
        GameObject singletonManagers = GameObject.Find("Singleton Managers");
        if (singletonManagers == null)
        {
            singletonManagers = new GameObject();
            singletonManagers.name = "Singleton Managers";
            DontDestroyOnLoad(singletonManagers);
        }
        _instance.transform.parent = singletonManagers.transform;
    }
}

