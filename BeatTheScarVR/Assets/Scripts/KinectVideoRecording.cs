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
    * @class KinectVideRecording
    * @brief Contains all the functionality surrounding the Kinect Sensor and converting images
     * to video format using ffmpeg video software.
    *
    * @author Geoff Hanson / MGU
    * @version 1
    * @date 10/11/17
    *
    */
public class KinectVideoRecording
{
    private KinectSensor ks; // Kinect Sensor object
    private ColorFrameReader cfr; // Color Frame Reader object
    private byte[] colorData; // Byte array containing color data
    private ColorImageFormat format; // Color Image Format object
    private Bitmap bmpSource; // Bitmap object
    private int imageSerial; 
    private List<Bitmap> bitmaps; // List containing Bitmaps
    private int count; // Counter
    private AVIWriter writer; // AVI Write object
    ImageCodecInfo myImageCodecInfo; // Image Codec Info object
    EncoderParameters myEncoderParameters; // Encoder Parameters object

    /**
    * @brief Default Constructor
     * Initialises the Kinect, Encoder and Recording functions.
     * 
    * @param
    * @return
    * @pre
    * @post
    */
    public KinectVideoRecording()
    {
        initKinect();
        initEncoder();
        record();
    }

    /**
    * @brief Initialises the Kinect
     * Initialises all the required Kinect Sensor variables to utilise the functionality
     * of the device.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
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

    /**
    * @brief Initialises the Encoder
     * Initialises all the required Encoder variables for image compression.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    private void initEncoder()
    {
        myImageCodecInfo = GetEncoderInfo("image/jpeg");
        Encoder myEncoderQ = Encoder.Quality;
        myEncoderParameters = new EncoderParameters(1);
        EncoderParameter myEncoderParameterQ = new EncoderParameter(myEncoderQ, 15L);
        myEncoderParameters.Param[0] = myEncoderParameterQ;
    }

    /**
    * @brief Records Frame
     * Called to record the frame to which the Kienct Sensor is viewing.
     * 
    * @param
    * @return void
    * @pre
    * @post
    */
    public void record()
    {
        cfr = ks.ColorFrameSource.OpenReader();
        cfr.FrameArrived += cfr_FrameArrived;
    }

    /**
    * @brief Converts Bitmap into .jpeg Image
     * Recieves the frame captured by the Kinect Sensor as BITMAP and saves as a .jpeg every 3 frames
     * to reduce game lag.
    * 
    * @param object sender
     * @param ColorFramArrivedEventArgs e
    * @return void
    * @pre
    * @post
    */
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

    /**
    * @brief Returns Image Codec Information
     * Finds the specific type of codec defined by the parameter given String mimeType
     * and returns it, else if nothing found returns null.
     * 
    * @param String mimeType
    * @return ImageCodecInfo
     * @return null
    * @pre
    * @post
    */
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

    /**
    * @brief Combines .jpeg files into .mp4 format
     * Takes a process id parameter as string pid including it in the .mp4 file location. Uses ffmpeg to compress and
     * convert .jpeg files into .mp4 video format and returns the URL (relative path) of the video.
    * 
    * @param string pid
    * @return string
    * @pre
    * @post
    */
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