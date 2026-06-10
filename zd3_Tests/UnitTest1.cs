using zd3_biblioteka;
namespace zd3_Tests
{
    [TestClass]
    public class OperatorTests
    {
        [TestMethod]
        //формула Q базового класса (1-ый тест)
        public void TestBase()
        {
            Operator op = new Operator("Test", 2, 50, 1000, 2010);
            double result = op.Quality();
            Assert.AreEqual(2500, result, 0.001);
        }

        [TestMethod]
        //Qp при p = true (есть плата за подкл.)
        //Q = 2500; Qp = 0.7 * 2500 = 1750 (2-ой тест)
        public void Test_02()
        {
            OperatorPlus op = new OperatorPlus("Plus", 2, 50, 1000, 2010, true, 5, false);
            double result = op.Quality();
            Assert.AreEqual(1750, result, 0.001);
        }

        [TestMethod]
        //Qp при p = false (нет платы)
        //q = 2500, qp = 1.5 * 2500 = 3750 (3-ий тест)

        public void Test_03()
        {
            OperatorPlus op = new OperatorPlus("Plus", 2, 50, 1000, 2010, false, 0, true);
            double result = op.Quality();
            Assert.AreEqual(3750, result, 0.001);
        }

        [TestMethod]
        //тест по добавлению и удалению (4-ый тест)
        //на перегрузки все
        public void Test_04()
        {
            Operatorlist list = new Operatorlist();
            //перегрузка метода Add(параметры)
            list.Add("A", 2, 50, 100, 2000);
            //объект
            list.Add(new Operator("B", 4, 80, 200, 2005));

            Assert.AreEqual(2, list.Count());

            //перегрузка ремув (имя)
            bool removed = list.Remove("A");
            Assert.IsTrue(removed);
            Assert.AreEqual(1, list.Count());

            //удаление несуществующего
            bool removedAgain = list.Remove("A");
            Assert.IsFalse(removedAgain);
        }
    }
}