using DAL;
namespace BLL.Tests
{
    public class TestDataProvider<T> : IDataProvider<T>
    {
        public T Data { get; set; }
        public string GetExtension() => ".test";
        public T Deserialize(string file)
        {
            return Data;
        }

        public void Serialize(T obj, string file)
        {
            Data = obj;
        }
    }
}
