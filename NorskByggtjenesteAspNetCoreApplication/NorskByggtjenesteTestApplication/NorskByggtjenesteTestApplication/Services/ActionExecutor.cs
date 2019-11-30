using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorskByggtjenesteTestApplication.Services
{
    public class ActionExecutor
    {
        private List<Action> ActionsToExecute { get; set; } = new List<Action>();

        public void AddActionToQueue(Action action)
        {
            this.ActionsToExecute.Add(action);
        }

        public Task Execute()
        {
            return Task.Run(() => ActionsToExecute.ForEach(action =>
            {
                Task.Run(() => action.Invoke()).Wait();
            }));
        }
    }
}
