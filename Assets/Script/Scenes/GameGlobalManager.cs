using UnityEngine;


public class GameGlobalManager : MonoBehaviour
{
    private static GameGlobalManager _instance;
    private static readonly object _syslock = new object();
    public static GameGlobalManager instance
    {
        get
        {
            return _instance;
        }
    }
    // Use this for initialization
    void Start()
    {
        Logger.Debugf("GameGlobalManager Start...");
        // 该实例本身就已经存在了，直接返回 //
        if (_instance == null)
        {
            GameObject go = new("gameGlobalManager");
            DontDestroyOnLoad(go);
            _instance = go.AddComponent<GameGlobalManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Logger.Debugf("GameGlobalManager  Update...");
        Network.Tick();
    }
}
