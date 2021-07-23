using System;
using System.IO;

namespace WordUnscramblerProgram.Workers
{
    class FileReader
    {
        public string[] Read(string filename)
        {
            string[] fileContent;
            try
            {
                fileContent = File.ReadAllLines(filename);
                return fileContent;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return fileContent;

            
        }
    }
}
