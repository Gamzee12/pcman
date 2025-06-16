using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PacMan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gametimer = new DispatcherTimer();
        bool goLeft, goRight, goDown, goUp;
        bool noLeft, noRight, noDown, noUp;
        int speed = 8;
        Rect pacmanHitBox;
        int hayaletSpeed = 10;
        int hayaletMoveStop = 130;
        int currentHayaletStep;
        int score = 0;
        private object orangeHayalet;
        private object orangeDusman;
        private ImageBrush pinkDusman;
        private object blueDusman;
        private string message;
        private object txtScore;

        public object MyCanvas { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            GameSetup();
        }

        private void CanvasKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left && noLeft == false)
            {
                goRight = goUp = goDown = false;
                noRight = noUp = noDown = false;
                goLeft = true;
                pacman.RenderTransform = new RotateTransform(-180, pacman.Width / 2, pacman.Height / 2);
            }

            if (e.Key == Key.Right && noRight == false)
            {
                goLeft = goUp = goDown = false;
                noLeft = noUp = noDown = false;
                noRight = true;
                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Up && noUp == false)
            {
                goLeft = goUp = goDown = false;
                noLeft = noUp = noDown = false;
                goUp = true;
                pacman.RenderTransform = new RotateTransform(0, pacman.Width / 2, pacman.Height / 2);
            }
            if (e.Key == Key.Down && noDown == false)
            {
                goLeft = goUp = goLeft = false;
                noLeft = noUp = noLeft = false;
                goDown = true;
                pacman.RenderTransform = new RotateTransform(90, pacman.Width / 2, pacman.Height / 2);
            }
        }
        private void GameSetup()
        {
            MyCanvas.Focus();
            gametimer.Tick += GameLoop;
            gametimer.Interval = TimeSpan.FromMilliseconds(20);
            gametimer.Start();
            currentHayaletStep = hayaletMoveStop;
            ImageBrush pacmanImage = new ImageBrush();
            pacmanImage.ImageSource = new BitmapImage(new Uri("pack :// application:,,,/Resimler/yellow.png"));
            pacman.Fill = pacmanImage;
            ImageBrush turuncuDusman = new ImageBrush();
            orangeHayalet.ImageSource = new BitmapImage(new Uri("pack :// application:,,,/Resimler/orange.png"));
            orangeDusman.Fill = orangeHayalet;
            ImageBrush blueHayalet = new ImageBrush();
            blueHayalet.ImageSource = new BitmapImage(new Uri("pack :// application:,,,/Resimler/blue.jpg"));
            blueDusman.Fill = blueHayalet;
            ImageBrush pinkHayalet = new ImageBrush();
            pinkHayalet.ImageSource = new BitmapImage(new Uri("pack :// application:,,,/Resimler/pink.png"));
            pinkDusman = pinkHayalet;
        }

        private void GameLoop(object sender, EventArgs e)
        {
            txtScore.Content = "Score :" + score;
            if (goRight)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + speed);
            }
            if (goLeft)
            {
                Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - speed);
            }
            if (goUp)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
            }
            if (goDown)
            {
                Canvas.SetTop(pacman, Canvas.GetTop(pacman) - speed);
            }
            if (goDown && Canvas.GetTop(pacman) + 80 > Application.Current.MainWindow.Height)
            {
                noDown = true;
                goUp = false;
            }
            if (goUp && Canvas.GetTop(pacman) < 1)
            {
                noUp = true;
                goUp = false;
            }
            if (goLeft && Canvas.GetLeft(pacman) - 10 < 1)
            {
                noLeft = true;
                goLeft = false;
            }
            if(goRight&&Canvas.GetLeft(pacman) + 70 > Application.Current.MainWindow.Width)
            {
                noRight = true;
                goRight = false;
            }
            pacmanHitBox = new Rect(Canvas.GetLeft(pacman), Canvas.GetTop(pacman), pacman.Width, pacman.Height);
            foreach(var x in MyCanvas.Children.OfType<Rectangle>())
            {
                Rect hitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                if ((string)x.Tag == "duvar")
                {
                    if (goLeft == true && pacmanHitBox.IntersectsWith((hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) + 10);
                        noLeft = true;
                        goLeft = false;
                    }
                    if (goRight == true && pacmanHitBox.IntersectsWith((hitBox))
                    {
                        Canvas.SetLeft(pacman, Canvas.GetLeft(pacman) - 10);
                        noRight = true;
                        goRight = false;
                    }
                    if (goDown == true && pacmanHitBox.IntersectsWith((hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                        noDown = true;
                        goDown = false;
                    }
                    if (goUp == true && pacmanHitBox.IntersectsWith((hitBox))
                    {
                        Canvas.SetTop(pacman, Canvas.GetTop(pacman) - 10);
                        noUp = true;
                        goUp= false;
                    }


                }
                if ((string)x.Tag == "coin")
                {
                    if (pacmanHitBox.IntersectsWith(hitBox) && x.Visibility.Visible)
                    {
                        x.Visibility = Visibility.Hidden;
                        score  ++;
                    }
                }
                if ((string)x.Tag == "hayalet")
                {
                    if (pacmanHitBox.IntersectsWith(hitBox))
                    {
                        GameOver("Hayaletler Seni Yakladı ,tekrar oynamak için tamam butonuna tıkla ");
                    }
                    {
                        if(x.Name.ToString()=="orangeDusman")
                        {
                            Canvas.SetLeft(x, Canvas.GetLeft(x) - hayaletSpeed);
                           
                        }
                        else
                        {
                            Canvas.SetLeft(x, Canvas.GetLeft(x) - hayaletSpeed);
                        }
                        currentHayaletStep --;
                        if (currentHayaletStep < 1)
                        {
                            currentHayaletStep = hayaletMoveStop;
                            hayaletSpeed =- hayaletSpeed
                        }

                    }
                }
                if(score == 140)
                {
                    GameOver("Kazandın,Bütün Coinleri Topladınızzzz");
                }
            }

        }

        private void GameOver()
        {
            //140
            gametimer.Stop();
            MessageBox.Show(message, "Pac Man Game ");
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
