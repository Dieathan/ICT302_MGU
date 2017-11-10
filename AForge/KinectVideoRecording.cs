using System.Collections;
using System.Collections.Generic;

using System.IO;
using System;
using System.Diagnostics;

using System.Drawing;
using System.Drawing.Imaging;

using AForge.Video.VFW;

using Windows.Kinect;

using UnityEngine;
using System.Runtime.InteropServices;

/**
 * 
 */
public class KinectVideoRecording
{

    private KinectSensor ks;
    private ColorFrameReader cfr;
    private byte[] colorData;
    private ColorImageFormat format;
    private Bitmap bmpSource;
    private int imageSerial;
    private List<Bitmap> bitmaps;
    private int count;
    private AVIWriter writer;
    ImageCodecInfo myImageCodecInfo;
    EncoderParameters myEncoderParameters;

    public KinectVideoRecording()
    {
        initKinect();
        initEncoder();
        record();
    }

    private void initKinect()
    {
        ks = KinectSensor.GetDefault();
        //ks.Open();
        var fd = ks.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Bgra);
        uint frameSize = fd.BytesPerPixel * fd.LengthInPixels;
        colorData = new byte[frameSize];
        format = ColorImageFormat.Bgra;
        imageSerial = 0;
        count = 0;
    }

    private void initEncoder()
    {
        myImageCodecInfo = GetEncoderInfo("image/jpeg");
        Encoder myEncoderQ = Encoder.Quality;
        myEncoderParameters = new EncoderParameters(1);
        EncoderParameter myEncoderParameterQ = new EncoderParameter(myEncoderQ, 15L);
        myEncoderParameters.Param[0] = myEncoderParameterQ;
    }

    public void record()
    {
        cfr = ks.ColorFrameSource.OpenReader();
        cfr.FrameArrived += cfr_FrameArrived;
    }

    private void cfr_FrameArrived(object sender, ColorFrameArrivedEventArgs e)
    {
        if (e.FrameReference == null) return;
        
        using (ColorFrame cf = e.FrameReference.AcquireFrame())
        {
            if (cf == null) return;

            cf.CopyConvertedFrameDataToArray(colorData, format);
            var fd = cf.FrameDescription;

            bmpSource = new Bitmap(fd.Width, fd.Height, PixelFormat.Format32bppRgb);
            BitmapData bmpData = bmpSource.LockBits(new Rectangle(0, 0, bmpSource.Width, bmpSource.Height), ImageLockMode.WriteOnly, bmpSource.PixelFormat);

            Marshal.Copy(colorData, 0, bmpData.Scan0, colorData.Length);
            bmpSource.UnlockBits(bmpData);

            if (count % 3 == 0)
            {
                bmpSource.Save("..\\ffmpeg\\Images\\img" + (imageSerial++) + ".jpeg", myImageCodecInfo, myEncoderParameters);
            }
        }

        count++;
    }

    private ImageCodecInfo GetEncoderInfo(String mimeType)
    {
        int j;
        ImageCodecInfo[] encoders;
        encoders = ImageCodecInfo.GetImageEncoders();
        for (j = 0; j < encoders.Length; ++j)
        {
            if (encoders[j].MimeType == mimeType)
                return encoders[j];
        }
        return null;
    }

    public string close(string pid)
    {
        string url;
        DateTime dt = DateTime.Now;

        url = dt.ToString("yyy-MM-dd HHmm");

        Process process = new Process();
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;
        startInfo.UseShellExecute = false;
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = "/C cd ..\\ffmpeg\\bin " +
            "& ffmpeg -y -r 8 -f image2 -s 1920x1080 -i ..\\Images\\img%d.jpeg -vcodec libx264 -crf 25 -pix_fmt yuv420p test.mp4 " +
            "& del /Q ..\\Images\\*" +
            "& copy test.mp4 \"..\\Videos\\" + pid + " " + url + ".mp4\" " +
            "& del /Q test.mp4";
        process.StartInfo = startInfo;
        process.Start();

        return url;
    }

}