using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

using RayTracing.Types;
using RayTracing.Types.Objects;
using RayTracing.Types.Observation;
using RayTracing.Types.Properties;

using Visualisation.Annotations;


namespace Visualisation
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel ()
        {
            var matrix = new Matrix (
                new List <Object> {new Sphere (20, new Vector (5, 40, 5), new Surface (0.5, new Colour (0xFF, 0, 0)))},
                new List <Vector> (),
                new Observator (new Vector (5, 0, 5), new Frame (new Vector (7.5, 5, 7.5), new Vector (2.5, 5, 2.5))));

            var bmp = matrix.GenerateBitmap (2, 1000, 1000);


            using (MemoryStream memory = new MemoryStream ())
            {
                bmp.Save (memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage ();
                bitmapimage.BeginInit ();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption  = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit ();

                BitmapImage = bitmapimage;
            }
        }

        private BitmapImage _bitmapImage;

        public BitmapImage BitmapImage
        {
            get => _bitmapImage;
            set
            {
                if (Equals (value, _bitmapImage))
                    return;
                _bitmapImage = value;
                OnPropertyChanged ();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
    }
}