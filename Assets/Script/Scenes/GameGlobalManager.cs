using UnityEngine;


public class GameGlobalManager : MonoBehaviour
{
     public static GameGlobalManager _instance;
    // Use this for initialization
    void Awake()
    {
        Logger.Debugf("GameGlobalManager Start...");
        // 该实例本身就已经存在了，直接返回 //
        if (_instance == null)
        {
            var go = this.gameObject;
            DontDestroyOnLoad(go);
           _instance=this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Logger.Debugf("GameGlobalManager  Update...");
        Network.Tick();
    }
    void OnDestroy(){
        Network.Disconnect();
    }
}
