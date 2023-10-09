using UnityEngine;


public class GameGlobalManager : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        Logger.Debugf("GameGlobalManager Start...");
        //DontDestroyOnLoad(this);
        // MirNetwork.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        // Logger.Debugf("GameGlobalManager  Update...");
        Network.Process();
    }
}
