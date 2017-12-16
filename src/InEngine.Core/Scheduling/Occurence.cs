﻿using System;
using InEngine.Core.Scheduling.LifeCycle;
using Quartz;

namespace InEngine.Core.Scheduling
{
    public class Occurence
    {
        public Schedule Schedule { get; set; }
        public AbstractCommand Command { get; set; }
        public IJobDetail JobDetail { get; set; }

        public static TriggerBuilder MakeTriggerBuilder(AbstractCommand command)
        {
            if (command == null)
                throw new ArgumentNullException(command.GetType().Name, "The command to schedule cannot be null.");
            return TriggerBuilder
                .Create()
                .WithIdentity($"{command.Name}:job:{command.ScheduleId}", command.SchedulerGroup);
        }

        public ScheduleLifeCycleBuilder RegisterJob(Action<DailyTimeIntervalScheduleBuilder> action)
        {
            return new ScheduleLifeCycleBuilder { 
                JobRegistration = Schedule.RegisterJob(Command, JobDetail, MakeTriggerBuilder(Command).WithDailyTimeIntervalSchedule(action).Build()) 
            };
        }

        public ScheduleLifeCycleBuilder RegisterJob(string cronExpression)
        {
            return new ScheduleLifeCycleBuilder {
                JobRegistration = Schedule.RegisterJob(Command, JobDetail, MakeTriggerBuilder(Command).WithCronSchedule(cronExpression).Build())
            };
        }

        public ScheduleLifeCycleBuilder RegisterJob(Action<SimpleScheduleBuilder> action)
        {
            return new ScheduleLifeCycleBuilder {
                JobRegistration = Schedule.RegisterJob(Command, JobDetail, MakeTriggerBuilder(Command).WithSimpleSchedule(action).Build())
            };
        }

        public ScheduleLifeCycleBuilder Cron(string cronExpression)
        {
            return RegisterJob(cronExpression);
        }

        public ScheduleLifeCycleBuilder EverySecond()
        {
            return RegisterJob(x => x.WithIntervalInSeconds(1).RepeatForever());
        }

        public ScheduleLifeCycleBuilder EveryMinute()
        {
            return RegisterJob(x => x.WithIntervalInMinutes(1).RepeatForever());
        }

        public ScheduleLifeCycleBuilder EveryFiveMinutes()
        {
            return RegisterJob(x => x.WithIntervalInMinutes(5).RepeatForever());
        }

        public ScheduleLifeCycleBuilder EveryTenMinutes()
        {
            return RegisterJob(x => x.WithIntervalInMinutes(10).RepeatForever());
        }

        public ScheduleLifeCycleBuilder EveryFifteenMinutes()
        {
            return RegisterJob(x => x.WithIntervalInMinutes(15).RepeatForever());
        }

        public ScheduleLifeCycleBuilder EveryThirtyMinutes()
        {
            return RegisterJob(x => x.WithIntervalInMinutes(30).RepeatForever());
        }

        public ScheduleLifeCycleBuilder Hourly()
        {
            return RegisterJob(x => x.WithIntervalInHours(1).RepeatForever());
        }

        public ScheduleLifeCycleBuilder HourlyAt(int minutesAfterTheHour)
        {
            return RegisterJob($"0 {minutesAfterTheHour} * * * ?");
        }

        public ScheduleLifeCycleBuilder Daily()
        {
            return RegisterJob(x => x.WithIntervalInHours(24).RepeatForever());
        }

        public ScheduleLifeCycleBuilder DailyAt(int hours, int minutes, int seconds = 0)
        {
            return RegisterJob(x => x.StartingDailyAt(new TimeOfDay(hours, minutes, seconds)));
        }
    }
}
