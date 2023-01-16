
public enum MirDirection : byte
{
    Up = 0,
    UpRight = 1,
    Right = 2,
    DownRight = 3,
    Down = 4,
    DownLeft = 5,
    Left = 6,
    UpLeft = 7
}
public enum MirAction : byte
{
    Standing,
    Walking,
    Running,
    Pushed,
    DashL,
    DashR,
    DashFail,
    Stance,
    Stance2,
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    Attack5,
    AttackRange1,
    AttackRange2,
    AttackRange3,
    Special,
    Struck,
    Harvest,
    Spell,
    Die,
    Dead,
    Skeleton,
    Show,
    Hide,
    Stoned,
    Appear,
    Revive,
    SitDown,
    Mine,
    Sneek,
    DashAttack,
    Lunge,

    WalkingBow,
    RunningBow,
    Jump,

    MountStanding,
    MountWalking,
    MountRunning,
    MountStruck,
    MountAttack,

    FishingCast,
    FishingWait,
    FishingReel
}