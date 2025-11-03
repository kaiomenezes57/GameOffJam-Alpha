using FMOD;
using FMOD.Studio;
using FMODUnity;
using Game.Core.Dialogue;
using System;
using System.Runtime.InteropServices;

namespace Game.Services.Audio
{
    public sealed class DialogueAudioService : IDialogueAudioService, IDisposable
    {
        private static DialogueAudioService _instance;
        private EventInstance _eventInstance;

        private TimelineInfo _timelineInfo;
        private GCHandle _timelineHandle;
        private EVENT_CALLBACK _dialogueCallback;

        public event Action OnLineEnd;

        public DialogueAudioService()
        {
            _instance = this;
        }

        public void Play(EventReference audioEvent)
        {
            Stop();

            _timelineInfo = new TimelineInfo();
            _timelineHandle = GCHandle.Alloc(_timelineInfo, GCHandleType.Pinned);
            _dialogueCallback = new EVENT_CALLBACK(FMODEventCallback);

            _eventInstance = RuntimeManager.CreateInstance(audioEvent);

            _eventInstance.setUserData(GCHandle.ToIntPtr(_timelineHandle));
            _eventInstance.setCallback(_dialogueCallback, EVENT_CALLBACK_TYPE.TIMELINE_MARKER);

            _eventInstance.start();
        }

        public void Stop()
        {
            if (!_eventInstance.isValid())
                return;

            _eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            _eventInstance.release();
            _timelineHandle.Free();
        }

        private static RESULT FMODEventCallback(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr parameterPtr)
        {
            var instance = new EventInstance(instancePtr);

            RESULT result = instance.getUserData(out IntPtr timelineInfoPtr);
            if (result != RESULT.OK) 
                return result;

            var timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            var timelineInfo = timelineHandle.Target as TimelineInfo;

            switch (type)
            {
                case EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    var markerProperties = (TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameterPtr, typeof(TIMELINE_MARKER_PROPERTIES));
                    timelineInfo.LastMarker = markerProperties.name;
                    
                    if (timelineInfo.LastMarker == "END")
                        _instance.OnLineEnd?.Invoke();
                    
                    break;
            }

            return RESULT.OK;
        }

        public void Dispose()
        {
            _instance = null;
            Stop();
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public StringWrapper LastMarker = new();
    }
}
