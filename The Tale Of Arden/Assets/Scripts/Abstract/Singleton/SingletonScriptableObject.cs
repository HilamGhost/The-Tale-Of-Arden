using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arden
{
    public class SingletonScriptableObject<T> : ScriptableObject where T : SingletonScriptableObject<T>
    {
        static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    T[] assets = Resources.LoadAll<T>("");
                    if (assets == null || assets.Length < 1)
                    {
                        throw new System.Exception("Could not find any singleton.");
                    }
                    else if (assets.Length > 1)
                    {
                        throw new System.Exception("Multiple instances of the singleton");
                    }
                    instance = assets[0];
                }
                return instance;
            }
        }
    }
}
