using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Markup;
using System.Windows.Input;

namespace GraphicsEditor
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Shape> Shapes { get; set; }
        public Shape SelectedShape { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Shapes = new ObservableCollection<Shape>();
            DataContext = this;
        }

        private void AddCircle_Click(object sender, RoutedEventArgs e)
        {
            Shapes.Add(new Shape { Width = 50, Height = 50, Color = Brushes.Red, X = 100, Y = 100 });
        }

        private void AddRectangle_Click(object sender, RoutedEventArgs e)
        {
            Shapes.Add(new Shape { Width = 100, Height = 50, Color = Brushes.Blue, X = 200, Y = 200 });
        }

        private void AddLine_Click(object sender, RoutedEventArgs e)
        {
            Shapes.Add(new Shape { X1 = 300, Y1 = 300, X2 = 400, Y2 = 400, IsLineVisible = true, Color = Brushes.Black });
        }

        private void ColorComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedShape != null && ColorComboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string selectedColor = selectedItem.Tag.ToString();
                SelectedShape.Color = (SolidColorBrush)new BrushConverter().ConvertFromString(selectedColor);
            }
        }

        private void WidthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SelectedShape != null)
            {
                SelectedShape.Width = e.NewValue;
            }
        }

        private void HeightSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SelectedShape != null)
            {
                SelectedShape.Height = e.NewValue;
            }
        }

        private void XSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SelectedShape != null)
            {
                SelectedShape.X = e.NewValue;
            }
        }

        private void YSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (SelectedShape != null)
            {
                SelectedShape.Y = e.NewValue;
            }
        }

        private void DrawingCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point clickPosition = e.GetPosition(DrawingCanvas);

            foreach (var shape in Shapes)
            {
                if (IsPointInsideShape(clickPosition, shape))
                {
                    SelectedShape = shape;
                    UpdateControls();
                    return;
                }
            }

            Shapes.Add(new Shape
            {
                Width = 50,
                Height = 50,
                Color = Brushes.Red,
                X = clickPosition.X - 25,
                Y = clickPosition.Y - 25
            });
        }

        private bool IsPointInsideShape(Point point, Shape shape)
        {
            if (shape.Width > 0 && shape.Height > 0)
            {
                double centerX = shape.X + shape.Width / 2;
                double centerY = shape.Y + shape.Height / 2;
                double radius = shape.Width / 2; double distance = Math.Sqrt(Math.Pow(point.X - centerX, 2) + Math.Pow(point.Y - centerY, 2));
                return distance <= radius;
            }

            if (shape.X <= point.X && point.X <= shape.X + shape.Width &&
shape.Y <= point.Y && point.Y <= shape.Y + shape.Height)
            {
                return true;
            }

            return false;
        }

        private void Shape_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SelectedShape = (Shape)((FrameworkElement)sender).DataContext;
            UpdateControls();
        }

        private void UpdateControls()
        {
            if (SelectedShape != null)
            {
                ColorComboBox.SelectedItem = GetComboBoxItemByColor(SelectedShape.Color);
                WidthSlider.Value = SelectedShape.Width;
                HeightSlider.Value = SelectedShape.Height;
                XSlider.Value = SelectedShape.X;
                YSlider.Value = SelectedShape.Y;
            }
        }

        private ComboBoxItem GetComboBoxItemByColor(Brush color)
        {
            if (color == Brushes.Red) return (ComboBoxItem)ColorComboBox.Items[0];
            if (color == Brushes.Blue) return (ComboBoxItem)ColorComboBox.Items[1];
            if (color == Brushes.Green) return (ComboBoxItem)ColorComboBox.Items[2];
            return null;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var filePath = "shapes.xaml"; if (File.Exists(filePath))
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    var shapes = (ObservableCollection<Shape>)XamlReader.Load(stream);
                    Shapes.Clear();
                    foreach (var shape in shapes)
                    {
                        Shapes.Add(shape);
                    }
                }
            }
            else
            {
                MessageBox.Show("Файл не найден!");
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var filePath = "shapes.xaml"; using (var stream = new FileStream(filePath, FileMode.Create))
            {
                var xaml = XamlWriter.Save(Shapes);
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(xaml);
                }
            }
        }
    }
}
