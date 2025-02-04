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
using System.Windows.Threading;
using System.Diagnostics;

namespace JuegosCartas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TextBlock firstClicked = null;
        private TextBlock secondClicked = null;
        private DispatcherTimer timer = new DispatcherTimer();
        
        Random random = new Random();
        private List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };
        
        public MainWindow()
        {
            InitializeComponent();
            timer.Interval = System.TimeSpan.FromMilliseconds(750);
            timer.Tick += timer_Tick;
            
            AssignIconsToSquares(); 
            //timer.Start();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
         
            timer.Stop();
            
            if (firstClicked != null && secondClicked != null)
            {
                firstClicked.Foreground = firstClicked.Background;
                secondClicked.Foreground = secondClicked.Background;
                firstClicked = null;
                secondClicked = null;
            }

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
            // The timer is only on after two non-matching 
            // icons have been shown to the player, 
            // so ignore any clicks if the timer is running
            if (timer.IsEnabled == true)
                return;

            DockPanel clickedLabel = sender as DockPanel;

            if (clickedLabel != null)
            {
                TextBlock iconTextBlock = clickedLabel.Children[0] as TextBlock; ;

                if(iconTextBlock != null)
                {
                    if(iconTextBlock.Foreground == Brushes.Black)
                        return;

                    if (firstClicked == null) //volterar y guardar
                    {
                        firstClicked = iconTextBlock;
                        //si esta volteada lo ponemos en negro
                        firstClicked.Foreground = Brushes.Black;
                        return;
                    }
                    // If the player gets this far, the timer isn't
                    // running and firstClicked isn't null,
                    // so this must be the second icon the player clicked
                    // Set its color to black
                    secondClicked = iconTextBlock;
                    secondClicked.Foreground = Brushes.Black;

                    // Check to see if the player won
                    CheckForWinner();

                    // If the player clicked two matching icons, keep them 
                    // black and reset firstClicked and secondClicked 
                    // so the player can click another icon
                    if (firstClicked.Text == secondClicked.Text)
                    {
                        firstClicked = null;
                        secondClicked = null;
                        return;
                    }
                    timer.Start();
                    
                }
               
            }
        }
        private void CheckForWinner()
        {
            // Go through all of the labels in the TableLayoutPanel, 
            // checking each one to see if its icon is matched
            foreach (DockPanel dockPanel in LayoutPrincipal.Children)
            {
                TextBlock textBlock = dockPanel.Children[0] as TextBlock;

                if (textBlock != null)
                {
                    if (textBlock.Foreground == textBlock.Background)
                        return;
                }
            }

            // If the loop didn’t return, it didn't find
            // any unmatched icons
            // That means the user won. Show a message and close the form
            MessageBox.Show("You matched all the icons!", "Congratulations");
            Close();
        }
    }
}