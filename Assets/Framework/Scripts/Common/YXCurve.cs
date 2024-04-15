using System;

namespace YXCell
{
    [Serializable]
    public class YXCurve
    {

        public CurveType curveType;
        public CurveDir curveDir;

        public static YXCurve LinearIn { get { return new YXCurve(CurveType.Linear, CurveDir.In); } }
        public static YXCurve LinearOut { get { return new YXCurve(CurveType.Linear, CurveDir.Out); } }
        public static YXCurve QuadraticIn { get { return new YXCurve(CurveType.Quadratic, CurveDir.In); } }
        public static YXCurve QuadraticOut { get { return new YXCurve(CurveType.Quadratic, CurveDir.Out); } }
        public static YXCurve CubicIn { get { return new YXCurve(CurveType.Cubic, CurveDir.In); } }
        public static YXCurve CubicOut { get { return new YXCurve(CurveType.Cubic, CurveDir.Out); } }
        public static YXCurve QuarticIn { get { return new YXCurve(CurveType.Quartic, CurveDir.In); } }
        public static YXCurve QuarticOut { get { return new YXCurve(CurveType.Quartic, CurveDir.Out); } }
        public static YXCurve QuinticIn { get { return new YXCurve(CurveType.Quintic, CurveDir.In); } }
        public static YXCurve QuinticOut { get { return new YXCurve(CurveType.Quintic, CurveDir.Out); } }
        public static YXCurve QuadraticDoubleIn { get { return new YXCurve(CurveType.QuadraticDouble, CurveDir.In); } }
        public static YXCurve QuadraticDoubleOut { get { return new YXCurve(CurveType.QuadraticDouble, CurveDir.Out); } }
        public static YXCurve CubicDoubleIn { get { return new YXCurve(CurveType.CubicDouble, CurveDir.In); } }
        public static YXCurve CubicDoubleOut { get { return new YXCurve(CurveType.CubicDouble, CurveDir.Out); } }
        public static YXCurve QuarticDoubleIn { get { return new YXCurve(CurveType.QuarticDouble, CurveDir.In); } }
        public static YXCurve QuarticDoubleOut { get { return new YXCurve(CurveType.QuarticDouble, CurveDir.Out); } }
        public static YXCurve QuinticDoubleIn { get { return new YXCurve(CurveType.QuinticDouble, CurveDir.In); } }
        public static YXCurve QuinticDoubleOut { get { return new YXCurve(CurveType.QuinticDouble, CurveDir.Out); } }
        public static YXCurve SineIn { get { return new YXCurve(CurveType.Sine, CurveDir.In); } }
        public static YXCurve SineOut { get { return new YXCurve(CurveType.Sine, CurveDir.Out); } }
        public static YXCurve SineDoubleIn { get { return new YXCurve(CurveType.SineDouble, CurveDir.In); } }
        public static YXCurve SineDoubleOut { get { return new YXCurve(CurveType.SineDouble, CurveDir.Out); } }
        public static YXCurve ExpoIn { get { return new YXCurve(CurveType.Expo, CurveDir.In); } }
        public static YXCurve ExpoOut { get { return new YXCurve(CurveType.Expo, CurveDir.Out); } }
        public static YXCurve ExpoDoubleIn { get { return new YXCurve(CurveType.ExpoDouble, CurveDir.In); } }
        public static YXCurve ExpoDoubleOut { get { return new YXCurve(CurveType.ExpoDouble, CurveDir.Out); } }
        public static YXCurve ElasticIn { get { return new YXCurve(CurveType.Elastic, CurveDir.In); } }
        public static YXCurve ElasticOut { get { return new YXCurve(CurveType.Elastic, CurveDir.Out); } }
        public static YXCurve ElasticDoubleIn { get { return new YXCurve(CurveType.ElasticDouble, CurveDir.In); } }
        public static YXCurve ElasticDoubleOut { get { return new YXCurve(CurveType.ElasticDouble, CurveDir.Out); } }
        public static YXCurve CircIn { get { return new YXCurve(CurveType.Circ, CurveDir.In); } }
        public static YXCurve CircOut { get { return new YXCurve(CurveType.Circ, CurveDir.Out); } }
        public static YXCurve CircDoubleIn { get { return new YXCurve(CurveType.CircDouble, CurveDir.In); } }
        public static YXCurve CircDoubleOut { get { return new YXCurve(CurveType.CircDouble, CurveDir.Out); } }
        public static YXCurve BackIn { get { return new YXCurve(CurveType.Back, CurveDir.In); } }
        public static YXCurve BackOut { get { return new YXCurve(CurveType.Back, CurveDir.Out); } }
        public static YXCurve BackDoubleIn { get { return new YXCurve(CurveType.BackDouble, CurveDir.In); } }
        public static YXCurve BackDoubleOut { get { return new YXCurve(CurveType.BackDouble, CurveDir.Out); } }
        public static YXCurve BounceIn { get { return new YXCurve(CurveType.Bounce, CurveDir.In); } }
        public static YXCurve BounceOut { get { return new YXCurve(CurveType.Bounce, CurveDir.Out); } }
        public static YXCurve BounceDoubleIn { get { return new YXCurve(CurveType.BounceDouble, CurveDir.In); } }
        public static YXCurve BounceDoubleOut { get { return new YXCurve(CurveType.BounceDouble, CurveDir.Out); } }
        public Func<float, float> func { get; private set; }
        public YXCurve(CurveType curveType, CurveDir curveDir)
        {
            this.curveType = curveType;
            this.curveDir = curveDir;
        }
        public YXCurve(Func<float, float> func)
        {
            this.func = func;
        }

        public YXCurve Reverse()
        {
            return new YXCurve(curveType, curveDir == CurveDir.In ? CurveDir.Out : CurveDir.In);
        }
    }
}
