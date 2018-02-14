﻿using System;
using System.Collections.Generic;
using System.Linq;
using SunCalc.Model;
using Xunit;

namespace SunCalc.Tests
{
    public class SunCalcTests
    {
        [Fact]
        public void Get_Sun_Position_Returns_Azimuth_And_Altitude_For_The_Given_Time_And_Location()
        {
            //Arrange
            var date = new DateTime(2013, 3, 5, 0, 0, 0, DateTimeKind.Utc);
            var lat = 50.5;
            var lng = 30.5;

            //Act
            var sunPosition = SunCalc.GetSunPosition(date, lat, lng);

            //Assert
            Assert.Equal(-2.5003175907168385, sunPosition.Azimuth, 15);
            Assert.Equal(-0.7000406838781611, sunPosition.Altitude, 15);
        }

        [Fact]
        public void Get_Moon_Position_Returns_Azimuth_Altitude_Distance_And_ParallacticAngle_For_The_Given_Time_And_Location()
        {
            //Arrange
            var date = new DateTime(2013, 3, 5, 0, 0, 0, DateTimeKind.Utc);
            var lat = 50.5;
            var lng = 30.5;

            //Act
            var sunPosition = SunCalc.GetMoonPosition(date, lat, lng);

            //Assert
            Assert.Equal(-0.9783999522438226, sunPosition.Azimuth, 15);
            Assert.Equal(0.0145514822438922, sunPosition.Altitude, 15);
            Assert.Equal(364121.37256256194, sunPosition.Distance, 15);
            Assert.Equal(-0.59832117604234014, sunPosition.ParallacticAngle, 15);
        }
        
        
        [Fact]
        public void Get_Sun_Phases_Returns_Sun_Phases_For_The_Given_Date_And_Location()
        {
            //Arrange
            var testData = new List<SunPhase>
            {
                new SunPhase(SunPhaseName.SolarNoon, new DateTime(2013, 3, 5, 10, 10, 57, DateTimeKind.Utc)), 
                new SunPhase(SunPhaseName.Nadir, new DateTime(2013, 3, 4, 22, 10, 57, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.Sunrise, new DateTime(2013, 3, 5, 4, 34, 56, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.Sunset, new DateTime(2013, 3, 5, 15, 46, 57, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.SunriseEnd, new DateTime(2013, 3, 5, 4, 38, 19, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.SunsetStart, new DateTime(2013, 3, 5, 15, 43, 34, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.Dawn, new DateTime(2013, 3, 5, 4, 2, 17, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.Dusk, new DateTime(2013, 3, 5, 16, 19, 36, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.NauticalDawn, new DateTime(2013, 3, 5, 3, 24, 31, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.NauticalDusk, new DateTime(2013, 3, 5, 16, 57, 22, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.NightEnd, new DateTime(2013, 3, 5, 2, 46, 17, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.Night, new DateTime(2013, 3, 5, 17, 35, 36, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.GoldenHourEnd, new DateTime(2013, 3, 5, 5, 19, 01, DateTimeKind.Utc)),
                new SunPhase(SunPhaseName.GoldenHour, new DateTime(2013, 3, 5, 15, 2, 52, DateTimeKind.Utc)),
            };
            
            var date = new DateTime(2013, 3, 5, 0, 0, 0, DateTimeKind.Utc);
            var lat = 50.5;
            var lng = 30.5;
            
            //Act
            var sunPosition = SunCalc.GetSunPhases(date, lat, lng).ToList();

            //Assert
            foreach (var testSunPhase in testData)
            {
                var sunPositionValue = sunPosition.First(x => x.Name.Value == testSunPhase.Name.Value);

                var testDataPhaseTime = testSunPhase.PhaseTime.ToString("yyyy-MM-dd hh:mm:ss");
                var sunPositionPhaseTime = sunPositionValue.PhaseTime.ToString("yyyy-MM-dd hh:mm:ss");
                Assert.Equal(testDataPhaseTime, sunPositionPhaseTime);
            }
        }
    }
}