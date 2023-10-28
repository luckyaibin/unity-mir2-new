using MLibraryUtils;
using UnityEngine;


public class MapConfigs
{
    public static readonly int MAP_TILE_WIDTH = 48;
    public static readonly int MAP_TILE_HEIGHT = 32;
    public static string MIR_RES_PATH;
    public static string MAP_DIR;
    public static string Data_Dir;
    public static readonly string[] MAP_LIBS = new string[400];
    public static readonly int OffSetX = Screen.width / 2 / MAP_TILE_WIDTH;
    public static readonly int OffSetY = Screen.height / 2 / MAP_TILE_HEIGHT - 1;
    public static readonly int ViewRangeX = OffSetX + 4;
    public static readonly int ViewRangeY = OffSetY + 4;

    private static void initLibConfigs()
    {
        #region MAP_LIBS
        //wemade mir2 (allowed from 0-99)
        MAP_LIBS[0] = Data_Dir + "Map/WemadeMir2/Tiles";
        MAP_LIBS[1] = Data_Dir + "Map/WemadeMir2/Smtiles";
        MAP_LIBS[2] = Data_Dir + "Map/WemadeMir2/Objects";
        for (int i = 2; i < 27; i++)
        {
            MAP_LIBS[i + 1] = Data_Dir + "Map/WemadeMir2/Objects" + i.ToString();
        }
        //shanda mir2 (allowed from 100-199)
        MAP_LIBS[100] = Data_Dir + "Map/ShandaMir2/Tiles";
        for (int i = 1; i < 10; i++)
        {
            MAP_LIBS[100 + i] = Data_Dir + "Map/ShandaMir2/Tiles" + (i + 1);
        }
        MAP_LIBS[110] = Data_Dir + "Map/ShandaMir2/SmTiles";
        for (int i = 1; i < 10; i++)
        {
            MAP_LIBS[110 + i] = Data_Dir + "Map/ShandaMir2/SmTiles" + (i + 1);
        }
        MAP_LIBS[120] = Data_Dir + "Map/ShandaMir2/Objects";
        for (int i = 1; i < 31; i++)
        {
            MAP_LIBS[120 + i] = Data_Dir + "Map/ShandaMir2/Objects" + (i + 1);
        }
        MAP_LIBS[190] = Data_Dir + "Map/ShandaMir2/AniTiles1";
        //wemade mir3 (allowed from 200-299)
        string[] Mapstate = { "", "wood/", "sand/", "snow/", "forest/" };
        for (int i = 0; i < Mapstate.Length; i++)
        {
            MAP_LIBS[200 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Tilesc";
            MAP_LIBS[201 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Tiles30c";
            MAP_LIBS[202 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Tiles5c";
            MAP_LIBS[203 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Smtilesc";
            MAP_LIBS[204 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Housesc";
            MAP_LIBS[205 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Cliffsc";
            MAP_LIBS[206 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Dungeonsc";
            MAP_LIBS[207 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Innersc";
            MAP_LIBS[208 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Furnituresc";
            MAP_LIBS[209 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Wallsc";
            MAP_LIBS[210 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "smObjectsc";
            MAP_LIBS[211 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Animationsc";
            MAP_LIBS[212 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Object1c";
            MAP_LIBS[213 + (i * 15)] = Data_Dir + "Map/WemadeMir3/" + Mapstate[i] + "Object2c";
        }
        Mapstate = new string[] { "", "wood", "sand", "snow", "forest" };
        //shanda mir3 (allowed from 300-399)
        for (int i = 0; i < Mapstate.Length; i++)
        {
            MAP_LIBS[300 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Tilesc" + Mapstate[i];
            MAP_LIBS[301 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Tiles30c" + Mapstate[i];
            MAP_LIBS[302 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Tiles5c" + Mapstate[i];
            MAP_LIBS[303 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Smtilesc" + Mapstate[i];
            MAP_LIBS[304 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Housesc" + Mapstate[i];
            MAP_LIBS[305 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Cliffsc" + Mapstate[i];
            MAP_LIBS[306 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Dungeonsc" + Mapstate[i];
            MAP_LIBS[307 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Innersc" + Mapstate[i];
            MAP_LIBS[308 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Furnituresc" + Mapstate[i];
            MAP_LIBS[309 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Wallsc" + Mapstate[i];
            MAP_LIBS[310 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "smObjectsc" + Mapstate[i];
            MAP_LIBS[311 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Animationsc" + Mapstate[i];
            MAP_LIBS[312 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Object1c" + Mapstate[i];
            MAP_LIBS[313 + (i * 15)] = Data_Dir + "Map/ShandaMir3/" + "Object2c" + Mapstate[i];
        }
        #endregion
    }
    public static void init()
    {
        MIR_RES_PATH = Application.dataPath + "/Resources/";
        MAP_DIR = MIR_RES_PATH + "mir/Map/";
        Data_Dir = MIR_RES_PATH + "mir/Data/";
        System.Console.Write("static void init Data_Dir ------>" + Data_Dir);
        initLibConfigs();
    }
}