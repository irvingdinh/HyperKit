using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bootstrap : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        Application.targetFrameRate = 120;
        
        Object.DontDestroyOnLoad(Object.Instantiate(Resources.Load("ProviderPrefab")));
    }
}
