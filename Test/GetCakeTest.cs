using DAL;
using Persistence;
using Xunit;

namespace Test
{
    public class GetCakeTest    
    {
        CakesDAL cakeDal = new CakesDAL();

        [Fact]
        public void GetByIdTest1()
        {
            int keySearch = 1;
            Cake result = cakeDal.GetCakeByIdTest(keySearch);
            Assert.True(result != null);
        }

        [Fact]
        public void GetByIdTest2()
        {
            int keySearch = 2;
            Cake result = cakeDal.GetCakeByIdTest(keySearch);
            Assert.True(result != null);
        }

        [Fact]
        public void GetByIdTest3()
        {
            int keySearch = 3;
            Cake result = cakeDal.GetCakeByIdTest(keySearch);
            Assert.True(result != null);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2.5)]
        public void GetByIdTest4(int keySearch)
        {
            Cake result = cakeDal.GetCakeByIdTest(keySearch);
            Assert.True(result == null);
        }
    }
}