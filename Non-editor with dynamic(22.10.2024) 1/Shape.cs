using System.ComponentModel;
using System.Windows.Media;

namespace GraphicsEditor
{
    public class Shape : INotifyPropertyChanged
    {
        private double _width;
        private double _height;
        private SolidColorBrush _color;
        private double _x;
        private double _y;
        private double _x1;
        private double _y1;
        private double _x2;
        private double _y2;
        private bool _isLineVisible;

        public double Width
        {
            get => _width;
            set { _width = value; OnPropertyChanged(nameof(Width)); }
        }

        public double Height
        {
            get => _height;
            set { _height = value; OnPropertyChanged(nameof(Height)); }
        }

        public SolidColorBrush Color
        {
            get => _color;
            set { _color = value; OnPropertyChanged(nameof(Color)); }
        }

        public double X
        {
            get => _x;
            set { _x = value; OnPropertyChanged(nameof(X)); }
        }

        public double Y
        {
            get => _y;
            set { _y = value; OnPropertyChanged(nameof(Y)); }
        }

        public double X1
        {
            get => _x1;
            set { _x1 = value; OnPropertyChanged(nameof(X1)); }
        }

        public double Y1
        {
            get => _y1;
            set { _y1 = value; OnPropertyChanged(nameof(Y1)); }
        }

        public double X2
        {
            get => _x2;
            set { _x2 = value; OnPropertyChanged(nameof(X2)); }
        }

        public double Y2
        {
            get => _y2;
            set { _y2 = value; OnPropertyChanged(nameof(Y2)); }
        }

        public bool IsLineVisible
        {
            get => _isLineVisible;
            set { _isLineVisible = value; OnPropertyChanged(nameof(IsLineVisible)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
