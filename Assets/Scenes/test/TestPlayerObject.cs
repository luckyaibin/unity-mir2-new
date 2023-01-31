using System.Collections;
using System.Collections.Generic;
using ServerPackets;
using UnityEngine;

public class TestPlayerObject : MonoBehaviour
{
    private ObjectPlayer objectPlayer;

    private PlayerObjectBuilder playerObjectBuilder;

    private PlayerController playerController;

    private MirAction mirAction = MirAction.Standing;

    private MirDirection mirDirection = MirDirection.Up;

    public GameObject testSpell;

    private void Awake(){
        this.objectPlayer = new ObjectPlayer{
            Hair = 0,
            Armour = 0,
            Weapon = 0,
            Location = new Vector2(1,1),
            Direction = MirDirection.Up,
            Name = "testPlayer"
        };
    }

    // Start is called before the first frame update
    void Start()
    {
        this.playerObjectBuilder = new PlayerObjectBuilder();
        var tmp = playerObjectBuilder.gameObject(objectPlayer);
        this.playerController = tmp.GetComponent<PlayerController>();
    }

    public void onDirectionClick(MirDirection direction){
        this.mirDirection = direction;
        this.playerController.playAnimAll(this.mirAction,this.mirDirection);
    }

    public void onActionClick(MirAction action){
        this.mirAction = action;
        this.playerController.playAnimAll(this.mirAction,this.mirDirection);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
