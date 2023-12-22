using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BLL.Tests
{
    [TestClass()]
    public class EntityServiceTests
    {
        EntityService service = new(new TestDataProvider<List<Person>>(), "", "");
        [TestMethod()]
        public void GetEntitiesTest()
        {
            // Arrange
            List<Person> list = new() { new(), new (), new () };
            service.Add(list[0]);
            service.Add(list[1]);
            service.Add(list[2]);
            // Act
            List<Person> read = service.GetEntities();
            // Assert
            Assert.AreEqual(list[0], read[0]);
            Assert.AreEqual(list[1], read[1]);
            Assert.AreEqual(list[2], read[2]);
            // Clear traces
            service.Remove(list[0]);
            service.Remove(list[1]);
            service.Remove(list[2]);
        }

        [TestMethod()]
        public void AddTest()
        {
            // Arrange
            List<Person> list = new() { new(), new(), new() };
            // Act
            service.Add(list[0]);
            service.Add(list[1]);
            List<Person> read = service.GetEntities();
            // Assert
            Assert.AreEqual(2, read.Count);
            Assert.AreEqual(list[0], read[0]);
            Assert.AreEqual(list[1], read[1]);
            // Clear traces
            service.Remove(list[0]);
            service.Remove(list[1]);
        }

        [TestMethod()]
        public void RemoveTest()
        {
            // Arrange
            List<Person> list = new() { new(), new(), new() };
            service.Add(list[0]);
            service.Add(list[1]);
            // Act
            service.Remove(list[0]);
            List<Person> read = service.GetEntities();
            // Assert
            Assert.AreEqual(1, read.Count);
            Assert.AreEqual(list[1], read[0]);
            // Clear traces
            service.Remove(list[1]);
        }
    }
}