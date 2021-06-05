namespace NameSorter
{
    public interface IIOService
    {
        /// <summary>
        /// This method should read the specified file and return the content as a stirng array
        /// </summary>
        /// <param name="path">The path to the file</param>
        /// <returns>String array containing all contents of the specified file</returns>
        string[] ReadFromTextFile(string path);
        /// <summary>
        /// This method should write the provided content to a text file with a given file name
        /// </summary>
        /// <param name="fileName">The name of the file to be saved</param>
        /// <param name="content">The content to be written to the file</param>
        void WriteToTextFile(string fileName, string[] content);
    }
}
