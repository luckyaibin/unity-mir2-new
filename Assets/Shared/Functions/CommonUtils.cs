using System;
using UnityEngine;

public class CommonUtils
{
    public static Color32 argb2Color32(int color)
    {
        var a = (color >> 24) & 0xff;
        var r = (color >> 16) & 0xff;
        var g = (color >> 8) & 0xff;
        var b = color & 0xff;

        return new Color32((byte)r, (byte)g, (byte)b, (byte)a);
    }
    public static int color32toArgb(Color32 color)
    {
        int a = color.a << 24;
        int r = (color.r << 16) & 0x00ff0000;
        int g = (color.g << 8) & 0x0000ff00;
        int b = color.b & 0x000000ff;

        return a | r | g | b;
    }
}