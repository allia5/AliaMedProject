﻿
using DTO;
using FakeItEasy;
using FluentAssertions;
using Server.Controllers;
using Server.Services.Foundation.cityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.ControllersTestUnit
{
    public class ControllerTestUnit
    {
        public readonly ICityService cityService;
        public ControllerTestUnit() { 
            this.cityService = A.Fake<ICityService>();
        }
        [Fact]
        public async void TestCity()
        {
         
        }

    }
}
