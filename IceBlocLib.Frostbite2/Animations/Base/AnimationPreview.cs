using IceBlocLib.Frostbite;
using IceBlocLib.Utility;
using System.Threading.Channels;

namespace IceBlocLib.Frostbite2.Animations.Base;

public class AnimationPreview
{
    public string Name;

    public int CodecType;
    public int AnimId;
    public float TrimOffset;
    public ushort EndFrame;
    public bool Additive;
    public Guid ID;
    public Guid ChannelToDofAsset;
    public float FPS;
    public StorageType StorageType;

    public string CodecFormat;

    public AnimationPreview(Stream stream, int index, ref GenericData gd, bool bigEndian, string codecFormat)
    {
        using var r = new BinaryReader(stream);
        r.ReadGdDataHeader(bigEndian, out uint base_hash, out uint base_type, out uint base_baseOffset);

        var baseData = gd.ReadValues(r, index, base_baseOffset, base_type, bigEndian);

        Name = (string)baseData["__name"];
        ID = (Guid)baseData["__guid"];

        /*
        CodecType = (int)baseData["CodecType"];
        AnimId = (int)baseData["AnimId"];
        TrimOffset = (float)baseData["TrimOffset"];
        EndFrame = (ushort)baseData["EndFrame"];
        Additive = (bool)baseData["Additive"];
        ChannelToDofAsset = (Guid)baseData["ChannelToDofAsset"];
        */

        // added for preview!
        CodecFormat = codecFormat;
    }
}
