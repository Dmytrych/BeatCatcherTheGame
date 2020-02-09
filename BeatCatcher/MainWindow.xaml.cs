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
using System.Windows.Media.Animation;

namespace BeatCatcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void StartButton_Click(object sender, RoutedEventArgs e)
        {
            (sender as UIElement).Visibility = Visibility.Hidden;
            BlockRoad road = new BlockRoad(new Point(this.Width / 2, this.Height / 2), this);
            road.CreateMovingBlock(mainGrid, 0);
            road.CreateMovingBlock(mainGrid, 20);
            road.CreateMovingBlock(mainGrid, 40);
            road.CreateMovingBlock(mainGrid, 60);
            road.CreateMovingBlock(mainGrid, 80);
            road.CreateMovingBlock(mainGrid, 100);
            road.CreateMovingBlock(mainGrid, 120);
            road.CreateMovingBlock(mainGrid, 140);
            road.CreateMovingBlock(mainGrid, 160);
            road.CreateMovingBlock(mainGrid, 180);
            this.WindowState = WindowState.Maximized;
        }
    }
    class BlockRoad
    {
        double deg = 3.14 / 180;
        Point center;
        private int ViewDistance { get; set; }
        private int StartingBlockSize { get; set; }
        public BlockRoad(Point center, MainWindow window)
        {
            this.center = center;
            ViewDistance = (int)window.ActualHeight / 10;
            StartingBlockSize = ViewDistance / 3;
        }
        public void CreateMovingBlock(Grid grid, double angle)
        {
            angle *= deg;
            Point startingPoint = new Point(center.X + ViewDistance * System.Math.Cos(angle), center.Y - ViewDistance * System.Math.Sin(angle));
            Image block = CreateBlock(grid);
            block.VerticalAlignment = VerticalAlignment.Top;
            block.HorizontalAlignment = HorizontalAlignment.Left;
            grid.Children.Add(block);
            ThicknessAnimation movement = ThicknessAnimationBlockMovement(startingPoint, angle, grid.ActualHeight / 2);
            DoubleAnimation resize = DoubleAnimationResizeBlock();
            block.BeginAnimation(Image.MarginProperty, movement);
            block.BeginAnimation(Image.WidthProperty, resize);
            block.BeginAnimation(Image.HeightProperty, resize);
        }
        private ThicknessAnimation ThicknessAnimationBlockMovement(Point startPosition, double angleDeg, double Length)
        {
            ThicknessAnimation animation = new ThicknessAnimation();
            Thickness pos = new Thickness();
            pos.Left = startPosition.X;
            pos.Top = startPosition.Y;
            animation.From = pos;
            pos.Left = startPosition.X + System.Math.Cos(angleDeg) * Length;
            pos.Top = startPosition.Y - System.Math.Sin(angleDeg) * Length;
            animation.To = pos;
            animation.AccelerationRatio = 1;
            animation.Duration = TimeSpan.FromSeconds(2);
            return animation;
        }
        private Image CreateBlock(Grid grid)
        {
            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri("Block.png", UriKind.Relative);
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.EndInit();
            Image imag = new Image();
            imag.Source = img;
            imag.Width = StartingBlockSize;
            imag.Height = StartingBlockSize;
            return imag;
        }
        private DoubleAnimation DoubleAnimationResizeBlock()
        {
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = StartingBlockSize;
            animation.To = ViewDistance;
            animation.AccelerationRatio = 1;
            animation.Duration = TimeSpan.FromSeconds(2);
            return animation;
        }
    }
}
