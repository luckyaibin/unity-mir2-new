
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;
using MLibraryUtils;

public class xxxxxx
{
    void xxxx(Libraries libs, Spell spell)
    {
        MLibrary BodyLibrary;
        int DrawFrame;
        int FrameInterval;
        int FrameCount;
        bool Blend;
        int Light;
        bool Repeat;
        switch (spell)
        {
            case Spell.TrapHexagon:
                BodyLibrary = libs.Magic;
                DrawFrame = 1390;
                FrameInterval = 100;
                FrameCount = 10;
                Blend = true;
                break;
            case Spell.FireWall:
                BodyLibrary = libs.Magic;
                DrawFrame = 1630;
                FrameInterval = 120;
                FrameCount = 6;
                Light = 3;
                Blend = true;
                break;
            case Spell.PoisonCloud:
                BodyLibrary = libs.Magic2;
                DrawFrame = 1650;
                FrameInterval = 120;
                FrameCount = 20;
                Light = 3;
                Blend = true;
                break;

            case Spell.Blizzard:
                BodyLibrary = libs.Magic2;
                DrawFrame = 1550;
                FrameInterval = 100;
                FrameCount = 30;
                Light = 3;
                Blend = true;
                Repeat = false;
                break;
            case Spell.MeteorStrike:
                BodyLibrary = libs.Magic2;
                DrawFrame = 1610;
                FrameInterval = 100;
                FrameCount = 30;
                Light = 3;
                Blend = true;
                Repeat = false;
                break;

            case Spell.Reincarnation:
                BodyLibrary = libs.Magic2;
                DrawFrame = 1680;
                FrameInterval = 100;
                FrameCount = 10;
                Light = 1;
                Blend = true;
                Repeat = true;
                break;
        }
    }
}