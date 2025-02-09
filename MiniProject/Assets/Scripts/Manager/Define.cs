using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public enum Languages
{
    Korean,
    English,
    Japanese,
}

public static class DataTableIds
{
    public static readonly string[] String =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp",
    };

    public static readonly string Bullet = "BulletTable";
    public static readonly string Player = "PlayerTable";
    public static readonly string NormalMonster = "NormalMonsterTable";
    public static readonly string UnBreakable = "UnBreakableTable";
    public static readonly string Boss = "BossTable";
    public static readonly string Minion = "MinionTable";
    public static readonly string EnemyBullet = "EnemyBulletTable";
}

public static class Variables
{
    public static Languages currentLang = Languages.Korean;
}

public static class Tags
{
    public static readonly string Player = "Player";
}

public static class SortingLayers
{
    public static readonly string Default = "Default";
}

public static class Layers
{
    public static readonly string Default = "Default";
    public static readonly string UI = "UI";
}

public static class GMCT
{
    public static readonly string GM = "GameController";
    public static readonly string UI = "UI";
}

public enum ItemType
{
    Null,
    Common,   // 일반 보급상자
    Legendary // 전설 보급상자
}

