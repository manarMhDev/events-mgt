

using Events.Contracts.Enums;

namespace Events.Contracts.Dtos
{
    public class ImagePath
    {
        public ImagePath(string rootPath, FolderType folderPath, string fileName)
        {
            this.folderPath = "wwwroot\\Uploads\\" + folderPath.ToString();
            string folderToCreate = rootPath + "\\" + this.folderPath;
            if (!Directory.Exists(folderToCreate))
            {
                Directory.CreateDirectory(folderToCreate);
            }
            physicalFolderPath = folderToCreate;
            physicalPath = folderToCreate + "\\" + fileName;
            absoluteUrl = "Uploads/" + folderPath + "/" + fileName;
            filename = fileName;
        }

        public string absoluteUrl { get; set; }
        public string physicalPath { get; set; }
        public string folderPath { get; set; }
        public string physicalFolderPath { get; set; }
        public string filename { get; set; }
        public string fullUrl { get; set; }
    }
}
