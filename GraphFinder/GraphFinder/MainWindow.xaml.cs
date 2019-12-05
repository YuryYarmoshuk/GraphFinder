using GraphFinder.Entity;
using GraphFinder.Model;
using GraphFinder.View;
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

namespace GraphFinder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Graph graph;
        Painter painter;
        Player player;
        PlayerActivity playerActivity;
        int playerScore;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetUp();
        }

        private void GraphGenerate()
        {
            erText.Text = "";

            GraphGenerator graphGenerator = new GraphGenerator();
            int nodeCount = 5;
            int percent = 70;
            int width = 5;

            try
            {
                nodeCount = Int32.Parse(nodeCountTextBox.Text);
                percent = Int32.Parse(percentTextBox.Text);
                width = Int32.Parse(difficult.SelectedItem.ToString());
            }
            catch
            {
                erText.Text = "Только целые числа";
            }

            graphGenerator.Width = width;

            graph = graphGenerator.Generate(nodeCount);

            graph.Percent = percent;

            graph.RefactorGraph();

            nodeGrid.ItemsSource = graph.Nodes;
            edgeGrid.ItemsSource = graph.Edges;

            painter = new Painter(canvas1, graph);
            painter.Draw();

            canvas1.IsEnabled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            erText.Text = "";

            try
            {
                painter.Draw();
            }
            catch
            {
                erText.Text = "Сгенерируйте граф";
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            PathFind();
        }

        private void PathFind()
        {
            erText.Text = "";

            try
            {
                PathConstanse pathConstanse = new PathConstanse();
                PathFinder pathFinder = new PathFinder(pathConstanse, graph);
                pathFinder.Search();
                playerScore = pathConstanse.GetMinLength();
            }
            catch
            {
                erText.Text = "Сгенерируйте граф";
            }
        }

        private void Canvas1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Node nextNode = graph.GetNodeByCenter(GetPointOnClick(e));
            int minusScore = graph.GetEdgeWeight(player.CurrentNode, nextNode);
            
            if (minusScore != -1)
            {
                playerActivity.MovePlayerToNode(player, nextNode, minusScore);
                Refresh();
            }

            if (player.Score < 0)
            {
                ViewResult("Ты проиграл, попробуешь еще раз?");
            }
            else if (player.Score == 0 && player.CurrentNode.Identifier.Equals("e"))
            {
                ViewResult("Ты победил, попробуешь еще раз?");
            }
        }

        private void ViewResult(string msgText)
        {
            if (MessageBox.Show(msgText, "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                SetUp();
            }
            else
            {
                Close();
            }
        }

        private Point GetPointOnClick(MouseButtonEventArgs e)
        {
            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;

            double leftSide = canvas1.Margin.Left;
            double rightSide = leftSide + canvas1.RenderSize.Width;
            double topSide = canvas1.Margin.Top;
            double bottomSide = topSide + canvas1.RenderSize.Height;

            double deltaX = painter.GetCellWidth();
            double deltaY = painter.GetCellHeight();

            double countHorizontal = canvas1.RenderSize.Width / deltaX;
            double countVertical = canvas1.RenderSize.Height / deltaY;

            int cellCoordinateX = -1;
            int cellcoordinateY = -1;

            for (int i = 1; i <= countHorizontal; i++)
            {
                if (x > (leftSide + deltaX * (i - 1)) && x < (leftSide + deltaX * i))
                {
                    for (int j = 1; j <= countVertical; j++)
                    {
                        if (y > (topSide + deltaY * (j - 1)) && y < (bottomSide + deltaY * j))
                        {
                            cellCoordinateX = i;
                            cellcoordinateY = j;
                        }
                    }
                }
            }

            return new Point(cellCoordinateX, cellcoordinateY);
        }

        private void EdgeChecked_Click(object sender, RoutedEventArgs e)
        {
            if (edgeChecked.IsChecked == true)
            {
                painter.IsEdgeShowed = true;
            }
            else
            {
                painter.IsEdgeShowed = false;
            }

            Refresh();
        }

        public void CheatActive()
        {
            graphTab.Visibility = Visibility.Visible;
            edgeChecked.Visibility = Visibility.Visible;
        }

        public void CheatOff()
        {
            graphTab.Visibility = Visibility.Hidden;
            edgeChecked.Visibility = Visibility.Hidden;
            edgeChecked.IsChecked = false;
        }

        private void PlayerCreator(int score, Node startNode)
        {
            player = new Player(score, startNode);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            CheatInput cheatInput = new CheatInput(this);
            cheatInput.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StringBuilder rules = new StringBuilder();

            rules.Append("Правила игры:\n1) Пройти от точки S до E за выделенное количество очков" +
                "\n2) Переходы между точками отнимают у игрока определенное количество очков" +
                "\n3) Нельзя сразу перейти от S к E" +
                "\n4) При повторном прохождении карта перестраивается" +
                "\n5) Если стало легко можно увеличить сложность" +
                "\n6) Красным подсвечивается вершина, на которой сейчас игрок" +
                "\nУдачи!");

            MessageBox.Show(rules.ToString());
            difficult.SelectedIndex = 0;
            playerActivity = new PlayerActivity();
            SetUp();
        }

        private void SetUp()
        {
            GraphGenerate();
            PathFind();
            PlayerCreator(playerScore, graph.GetFirstNode());
            Refresh();
            CheatOff();
        }

        private void Refresh()
        {
            scoreLabel.Content = "Оставшиеся очки: " + player.Score;
            Button_Click_1(this, new RoutedEventArgs());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetUp();
        }
    }
}