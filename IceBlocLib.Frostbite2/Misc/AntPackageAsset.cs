using IceBlocLib.Frostbite2.Animations.Base;
using IceBlocLib.InternalFormats;

namespace IceBlocLib.Frostbite2.Misc;

public class AntPackageAsset
{
    public static List<InternalAnimation> ConvertToInternal(in Dbx dbx)
    {
        Guid guid = (Guid)dbx.Prim["StreamingGuid"].Value;
        if (guid == Guid.Empty)
            return new List<InternalAnimation>();

        using var chunk = new MemoryStream(IO.GetChunk(guid));

        return ConvertToInternal(chunk);
    }

    public static List<InternalAnimation> ConvertToInternal(Stream chunk)
    {
        List<InternalAnimation> result = new();
        GenericData gd = new(chunk);

        for (int i = 0; i < gd.Data.Count; i++)
        {
            using var stream = new MemoryStream(gd.Data[i].Bytes.ToArray());
            object entry = gd.Deserialize(stream, i, gd.Data[i].BigEndian);
            if (entry is FrameAnimation frameAnim)
            {
                Console.Write("Processing Frame Anim: "+ frameAnim.Name + "\n");

                /* Highflex: stop adding if invalid */
                if (!frameAnim.bIsInvalid)
                    result.Add(frameAnim.ConvertToInternal());
            }
            else if (entry is RawAnimation rawAnim)
            {
                Console.Write("Processing Raw Anim: " + rawAnim.Name + "\n");

                /* Highflex: stop adding if invalid */
                if (!rawAnim.bIsInvalid)
                    result.Add(rawAnim.ConvertToInternal());
            }   
            else if (entry is DctAnimation dctAnim)
            {
                Console.Write("Processing DCT Anim: " + dctAnim.Name + "\n");

                /* Highflex: stop adding if invalid */
                if (!dctAnim.bIsInvalid)
                    result.Add(dctAnim.ConvertToInternal());
            }
            //else if (entry is CurveAnimation curAnim)
            //    result.Add(curAnim.ConvertToInternal());
        }
        return result;
    }
}
