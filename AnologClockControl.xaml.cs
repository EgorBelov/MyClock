using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace MyClock
{
    /// <summary>
    /// Interaction logic for AnologClockControl.xaml
    /// </summary>
    public partial class AnologClockControl : UserControl
    {
        private double _hourAngle;
        public AnologClockControl()
        {
            InitializeComponent();
            CompositionTarget.Rendering += CompositionTarget_Rendering;
           
        }

        private void CompositionTarget_Rendering(object sender, object args)
        {
            DateTime dt = DateTime.Now;
            rotateSecond.Angle = 6 * (dt.Second + dt.Millisecond / 1000.0);
            rotateMinute.Angle = 6 * dt.Minute + rotateSecond.Angle / 60;
            rotateHour.Angle = 30 * (dt.Hour % 12) + rotateMinute.Angle / 12;
        }

        const double ScaleRate = 2.0;
        private void canvas1_MouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0)
            {
                scaleTransform1.CenterX = e.GetPosition(cnv).X;
                scaleTransform1.CenterY = e.GetPosition(cnv).Y;
                scaleTransform1.ScaleX *= ScaleRate;
                scaleTransform1.ScaleY *= ScaleRate;
            }
            else
            {
                scaleTransform1.CenterX = e.GetPosition(cnv).X;
                scaleTransform1.CenterY = e.GetPosition(cnv).Y;
                scaleTransform1.ScaleX /= ScaleRate;
                scaleTransform1.ScaleY /= ScaleRate;
            }
        }
    }
}
