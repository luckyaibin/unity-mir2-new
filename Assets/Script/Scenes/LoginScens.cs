using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginScens : MonoBehaviour, ProcessPacket
{
    private const string TAG = "LoginScens";
    public Button login;
    public Button connect;
    public Button startGame;
    public Text characterInfo;
    private List<SelectInfo> characters;
     void Start()
    {
        login.onClick.AddListener(delegate
        {
            OnClick(login.gameObject);

        });
        connect.onClick.AddListener(delegate
        {
            OnClick(connect.gameObject);
        });
        startGame.onClick.AddListener(delegate
        {
            OnClick(startGame.gameObject);
        });
        MapConfigs.init();
    }

    // Update is called once per frame
    void Update()
    {
        // Logger.Debugf("fucking running...");
    }
    public void OnClick(GameObject sender)
    {
        Debug.Log(sender.name);
        switch (sender.name)
        {
            case "BtnConnect":Network.Connect("127.0.0.1",7000);
                Network.loginScens = this;
                break;
            case "BtnLogin":
                var account = new ClientPackets.Login { AccountID = "wab2", Password = "123456" };
                Network.Enqueue(account);
                break;
            case "BtnStart":
                Network.Enqueue(new ClientPackets.StartGame { CharacterIndex = characters[0].Index });
                // DontDestroyOnLoad(gameManager);
                SceneManager.LoadScene("mainScens");
                Network.loginScens = null;
                break;
        }
    }

    public void process(Packet p)
    {
        switch (p.Index)
        {
            case (short)ServerPacketIds.Connected:
                Network.Connected = true;
                SendVersion();
                break;
            case (short)ServerPacketIds.ClientVersion:
                // ClientVersion((S.ClientVersion)p);
                break;
            case (short)ServerPacketIds.NewAccount:
                //  NewAccount((S.NewAccount)p);
                break;
            case (short)ServerPacketIds.ChangePassword:
                //  ChangePassword((S.ChangePassword)p);
                break;
            case (short)ServerPacketIds.ChangePasswordBanned:
                //  ChangePassword((S.ChangePasswordBanned)p);
                break;
            case (short)ServerPacketIds.Login:
                //   Login((S.Login)p);
                break;
            case (short)ServerPacketIds.LoginBanned:
                //  Login((S.LoginBanned)p);
                break;
            case (short)ServerPacketIds.LoginSuccess:
                Login((ServerPackets.LoginSuccess)p);
                break;
            case (short)ServerPacketIds.StartGame:
                StartGame((ServerPackets.StartGame)p);
                break;
            default:
                //    base.ProcessPacket(p);
                break;
        }
    }

    private void Login(ServerPackets.LoginSuccess p)
    {
        var strTmp = "";
        characters = p.Characters;
        foreach (var tmp in p.Characters)
        {
            strTmp += "name :" + tmp.Name + "\n";
            strTmp += "Level :" + tmp.Level + "\n";
            strTmp += "Class :" + tmp.Class + "\n";
            strTmp += "LastAccess :" + tmp.LastAccess + "\n";
        }
        characterInfo.text = strTmp;
    }

    private void SendVersion()
    {
        ClientPackets.ClientVersion p = new ClientPackets.ClientVersion();
        p.VersionHash = Encoding.ASCII.GetBytes("Sending Client Version.".ToCharArray());
        Network.Enqueue(p);
    }

    public void StartGame(ServerPackets.StartGame p)
    {
        switch (p.Result)
        {
            case 0:
                Logger.Infof("%s->%s",TAG, "Starting the game is currently disabled.");
                break;
            case 1:
                Logger.Infof("%s->%s",TAG, "You are not logged in.");
                break;
            case 2:
                Logger.Infof("%s->%s",TAG, "Your character could not be found.");
                break;
            case 3:
                Logger.Infof("%s->%s",TAG, "No active map and/or start point found.");
                break;
            case 4:
                Logger.Infof("%s->%s",TAG, "start game success");
                break;
        }


    }

    private void OnDestroy()
    {
        Destroy(gameObject);
    }

}
