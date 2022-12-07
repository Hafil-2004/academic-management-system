﻿using AcademicManagementSystem.Context.AmsModels;

namespace AcademicManagementSystemTest.MockData;

public static class SessionMockData
{
    public static readonly List<Session> Sessions = new List<Session>()
    {
        new Session()
        {
            Id = 1,
            ClassScheduleId = 1,
            SessionTypeId = 1,
            RoomId = 1,
            Title = "Session 1",
            LearningDate = new DateTime(2022, 01, 03),
            StartTime = new TimeSpan(8,0,0),
            EndTime = new TimeSpan(12,0,0)
        },
        new Session()
        {
            Id = 1,
            ClassScheduleId = 1,
            SessionTypeId = 1,
            RoomId = 1,
            Title = "Session 2",
            LearningDate = new DateTime(2022, 01, 05),
            StartTime = new TimeSpan(8,0,0),
            EndTime = new TimeSpan(12,0,0)
        },
        new Session()
        {
            Id = 1,
            ClassScheduleId = 1,
            SessionTypeId = 1,
            RoomId = 1,
            Title = "Session 3",
            LearningDate = new DateTime(2022, 01, 07),
            StartTime = new TimeSpan(8,0,0),
            EndTime = new TimeSpan(12,0,0)
        },
        new Session()
        {
            Id = 1,
            ClassScheduleId = 1,
            SessionTypeId = 1,
            RoomId = 1,
            Title = "Session 4",
            LearningDate = new DateTime(2022, 01, 10),
            StartTime = new TimeSpan(8,0,0),
            EndTime = new TimeSpan(12,0,0)
        },
        new Session()
        {
            Id = 1,
            ClassScheduleId = 1,
            SessionTypeId = 1,
            RoomId = 1,
            Title = "Session 5",
            LearningDate = new DateTime(2022, 01, 12),
            StartTime = new TimeSpan(8,0,0),
            EndTime = new TimeSpan(12,0,0)
        },
    };
}