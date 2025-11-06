namespace Game.Core.PhoneNotepad
{
    public interface IPhoneNotepadManager
    {
        void SetTasks(params PhoneNotepadTaskData[] notepadDatas);
        void CompleteTask(IPhoneNotepadTaskCompleteTrigger taskCompleteTrigger);
    }
}
