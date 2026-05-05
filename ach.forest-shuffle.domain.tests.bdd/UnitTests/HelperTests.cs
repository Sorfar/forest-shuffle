using Ach.Forest_Shuffle.Domain.Tests.Bdd.Support.Resources;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ach.Forest_Shuffle.Domain.Tests.Bdd.UnitTests
{
    public class HelperTests
    {
        [Fact]
        public void Test()
        {
            var test = LivingOrganismsNameHelper.LivingOrganismsFrToTypeDictionnary
                .FirstOrDefault(e => e.Key.Equals("pic épeiche", StringComparison.CurrentCultureIgnoreCase)).Value;

            Assert.NotEmpty(test);
        }
    }
}
