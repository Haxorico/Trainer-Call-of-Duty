public static class Offsets
{
    public static int dwLocalPlayer = 0x0;
    public static int dwPlayerHP = 0x12FC58;
    public static int dwPlayerPosition = 0x2BDE94;
    public static int m_vecOrigin = 0x0;
    public static int m_vecViewOffset = 0x0;

    public static uint pViewMatrix = 0xDBEB90;

    //"gamex86.dll"+0029D794
    public static int eFirstEntityAdr = 0x0029D794;
    public static int eNextEntity = 0x60;

    public static int ePos1a = 0x20;
    public static int ePos2a = 0xC8;
    public static int ePos3a = 0xD4;
    public static int ePos4a = 0xE0;
    public static int ePart2 = 0x10C;
    public static int eCurrentHP = 0x1D8;
    public static int eMaxHP = 0x1DC;

    public static int ePos1b = 0xC;
    public static int ePos2b = 0x6C;
    public static int ePos3b = 0x78;
    public static int ePos4b = 0xCC;
    public static int ePos5b = 0xD8;


    public const string NAME_PROCESS = "CoDSP";
    public const string NAME_MODULE_CLIENT = "CoDSP.exe";
    public const string NAME_MODULE_ENGINE = "gamex86.dll";
    public const string NAME_WINDOW = "Call of Duty";

    public static System.IntPtr MOD_BASE_ADR { get; set; }
}

