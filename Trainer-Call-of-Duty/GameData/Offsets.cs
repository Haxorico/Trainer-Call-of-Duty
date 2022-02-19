public static class Offsets
{


    //m => requires module engine
    //e => requires entity address
    //p => requires base address (.exe)

    public static uint pViewMatrix = 0xDBEB90;

    public static uint mEntityListStart = 0x00296540;
    public static int mLocalPlayer = 0x0012FA80;

    public static int eTeam = 0x10;
    public static int ePos1 = 0x20;
    public static int ePos2 = 0xC8;
    public static int ePos3 = 0xD4;
    public static int ePos4 = 0xE0;
    public static int eCurrentHP = 0x1D8;
    public static int eMaxHP = 0x1DC;

    public const string NAME_PROCESS = "CoDSP";
    public const string NAME_BASE = "CoDSP.exe";
    public const string NAME_MODULE_ENGINE = "gamex86.dll";
    public const string NAME_WINDOW = "Call of Duty";

    public static System.IntPtr ADR_BASE { get; set; }
    public static System.IntPtr ADR_MOD_ENGINE { get; set; }
}

