using Xunit;

namespace Common.Tests
{
    public class CountryManagerTests
    {
        [Fact]
        public void HasIso2_KeyIsNull_ReturnFalse()
        {
            Assert.False(CountryManager.HasIso2(null));
        }

        [Fact]
        public void HasIso3_KeyIsNull_ReturnFalse()
        {
            Assert.False(CountryManager.HasIso3(null));
        }

        [Fact]
        public void Iso3ToIso2_KeyIsNull_ReturnEmptyString()
        {
            Assert.Empty(CountryManager.Iso3ToIso2(null));
        }

        [Fact]
        public void Iso2ToIso3_KeyIsNull_ReturnEmptyString()
        {
            Assert.Empty(CountryManager.Iso2ToIso3(null));
        }

        [Fact]
        public void GetCountryNameByIso3_KeyIsNull_ReturnEmptyString()
        {
            Assert.Empty(CountryManager.GetCountryNameByIso3(null));
        }

        [Fact]
        public void GetCountryNameByIso2_KeyIsNull_ReturnEmptyString()
        {
            Assert.Empty(CountryManager.GetCountryNameByIso2(null));
        }

        [Fact]
        public void HasIso3_ValidIso2_ReturnFalse()
        {
            Assert.False(CountryManager.HasIso3("RU"));
        }

        [Fact]
        public void HasIso3_ValidIso3_ReturnTrue()
        {
            Assert.True(CountryManager.HasIso3("RUS"));
        }
        
        [Fact]
        public void HasIso3_InvalidIso3_ReturnFalse()
        {
            Assert.False(CountryManager.HasIso3("InvalidIso"));
        }

        [Fact]
        public void HasIso2_ValidIso3_ReturnFalse()
        {
            Assert.False(CountryManager.HasIso2("RUS"));
        }        

        [Fact]
        public void HasIso2_ValidIso2_ReturnTrue()
        {
            Assert.True(CountryManager.HasIso2("RU"));
        }

        [Fact]
        public void HasIso2_InvalidIso3_ReturnFalse()
        {
            Assert.False(CountryManager.HasIso2("InvalidIso"));
        }

        [Fact]
        public void Iso3ToIso2_ValidIso3_ReturnIso2()
        {
            // Arrange
            const string inputIso3 = "RUS";
            const string expectedIso2 = "RU";

            // Assert
            Assert.Equal(expectedIso2, CountryManager.Iso3ToIso2(inputIso3));
        }

        [Fact]
        public void Iso3ToIso2_InvalidIso3_ReturnEmptyString()
        {
            // Arrange
            const string inputIso3 = "InvalidIso";
            const string expectedIso2 = "";

            // Assert
            Assert.Equal(expectedIso2, CountryManager.Iso3ToIso2(inputIso3));
        }

        [Fact]
        public void Iso3ToIso2_ValidIso2_ReturnEmptyString()
        {
            // Arrange
            const string inputIso3 = "RU";
            const string expectedIso2 = "";

            // Assert
            Assert.Equal(expectedIso2, CountryManager.Iso3ToIso2(inputIso3));
        }

        [Fact]
        public void Iso2ToIso3_ValidIso2_ReturnIso3()
        {
            // Arrange
            const string inputIso2 = "RU";
            const string expectedIso3 = "RUS";

            // Assert
            Assert.Equal(expectedIso3, CountryManager.Iso2ToIso3(inputIso2));
        }

        [Fact]
        public void Iso2ToIso3_InvalidIso2_ReturnEmptyString()
        {
            // Arrange
            const string inputIso2 = "AA";
            const string expectedIso3 = "";

            // Assert
            Assert.Equal(expectedIso3, CountryManager.Iso2ToIso3(inputIso2));
        }        
        
        [Fact]
        public void Iso2ToIso3_ValidIso3_ReturnEmptyString()
        {
            // Arrange
            const string inputIso2 = "RUS";
            const string expectedIso3 = "";

            // Assert
            Assert.Equal(expectedIso3, CountryManager.Iso2ToIso3(inputIso2));
        }

        [Fact]
        public void GetCountryNameByIso3_ValidIso3_ReturnCountryName()
        {
            // Arrange
            const string inputIso3 = "RUS";
            const string expectedCountryName = "Russia";

            // Assert
            Assert.Equal(expectedCountryName, CountryManager.GetCountryNameByIso3(inputIso3));
        }      
        
        [Fact]
        public void GetCountryNameByIso3_InvalidIso3_ReturnEmptyString()
        {
            // Arrange
            const string inputIso3 = "AAA";
            const string expectedCountryName = "";

            // Assert
            Assert.Equal(expectedCountryName, CountryManager.GetCountryNameByIso3(inputIso3));
        }
        
        [Fact]
        public void GetCountryNameByIso2_ValidIso2_ReturnCountryName()
        {
            // Arrange
            const string inputIso2 = "RU";
            const string expectedCountryName = "Russia";

            // Assert
            Assert.Equal(expectedCountryName, CountryManager.GetCountryNameByIso2(inputIso2));
        }        
        
        [Fact]
        public void GetCountryNameByIso2_InvalidIso2_ReturnEmptyString()
        {
            // Arrange
            const string inputIso2 = "AA";
            const string expectedCountryName = "";

            // Assert
            Assert.Equal(expectedCountryName, CountryManager.GetCountryNameByIso2(inputIso2));
        }

        [Fact]
        public void GetIso2ByName_ValidName()
        {
            // Arrange
            const string name = "Russia";
            const string expectedIso2 = "RU";

            // Assert
            Assert.Equal(expectedIso2, CountryManager.GetIso2ByName(name));
        }

        [Fact]
        public void GetIso2ByName_InvalidValidName()
        {
            // Arrange
            const string name = "XXX";
            const string expectedIso2 = "";

            // Assert
            Assert.Equal(expectedIso2, CountryManager.GetIso2ByName(name));
        }

        [Fact]
        public void GetIso3ByName_ValidName()
        {
            // Arrange
            const string name = "Russia";
            const string expectedIso2 = "RUS";

            // Assert
            Assert.Equal(expectedIso2, CountryManager.GetIso3ByName(name));
        }

        [Fact]
        public void GetIso3ByName_InvalidValidName()
        {
            // Arrange
            const string name = "XXX";
            const string expectedIso2 = "";

            // Assert
            Assert.Equal(expectedIso2, CountryManager.GetIso3ByName(name));
        }
    }
}
