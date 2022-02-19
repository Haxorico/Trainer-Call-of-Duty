public static class Offsets
{


    public static uint mEntityListStart = 0x00296540;

    public static uint pViewMatrix = 0xDBEB90;

    //"gamex86.dll"+0029D794
    //public static uint eFirstEntityAdr = 0x0012FA80;
    public static uint pLocalPlayer = 0x0012FA80;
    public static uint eTeam = 0x10;
    public static int ePos1a = 0x20;
    public static int ePos2a = 0xC8;
    public static int ePos3a = 0xD4;
    public static int ePos4a = 0xE0;
    //public static uint ePart2 = 0x10C;
    //public static uint ePart2b = 0x108;
    //public static uint ePart3b = 0x4;
    //public static uint eNextEntity = 0x60;
    //public static uint eNextEntityB = 0x60;
    public static uint eCurrentHP = 0x1D8;
    public static int eMaxHP = 0x1DC;

    //public static int ePos1b = 0x0C;
    //public static int ePos2b = 0x6C;
    //public static int ePos3b = 0x78;
    //public static int ePos4b = 0xCC;
    //public static int ePos5b = 0xD8;


    public const string NAME_PROCESS = "CoDSP";
    public const string NAME_BASE = "CoDSP.exe";
    public const string NAME_MODULE_ENGINE = "gamex86.dll";
    public const string NAME_WINDOW = "Call of Duty";

    public static System.IntPtr ADR_BASE { get; set; }
    public static System.IntPtr ADR_MOD_ENGINE { get; set; }
}

