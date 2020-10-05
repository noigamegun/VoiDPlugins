using System;
using System.Numerics;
using OpenTabletDriver.Plugin.Attributes;
using OpenTabletDriver.Plugin.Tablet;
using OTDPlugins.MeL.Core;

namespace OTDPlugins.MeL
{
    [PluginName("MeL")]
    public class MeLFilter : IFilter
    {
        public Vector2 Filter(Vector2 point)
        {
            Core.Add(point);
            return Core.IsReady ? Core.Predict(DateTime.UtcNow, Offset) : point;
        }

        public FilterStage FilterStage => FilterStage.PostTranspose;

        [UnitProperty("Offset", "ms")]
        public float Offset { set; get; }

        [Property("Samples")]
        public int Samples { set => Core.Samples = value; }

        [Property("Complexity")]
        public int Complexity { set => Core.Complexity = value; }

        [Property("Weight")]
        public float Weight { set => Core.Weight = value; }

        private readonly MLCore Core = new MLCore();
    }
}