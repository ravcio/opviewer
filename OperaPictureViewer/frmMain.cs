using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.IO;

namespace OperaPictureViewer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void cmdGo_Click(object sender, EventArgs e)
        {
            int w = 100;
            int h = 150;
            //            Bitmap bitmap = new Bitmap(100, 150, 100 * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, ptr);
            Bitmap bitmap = new Bitmap(w, h);

            TimeDiagnose t = new TimeDiagnose();
            t.Start();
            int repeat = 5000;
            for (int i = 0; i < repeat; i++)
                fillBitmap2(bitmap);
            double time = t.Stop(repeat);

            pictureBox.Image = bitmap;

            Graphics g = Graphics.FromImage(bitmap);
            g.DrawEllipse(new Pen(Color.Black), 30, 30, 20, 50);

            MessageBox.Show("time=" + time.ToString() + " ms");
        }



        private void cmdExit_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Save();
            
            Application.Exit();
        }

        void fillBitmap2(Bitmap bitmap)
        {
            // option 2
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), 
                ImageLockMode.ReadWrite, bitmap.PixelFormat);
            Debug.Assert(bitmap.PixelFormat == PixelFormat.Format32bppArgb);

            unsafe
            {
                byte* ptr = (byte*)(data.Scan0);
                for (int y = 0; y < data.Height; y++)
                {
                    for (int x = 0; x < data.Width; x++)
                    {
                        *(ptr + 0) = 0x00;  // b
                        *(ptr + 1) = 0x00;  // g
                        *(ptr + 2) = 0xff;  // r
                        *(ptr + 3) = 0xff;  // a
                        // write the logic implementation here
                        ptr += 4;
                    }
                    ptr += data.Stride - data.Width * 4;
                }
            }
            // Unlock the bits.
            bitmap.UnlockBits(data);
        }


        /// <summary>
        /// Reads data from a stream until the end is reached. The
        /// data is returned as a byte array. An IOException is
        /// thrown if any of the underlying IO calls fail.
        /// </summary>
        /// <param name="stream">The stream to read data from</param>
        /// <param name="initialLength">The initial buffer length</param>
        public static byte[] ReadFully(Stream stream, int initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }



    }
}