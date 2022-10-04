using Pools;
using UnityEngine;

public class Lines : MonoBehaviour
{
    #region Singleton

    //-------------------------------------------------------------
    public static Lines Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            AwakeJob();
            // transform.SetParent(null);
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //-------------------------------------------------------------

    #endregion

    public PatternLine prefab;
    public PatternLinePool pool;
    public PatternLine Get() => pool.Get();
    void AwakeJob()
    {
        pool.SetPrefab(prefab);
    }
}