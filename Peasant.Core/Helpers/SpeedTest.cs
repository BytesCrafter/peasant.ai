
using System;
using System.IO;

namespace Peasant.Core.Helpers;

public class SpeedTest 
{
    public static void Start() 
    {
        const string tempfile = "tempfile.tmp";
        System.Net.WebClient webClient = new System.Net.WebClient();

        Console.WriteLine("Ongoing Speedtest....");

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
        webClient.DownloadFile("http://speedtest.ftp.otenet.gr/files/test10Mb.db", tempfile);
        sw.Stop();

        FileInfo fileInfo = new FileInfo(tempfile);
        long speed = fileInfo.Length / sw.Elapsed.Seconds / 1000 / 1000;

        Console.WriteLine("Download duration: {0} Seconds", sw.Elapsed.Seconds);
        Console.WriteLine("File size: {0} MB", (fileInfo.Length/1000/1000).ToString("N0"));
        Console.WriteLine("Speed: {0} Mbps ", speed.ToString("N0"));

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}