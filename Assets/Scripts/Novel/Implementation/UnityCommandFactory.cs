using Implementation.Commands;
using RenDisco;
using RenDisco.Commands;
using RenDisco.Implementation.Commands;
using System.Threading;


namespace Implementation
{
    public class UnityCommandFactory : CommandFactory
    {
        public UnityCommandFactory(DialogueComponent dialogueComponent, IStorage storage) : base(storage)
        {
            DialogueComponent = dialogueComponent;
        }

        public DialogueComponent DialogueComponent { get; }

        protected override DefineCommand CreateDefineCommand(Define define)
        {
            return new UnityDefineCommand(define, Storage, SynchronizationContext);
        }

        protected override DialogueCommand CreateDialogueCommand(Dialogue dialogue)
        {
            return new UnityDialogueCommand(dialogue, DialogueComponent, SynchronizationContext);
        }

        protected override HideCommand CreateHideCommand(Hide hide)
        {
            return new UnityHideCommand(hide, SynchronizationContext);
        }

        protected override NarrationCommand CreateNarrationCommand(Narration narration)
        {
            return new UnityNarrationCommand(narration, SynchronizationContext);
        }

        protected override PauseCommand CreatePauseCommand(Pause pause)
        {
            return new UnityPauseCommand(pause, SynchronizationContext);
        }

        protected override PlayMusicCommand CreatePlayMusicCommand(PlayMusic playMusic)
        {
            return new UnityPlayMusicCommand(playMusic, SynchronizationContext);
        }

        protected override ShowImageCommand CreateShowImageCommand(Show show)
        {
            return new UnityShowImageCommand(show, SynchronizationContext);
        }

        protected override ShowSceneCommand CreateShowSceneCommand(Scene scene)
        {
            return new UnityShowSceneCommand(scene, SynchronizationContext);
        }

        protected override StopMusicCommand CreateStopMusicCommand(StopMusic stopMusic)
        {
            return new UnityStopMusicCommand(stopMusic, SynchronizationContext);
        }
    }
}
