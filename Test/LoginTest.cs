using DAL;
using Persistence;
using Xunit;

namespace Test
{
    public class LoginTest
    {
        private StaffDAL staffDal = new StaffDAL();
        private Staff staff = new Staff();

        [Fact]
        public void LoginTest1()
        {

            staff.userName = "hung";
            staff.password = "1";

            Staff result = staffDal.Login(staff);
            Assert.True(result != null);
            Assert.True(result!.staffRole == 1);
        }

        [Theory]
        [InlineData("hung","123")]
        [InlineData("hung","Pf1301")]
        [InlineData("sadasf","Pffasfas1301")]


        public void LoginTest2(string userName , string password)
        {   
            staff.userName = userName;
            staff.password = password;
            Staff result = staffDal.Login(staff);
            Assert.True(result == null);
        }
    }
}

