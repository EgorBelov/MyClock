using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyClock
{
    /// <summary>
    /// Interaction logic for DigitalClockControl.xaml
    /// </summary>
    public partial class DigitalClockControl : UserControl
    {
        private DispatcherTimer timer;
        private int hours, minutes, seconds;


        public DigitalClockControl()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;

            // Set start time to current time
            hours = DateTime.Now.Hour;
            minutes = DateTime.Now.Minute;
            seconds = DateTime.Now.Second;

            // Display initial time
            UpdateDisplay();
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            // Increment time
            seconds++;
            if (seconds == 60)
            {
                seconds = 0;
                minutes++;
                if (minutes == 60)
                {
                    minutes = 0;
                    hours++;
                    if (hours == 24)
                    {
                        hours = 0;
                    }
                }
            }

            // Update display
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            // Update hours display
            int hoursTens = hours / 10;
            int hoursOnes = hours % 10;
            DrawDigit(hoursTens, HourTenCanvas);
            DrawDigit(hoursOnes, HourOneCanvas);

            // Update minutes display
            int minutesTens = minutes / 10;
            int minutesOnes = minutes % 10;
            DrawDigit(minutesTens, MinuteTenCanvas);
            DrawDigit(minutesOnes, MinuteOneCanvas);

            // Update seconds display
            int secondsTens = seconds / 10;
            int secondsOnes = seconds % 10;
            DrawDigit(secondsTens, SecondTenCanvas);
            DrawDigit(secondsOnes, SecondOneCanvas);
        }

        private void DrawDigit(int digit, Canvas canvas)
        {
            // Clear canvas
            canvas.Children.Clear();

            // Draw digit using 7-segment display
            if (digit >= 0 && digit <= 9)
            {
                bool[] segments = GetSegmentsForDigit(digit);
                DrawSegment(segments[0], new Point(0, 2), new Point(2, 0), new Point(16, 0), new Point(18, 2), new Point(16, 4), new Point(2, 4), canvas);
                DrawSegment(segments[1], new Point(18, 2), new Point(20, 4), new Point(20, 20), new Point(18, 22), new Point(16, 20), new Point(16, 4), canvas);
                DrawSegment(segments[2], new Point(18, 22), new Point(20, 24), new Point(20, 40), new Point(18, 42), new Point(16, 40), new Point(16, 24), canvas);
                DrawSegment(segments[3], new Point(0, 42), new Point(2, 40), new Point(16, 40), new Point(16, 42), new Point(16, 44), new Point(2, 44), canvas);
                DrawSegment(segments[4], new Point(0, 42), new Point(2, 40), new Point(2, 24), new Point(0, 22), new Point(-2, 24), new Point(-2, 40), canvas);
                DrawSegment(segments[5], new Point(0, 22), new Point(2, 20), new Point(2, 4), new Point(0, 2), new Point(-2, 4), new Point(-2, 20), canvas);
                DrawSegment(segments[6], new Point(0, 22), new Point(2, 24), new Point(16, 24), new Point(18, 22), new Point(16, 20), new Point(2, 20), canvas);
            }
            else
            {
                // Invalid digit
                throw new ArgumentException("Invalid digit");
            }
        }
        private bool[] GetSegmentsForDigit(int digit)
        {
            // Returns an array of 7 bools representing the segments of the 7-segment display for a given digit
            switch (digit)
            {
                case 0:
                    return new bool[] { true, true, true, true, true, true, false };
                case 1:
                    return new bool[] { false, true, true, false, false, false, false };
                case 2:
                    return new bool[] { true, true, false, true, true, false, true };
                case 3:
                    return new bool[] { true, true, true, true, false, false, true };
                case 4:
                    return new bool[] { false, true, true, false, false, true, true };
                case 5:
                    return new bool[] { true, false, true, true, false, true, true };
                case 6:
                    return new bool[] { true, false, true, true, true, true, true };
                case 7:
                    return new bool[] { true, true, true, false, false, false, false };
                case 8:
                    return new bool[] { true, true, true, true, true, true, true };
                case 9:
                    return new bool[] { true, true, true, true, false, true, true };
                default:
                    throw new ArgumentException("Invalid digit");
            }
        }

        private void DrawSegment(bool draw, Point p1, Point p2, Point p3, Point p4, Point p5, Point p6, Canvas canvas)
        {
            // Draw a segment of the 7-segment display
            if (draw)
            {
                Polyline segment = new Polyline();
                segment.Points.Add(p1);
                segment.Points.Add(p2);
                segment.Points.Add(p3);
                segment.Points.Add(p4);
                segment.Points.Add(p5);
                segment.Points.Add(p6);
                segment.Points.Add(p1);
                segment.Stroke = Brushes.Pink;
                segment.Fill = Brushes.Black;

                // Remove unnecessary lines to prevent filling of gaps between segments
                if (p1.X == p2.X && p2.X == p3.X)
                {
                    segment.Points.RemoveAt(1);
                    segment.Points.RemoveAt(1);
                }
                else if (p2.Y == p3.Y && p3.Y == p4.Y)
                {
                    segment.Points.RemoveAt(2);
                    segment.Points.RemoveAt(2);
                }
                else if (p4.X == p5.X && p5.X == p6.X)
                {
                    segment.Points.RemoveAt(4);
                    segment.Points.RemoveAt(4);
                }
                else if (p5.Y == p6.Y && p6.Y == p1.Y)
                {
                    segment.Points.RemoveAt(5);
                    segment.Points.RemoveAt(5);
                }

                canvas.Children.Add(segment);
            }
        }
    }
}
