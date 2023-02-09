using System.Collections;
using System.Collections.Generic;
using ServerPackets;
using UnityEngine;

public class TestPlayerObject : MonoBehaviour
{
    private ObjectPlayer objectPlayerData;

    //private PlayerObjectBuilder playerObjectBuilder;

    //代码创建
    private PlayerController playerController;

    private MirAction mirAction = MirAction.Standing;

    private MirDirection mirDirection = MirDirection.Up;

    public GameObject testSpell;

    private void Awake(){
        this.objectPlayerData = new ObjectPlayer{
            Hair = 0,
            Armour = 0,
            Weapon = 0,
            Location = new Vector2(0,0),
            Direction = MirDirection.Up,
            Name = "testPlayer"
        };
        var localPos = this.transform.localPosition;
        Debug.LogFormat( "当前坐标: {0},{1},{2}",localPos.x,localPos.y,localPos.z );
        this.playerController = this.gameObject.AddComponent<PlayerController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // this.playerObjectBuilder = new PlayerObjectBuilder();
        // var tmp = playerObjectBuilder.gameObject(objectPlayerData);
        // //tmp.transform.SetParent()
        // this.playerController = tmp.AddComponent<PlayerController>();

        this.playerController.onInit(this.objectPlayerData);
    }

    public void onDirectionClick(MirDirection direction){
        this.mirDirection = direction;
        this.playerController.playAnimArmourHairWeapon(this.mirAction,this.mirDirection);
    }

    public void onActionClick(MirAction action){
        this.mirAction = action;
        this.playerController.playAnimArmourHairWeapon(this.mirAction,this.mirDirection);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
