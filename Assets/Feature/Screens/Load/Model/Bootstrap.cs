using Core.Auth.Implementation;
using Core.NetworkRepositories.Implementation;
using Core.Shared;
using Core.Storage.Implementation;
using Novel.Feature.Screens.Load;
using Unity.VisualScripting;

public class Bootstrap
{
    private ICommandQueueInvoker _commandQueueInvoker;
    public Bootstrap(ICommandQueueInvoker commandQueueInvoker)
    {
        _commandQueueInvoker = commandQueueInvoker;
        _commandQueueInvoker.Add(new AuthCommand(
            new SecurityStorage(), 
            new LoginRepository(), 
            new RegisterRepository(),
            new SessionManager(),
            new RefreshRepository()));
    }

    public async void Init()
    {
        var result =  await _commandQueueInvoker.Invoke();
    }
}
