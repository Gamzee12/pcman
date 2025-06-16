using System;
using System.Windows.Media;

namespace PacMan
{
    public class pacman
    {
        public pacman()
        {
        }
        public static ImageBrush Fill { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public static RotateTransform RenderTransform { get; internal set; }
        public static int Width { get; internal set; }
        public static int Height { get; internal set; }
    }
}
