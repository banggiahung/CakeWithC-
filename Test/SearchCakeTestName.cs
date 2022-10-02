using DAL;
using Persistence;
using Xunit;


namespace Test 
{

    public class SearchCakeTestName
    {
        CakesDAL cakeDal = new CakesDAL();

        [Fact]
        public void SearchByNameTest1()
        {
            List<Cake> list = new List<Cake>();
            string commandText = "Bánh 2 tầng";
            List<Cake> result = cakeDal.GetByNameList(list,commandText);
            Assert.True(result != null);
        }
        [Fact]
        public void SearchByNameTest2()
        {
            string commandText = "bánh đám cưới";
            List<Cake> list = new List<Cake>();
            List<Cake> result = cakeDal.GetByNameList(list,commandText);
            Assert.True(result != null);
        }

        [Fact]
        public void SearchByNameTest3()
        {
            string commandText = "bánh sinh nhật";
            List<Cake> list = new List<Cake>();
            List<Cake> result = cakeDal.GetByNameList(list,commandText);
            Assert.True(result != null);
        }

        [Theory]
        [InlineData("fasfas")]
        [InlineData("fsafqwre")]
        [InlineData("fwqrqwr")]
        public void SearchBookByBookNameTest4(string commandText)
        {
            List<Cake> list = new List<Cake>();
            List<Cake> result = cakeDal.GetByNameList(list,commandText);
            Assert.True(result.Count == 0);
        }
    
    }
}