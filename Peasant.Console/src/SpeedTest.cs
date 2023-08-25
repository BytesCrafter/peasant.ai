

public class Result {
    public float size = 1000000; //1gb
    public float duration = 5; //5s
    public int speed = 0; //kbps
}

public class SpeedTest {
    public static void Start() {
        const string tempfile = "tempfile.tmp";
        System.Net.WebClient webClient = new System.Net.WebClient();

        Console.WriteLine("Ongoing Speedtest....");

        System.Diagnostics.Stopwatch sw = System.Diagnostics.Stopwatch.StartNew();
        webClient.DownloadFile("http://speedtest.ftp.otenet.gr/files/test10Mb.db", tempfile);
        sw.Stop();

        System.IO.FileInfo fileInfo = new System.IO.FileInfo(tempfile);
        long speed = fileInfo.Length / sw.Elapsed.Seconds / 1000 / 1000;

        Console.WriteLine("Download duration: {0} Seconds", sw.Elapsed.Seconds);
        Console.WriteLine("File size: {0} MB", (fileInfo.Length/1000/1000).ToString("N0"));
        Console.WriteLine("Speed: {0} Mbps ", speed.ToString("N0"));

        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();
    }
}