using System.IO;

namespace DAL
{
    public class EntityContext
    {
        public List<Person> Entities { get; set; } = new List<Person>();
        public string FilePath {get; set; }
        public string FileName { get; set; }
        private string? fileExtension;
        private string? filePathFULL;
        private IDataProvider<List<Person>>? dataProvider;
        
        public EntityContext(string fileName, string filePath)
        {
            FileName = fileName;
            FilePath = filePath;
        }
        public void SetDataProvider (IDataProvider<List<Person>> dataProvider)
        {
            this.dataProvider = dataProvider;
            fileExtension = dataProvider.GetExtension();
            filePathFULL = FilePath + FileName + fileExtension;
        }
        public void GetDataFromFile()
        {
            if (dataProvider == null) { throw new Exception("Set a type of data provider first!"); }
            if (!new FileInfo(filePathFULL).Exists || new FileInfo(filePathFULL).Length == 0) return;
            Entities = dataProvider.Deserialize(filePathFULL);
        }
        public void AddIntoFile(Person obj)
        {
            if (dataProvider == null) { throw new Exception("Set a type of data provider first!"); }
            Entities.Add(obj);
            UpdateFile();
        }
        public void RemoveFromFile(Person obj)
        {
            if (dataProvider == null) { throw new Exception("Set a type of data provider first!"); }
            if (Entities.Remove(obj) == true) UpdateFile();
        }
        private void UpdateFile()
        {
            dataProvider.Serialize(Entities, filePathFULL);        
        }
        private void ClearFile()
        {
            FileStream fs = new FileStream(FilePath, FileMode.Truncate, FileAccess.Write);
            fs.Close();
        }
    }
}