using UniRx;
using System;
using Core.Shared;

public class BootstrapViewModel : IDisposable
{
    private readonly Bootstrap _bootstrap;
    private readonly CompositeDisposable _disposables = new CompositeDisposable();

    // Exposed reactive properties for UI binding
    public IReadOnlyReactiveProperty<int> CommandExecuted { get; private set; }
    public IReadOnlyReactiveProperty<float> Progress { get; private set; }
    public IReadOnlyReactiveProperty<bool> IsInitialized { get; private set; }
    public IReadOnlyReactiveProperty<string> LastError { get; private set; }

    // Exposed command to start initialization
    public IAsyncReactiveCommand<Unit> StartInitCommand { get; private set; }

    private readonly ReactiveProperty<int> _commandExecutedSubject;
    private readonly ReactiveProperty<float> _progressSubject;
    private readonly ReactiveProperty<bool> _isInitializedSubject;
    private readonly ReactiveProperty<string> _lastErrorSubject;

    public BootstrapViewModel(Bootstrap bootstrap)
    {
        _bootstrap = bootstrap ?? throw new ArgumentNullException(nameof(bootstrap));

        // Wrap Bootstrap's ReactiveProperty
        _commandExecutedSubject = _bootstrap.CommandExecuted;
        _progressSubject = new ReactiveProperty<float>(0);
        _isInitializedSubject = new ReactiveProperty<bool>(false);
        _lastErrorSubject = new ReactiveProperty<string>(null);

        CommandExecuted = _commandExecutedSubject.ToReadOnlyReactiveProperty();
        Progress = _progressSubject.ToReadOnlyReactiveProperty();
        IsInitialized = _isInitializedSubject.ToReadOnlyReactiveProperty();
        LastError = _lastErrorSubject.ToReadOnlyReactiveProperty();

        StartInitCommand = _bootstrap.InitCommandAsync;

        // Subscribe to command execution to update progress
        _commandExecutedSubject
            .Select(count => CalculateProgress(count))
            .Subscribe(p => _progressSubject.Value = p)
            .AddTo(_disposables);

        // Detect when init is complete
        _commandExecutedSubject
            .Where(count => count >= GetTotalCommandCount())
            .Take(1)
            .Subscribe(_ => _isInitializedSubject.Value = true)
            .AddTo(_disposables);

    }

    private float CalculateProgress(int executed)
    {
        int total = GetTotalCommandCount();
        return total > 0 ? (float)executed / total : 1f;
    }

    private int GetTotalCommandCount()
    {
        // Assuming ICommandQueueInvokerAsync has a way to get total count
        return _bootstrap.GetType()
            .GetField("_commandQueueInvoker", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(_bootstrap) is ICommandQueueInvokerAsync invoker
            ? invoker.Length
            : 0;
    }

    public void Dispose()
    {
        _disposables?.Dispose();
        _progressSubject?.Dispose();
        _isInitializedSubject?.Dispose();
        _lastErrorSubject?.Dispose();
    }
}