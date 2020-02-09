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
            BlockRoad road = new BlockRoad(new Point(this.ActualWidth / 2, this.ActualHeight / 2), this);
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
            ThicknessAnimation movement;
            DoubleAnimation resize;
            Image block = CreateBlock(grid);
            angle *= deg;
            grid.Children.Add(block);
            //Ellipse el = new Ellipse();
            //grid.Children.Add(el);
            //el.Width = 10;
            //el.Height = 10;
            //el.Fill = Brushes.White;
            movement = ThicknessAnimationBlockMovement( angle, grid.ActualHeight / 2);
            resize = DoubleAnimationResizeBlock();
            block.BeginAnimation(Image.MarginProperty, movement);
            block.BeginAnimation(Image.WidthProperty, resize);
            block.BeginAnimation(Image.HeightProperty, resize);
        }
        private ThicknessAnimation ThicknessAnimationBlockMovement(double angleDeg, double Length)
        {
            ThicknessAnimation animation = new ThicknessAnimation();
            Thickness pos = new Thickness();
            pos.Left = - ViewDistance * System.Math.Cos(angleDeg);
            pos.Top = - ViewDistance * System.Math.Sin(angleDeg);
            animation.From = pos;
            pos.Left = - System.Math.Cos(angleDeg) * Length;
            pos.Top = - System.Math.Sin(angleDeg) * Length;
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
