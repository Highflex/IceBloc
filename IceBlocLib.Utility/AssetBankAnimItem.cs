using IceBlocLib.Frostbite;
using IceBlocLib.InternalFormats;

namespace IceBlocLib.Utility;

public class AssetBankAnimItem
{
    public string Name { get; set; }
    public string Codec { get; set; }

    public AssetBankAnimItem(string name, string codec)
    {
        Name = name;
        Codec = codec;
    }
}
