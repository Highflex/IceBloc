﻿using IceBlocLib.InternalFormats;
using IceBlocLib.Utility;

namespace IceBlocLib.Utility.Export;

public class SkeletonExporterSMD : ISkeletonExporter
{
    public void Export(InternalSkeleton skeleton, string path)
    {
        // Start writing to disk.
        using var w = new StreamWriter(File.OpenWrite(path + ".smd"));
        w.WriteLine("version 1");

        w.WriteLine("nodes");

        for (int i = 0; i < skeleton.LocalTransforms.Count; i++)
        {
            w.WriteLine($"{i} \"{skeleton.BoneNames[i]}\" {skeleton.BoneParents[i]}");
        }

        w.WriteLine("end");

        w.WriteLine("skeleton\ntime 0");

        for (int i = 0; i < skeleton.LocalTransforms.Count; i++)
        {
            var pos = skeleton.LocalTransforms[i].Position;
            var rot = skeleton.LocalTransforms[i].EulerAngles;
            w.WriteLine($"{i} {pos.X * 100.0f} {pos.Y * 100.0f} {pos.Z * 100.0f} {rot.X.DegRad()} {rot.Y.DegRad()} {rot.Z.DegRad()}"); // Highflex: implement global scaling here
        }

        w.WriteLine("end");
    }
}
