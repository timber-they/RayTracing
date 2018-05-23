using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

using RayTracing.Types;
using RayTracing.Types.Objects;
using RayTracing.Types.Objects.Interfaces;
using RayTracing.Types.Observation;
using RayTracing.Types.Properties;

using Visualisation.Annotations;


namespace Visualisation
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel ()
        {
            var observatorPosition = new Vector (0, 0, 2);
            var observator = new Observator (observatorPosition,
                                             new Frame (observatorPosition, 1, new Vector (0, 1, 0), 2, 1));
            var matrix = new Matrix (
                new List <IObject>
                {
                    new Sphere (2, new Vector (0, 10, 2), new Surface (0.2, new Colour (0x40, 0x40, 0x40))),
                    new Sphere (1, new Vector (-4, 10, 2), new Surface (0.2, new Colour (0x40, 0x40, 0x40)))
                },
                new List <ILightSource>
                {
                    new SphericalLightSource (1, new Vector (5, 3, 10), new Colour (0xFF, 0xFF, 0xFF), 70)
                },
                observator);

            var bmp = matrix.GenerateBitmap (3, 2000);


            using (var memory = new MemoryStream ())
            {
                bmp.Save (memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                var bitmapimage = new BitmapImage ();
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
        private void OnPropertyChanged ([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
    }
}