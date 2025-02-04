using System.Runtime.CompilerServices;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JuegosCartas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        private List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        private TextBlock firstClicked = null;
        private TextBlock secondClicked = null;
        public MainWindow()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            // The Grid has 16 labels,
            // and the icon list has 16 icons,
            // so an icon is pulled at random from the list
            // and added to each label
            foreach (DockPanel dockPanel in LayoutPrincipal.Children)
            {
                TextBlock iconTextBlock = dockPanel.Children[0] as TextBlock;
                if (iconTextBlock != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconTextBlock.Text = icons[randomNumber];
                    iconTextBlock.Foreground = iconTextBlock.Background;
                    icons.RemoveAt(randomNumber);
                }
            }
        }
        private void Celda_Click(object sender, MouseButtonEventArgs e)
        {
            TextBlock clickedLabel = sender as TextBlock;

            if (clickedLabel != null)
            {
                if (clickedLabel.Foreground.Equals(clickedLabel.Background))
                {
                    if (firstClicked == null) //volterar y guardar
                    {
                        firstClicked = clickedLabel;
                        //si esta volteada lo ponemos en negro
                        firstClicked.Foreground = new SolidColorBrush(Colors.Black);
                        return;
                    }
                    if (secondClicked == null && clickedLabel != firstClicked) //volterar y comparar
                    {
                        secondClicked = clickedLabel;
                        //si esta volteada lo pones en negro
                        secondClicked.Foreground = new SolidColorBrush(Colors.Black);

                        if (secondClicked == firstClicked)
                        {


                        }
                    }

                }

            }
        }
    }
}