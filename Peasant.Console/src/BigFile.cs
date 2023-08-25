
using System.Reflection.Emit;

public class BigClass {
    public static void Create(string filename = "dummyfile", int gigabyte = 1) {
        FileStream fs = new FileStream(@"c:\temp\"+filename, FileMode.Create);
        fs.Seek(2048L * 1024 * 1024 * gigabyte, SeekOrigin.Begin);
        fs.WriteByte(0);
        fs.Close();
    }
}
