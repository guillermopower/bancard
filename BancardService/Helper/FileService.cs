namespace Bancard.Core.Helper
{
    public class FileService
    {

        public FileService() { }
        public void write(string text, string fullPath)
        {
            using (StreamWriter writer = new StreamWriter(fullPath))
            {
                writer.WriteLine(text);
            }  
        }

        public string read(string fullPath)
        {
            // Read a file
            string readText = File.ReadAllText(fullPath);
            return readText;
        }
    }
}
