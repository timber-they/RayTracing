using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using RayTracing.Types;
using RayTracing.Types.Objects;
using RayTracing.Types.Objects.Cuboical;
using RayTracing.Types.Objects.Interfaces;
using RayTracing.Types.Observation;
using RayTracing.Types.Properties;

using Visualisation.Properties;


namespace Visualisation
{
    public sealed class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel () => LoadedCommand = new LoadedCommand (this);

        public async void GenerateBitmap ()
        {
            var observatorPosition = new Vector (0, 0, 2);
            var observator = new Observator (observatorPosition,
                                             new Frame (observatorPosition, 1, new Vector (0, 1, 0), 2, 1));
            var matrix = new Matrix (observator, new List <IObject>
                {
                    new Sphere (new Surface (0.8, new Colour (0x0, 0xA0, 0x8)), 2,
                                new Vector (0, 10, 2)),
                    new Sphere (new Surface (0.8, new Colour (0xA0, 0x8, 0x0)), 1,
                                new Vector (-4, 10, 2)),
                    new Plain (new Surface (0.5, new Colour (0x42, 0x42, 0x42)),
                               new Vector (-10, 30, 0),
                               new Vector (10, 30, 0), new Vector (10, 0, 0),
                               new Vector (-10, 0, 0)),
                    //new Cube (new Plain (new Surface (0.8, new Colour (0x0, 0x80, 0xFF)),
                    //                     new Vector (3.3, 7.6, 3), new Vector (3.6, 7.6, 3),
                    //                     new Vector (3.6, 7.3, 3),
                    //                     new Vector (3.3, 7.3, 3))),
                    //new Plain (new Surface (0.8, new Colour (0x0, 0x0, 0xF0)),
                    //           new Vector (3.3, 7.6, 3), new Vector (3.3, 7.3, 3), new Vector (3.3, 7.3, 3.3),
                    //           new Vector (3.3, 7.6, 3.3))
                    //new CubicalLightSource (
                    //    new Plain (new Surface (0, new Colour (0xFF, 0xFF, 0xFF)),
                    //               new Vector (4, 4, 9), new Vector (6, 4, 9),
                    //               new Vector (6, 2, 9),
                    //               new Vector (4, 2, 9)), 200),
                    //new SphericalLightSource (1, new Vector (5, 3, 10), new Colour (0xFF, 0xFF, 0xFF), 200),
                }.Concat (
                    CubicalLightSource.GenerateLightSources (
                        new Plain (new Surface (0, new Colour (0xFF, 0xFF, 0xFF)),
                                   new Vector (4, 4, 9), new Vector (6, 4, 9),
                                   new Vector (6, 2, 9),
                                   new Vector (4, 2, 9)), 4, 200)).ToArray ()
            );

            var width = 1000;
            ProgressMaximum = width * width / 2;
            await Task.Run (() =>
            {
                var bmp = matrix.GenerateBitmap (3, width, i => Progress = i);

                using (var memory = new MemoryStream ())
                {
                    bmp.Save (memory, System.Drawing.Imaging.ImageFormat.Bmp);
                    memory.Position = 0;
                    var bitmapimage = new BitmapImage ();
                    bitmapimage.BeginInit ();
                    bitmapimage.StreamSource = memory;
                    bitmapimage.CacheOption  = BitmapCacheOption.OnLoad;
                    bitmapimage.EndInit ();
                    bitmapimage.Freeze ();

                    BitmapImage = bitmapimage;
                }

                bmp.Save (@"C:\Users\Meta Colon\Desktop\Rendered.bmp");
            });
        }

        private BitmapImage _bitmapImage;
        private int         _progressMaximum;
        private int         _progress;

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

        public int Progress
        {
            get => _progress;
            set
            {
                if (Equals (value, _progress))
                    return;
                _progress = value;
                OnPropertyChanged ();
            }
        }

        public int ProgressMaximum
        {
            get => _progressMaximum;
            set
            {
                if (Equals (value, _progressMaximum))
                    return;
                _progressMaximum = value;
                OnPropertyChanged ();
            }
        }

        public LoadedCommand LoadedCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged ([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke (this, new PropertyChangedEventArgs (propertyName));
    }
}