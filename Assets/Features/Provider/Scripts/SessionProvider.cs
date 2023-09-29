using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SessionProvider : MonoBehaviour
{
    public static SessionProvider Instance;

    private readonly Dictionary<string, object> _cache = new();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T Get<T>(SessionKey key, T defaultValue)
    {
        if (_cache.TryGetValue(key.ToString(), out var value))
        {
            return (T)value;
        }

        return defaultValue;
    }

    public void Set<T>(SessionKey key, T value)
    {
        _cache[key.ToString()] = value;
    }
}

public abstract class SessionKey
{
    //
}