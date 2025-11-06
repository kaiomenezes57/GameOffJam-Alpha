using System.Collections.Generic;
using Game.Core.PhoneNotepad;
using System.Linq;
using VContainer;

namespace Game.Domains.PhoneNotepad
{
    public sealed class PhoneNotepadManager : IPhoneNotepadManager
    {
        [Inject] private readonly IPhoneNotepadView _phoneNotepadView;
        private readonly List<PhoneNotepadTaskData> _currentTasks = new();

        public void SetTasks(params PhoneNotepadTaskData[] notepadTask)
        {
            foreach (var task in notepadTask)
            {
                if (task.IsValid() && !_currentTasks.Contains(task))
                    _currentTasks.Add(task);
            }

            RefreshVisual();
        }

        public void CompleteTask(IPhoneNotepadTaskCompleteTrigger taskCompleteTrigger)
        {
            var task = _currentTasks
                .FirstOrDefault(t => t.TaskCompleteTrigger.GetType() == taskCompleteTrigger.GetType());
            
            if (task.Equals(default(PhoneNotepadTaskData)) || 
                !task.IsValid())
                return;

            task.SetCompleted();
            RefreshVisual();
        }

        private void RefreshVisual()
        {
            string[] toDoList = _currentTasks
                .Where(task => task.IsValid() && !task.IsCompleted)
                .Select(task => task.Task.GetLocalizedString())
                .ToArray();
            _phoneNotepadView.Refresh(toDoList);
        }
    }
}
