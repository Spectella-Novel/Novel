using Core.Auth.Implementation;
using Core.Http.Implementation;
using Core.Shared;
using Core.Storage.Implementation;
using Novel.Feature.Screens.Load;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class Bootstrap
{
    private ICommandQueueInvokerAsync _commandQueueInvoker;
    public ReactiveProperty<int> CommandExecuted { get; private set; }
    public AsyncReactiveCommand InitCommandAsync { get; private set; }
    public Bootstrap(ICommandQueueInvokerAsync commandQueueInvoker)
    {
        _commandQueueInvoker = commandQueueInvoker;
        var httpService = new MockHttpService();
        CommandExecuted = new ReactiveProperty<int>(0);

        _commandQueueInvoker.Add(new AuthCommand(
            new DefaultStorage(), 
            new LoginRepository(httpService), 
            new RegisterRepository(httpService),
            new SessionManager(),
            new RefreshRepository()));
        InitCommandAsync = new AsyncReactiveCommand();
        InitCommandAsync.Subscribe(_ =>
        {
            Init();//heavy operation
            return null;
        });
    }

    public async void Init()
    {
        while (_commandQueueInvoker.Length > 0)
        {
            try
            {
                var result = await _commandQueueInvoker.Invoke();

                if (!result.IsSuccess)
                {
                    Debug.Log($"Bootstrap:Init() {result.Error}");
                }
                CommandExecuted.Value = _commandQueueInvoker.ExecutedLength;
            }
            catch (System.Exception ex)
            {
                Debug.LogError(ex);
            }
        }
        Debug.Log("Init complete");
    }
}
