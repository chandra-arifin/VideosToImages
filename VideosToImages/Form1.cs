using MediaToolkit.Model;
using MediaToolkit.Options;
using MediaToolkit;
using MediaToolkit.Services;
using MediaToolkit.Tasks;

namespace VideosToImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            SaveGambar();
        }

        //[Obsolete]
        //private void SaveGambar()
        //{
        //    using (var engine = new Engine())
        //    {
        //        var mp4 = new MediaFile { Filename = mp4FilePath };

        //        engine.GetMetadata(mp4);

        //        var i = 0;
        //        while (i < mp4.Metadata.Duration.TotalSeconds)
        //        {
        //            var options = new ConversionOptions { Seek = TimeSpan.FromSeconds(i) };
        //            var outputFile = new MediaFile { Filename = string.Format("{0}\\image-{1}.jpeg", outputPath, i) };
        //            engine.GetThumbnail(mp4, outputFile, options);
        //            i++;
        //        }
        //    }
        //}

        private async Task SaveGambar()
        {
            string fileName = "Vid_10";
            string _tempFile = @$"D:\WCT\Videos\{fileName}.mp4";
            // _env is the injected IWebHostEnvironment
            // _tempPath is temporary file storage
            var ffmpegPath = Application.StartupPath + "ffmpeg.exe";//Path.Combine(_env.ContentRootPath, "<path-to-ffmpeg.exe>");

            var mediaToolkitService = MediaToolkitService.CreateInstance(ffmpegPath);
            var metadataTask = new FfTaskGetMetadata(_tempFile);
            var metadata = await mediaToolkitService.ExecuteAsync(metadataTask);

            var i = 0;
            while (i < metadata.Metadata.Streams.First().DurationTs)
            {
                var outputFile = string.Format("{0}\\image-{1:0000}.jpeg", Application.StartupPath + @$"\Test\{fileName}\", i);
                var thumbTask = new FfTaskSaveThumbnail(_tempFile, outputFile, TimeSpan.FromSeconds(i));
                _ = await mediaToolkitService.ExecuteAsync(thumbTask);
                i += 2;
            }
            MessageBox.Show("Selesai");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            
        }
    }
}