using NorskByggtjenesteTestApplication.Services;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace NorskByggtjenesteTestApplication.Tests
{
    public class ActionExecutorTests
    {
        [Fact]
        public void ActionsMustBeExecutedInBackground()
        {
            var executer = new ActionExecutor();

            executer.AddActionToQueue(() =>
            {
                Thread.Sleep(Timeout.Infinite);
            });

            executer.Execute();

            Assert.True(true);
        }

        [Fact]
        public void ActionsMustBeExecutedSequentially()
        {
            StringBuilder stringBuilder = new StringBuilder();

            var executer = new ActionExecutor();
            executer.AddActionToQueue(() =>
            {
                stringBuilder.Append("My ");
            });
            executer.AddActionToQueue(() =>
            {
                stringBuilder.Append("name ");
            });
            executer.AddActionToQueue(() =>
            {
                stringBuilder.Append("is ");
            });
            executer.AddActionToQueue(() =>
            {
                stringBuilder.Append("Dmitriy");
            });

            var task = executer.Execute();
            task.GetAwaiter().GetResult();

            Assert.Equal("My name is Dmitriy", stringBuilder.ToString());
        }

        [Fact]
        public void ActionsCanBeAddedNotAtTheSameTime()
        {
            int sum = 0;

            var executer = new ActionExecutor();

            sum += 5;

            executer.AddActionToQueue(() =>
            {
                sum += 10;
            });

            sum += 5;

            executer.AddActionToQueue(() =>
            {
                sum += 10;
            });

            var task = executer.Execute();
            task.GetAwaiter().GetResult();

            Assert.Equal(30, sum);
        }

        [Fact]
        public void ActionsCanBeExecutedNotAtTheSameThread()
        {
            List<int> threadIds = new List<int>();

            var executer = new ActionExecutor();

            executer.AddActionToQueue(() =>
            {
                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
            });

            executer.AddActionToQueue(() =>
            {
                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
            });

            executer.AddActionToQueue(() =>
            {
                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
            });

            executer.AddActionToQueue(() =>
            {
                threadIds.Add(Thread.CurrentThread.ManagedThreadId);
            });

            var task = executer.Execute();
            task.GetAwaiter().GetResult();

            var uniqueItems = threadIds.Distinct().Count();

            Assert.True(uniqueItems >= 1);
        }
    }
}
