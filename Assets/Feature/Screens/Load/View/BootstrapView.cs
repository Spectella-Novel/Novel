using Novel.Feature.Screens.Load;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class BootstrapView : MonoBehaviour {
    BootstrapViewModel BootstrapViewModel { get; set; }
    public void Init(BootstrapViewModel viewModel)
    {
        if (viewModel == null)
        {
            BootstrapViewModel = new BootstrapViewModel(new Bootstrap(new CommandQueueInvokerAsync()));
        }
        BootstrapViewModel.StartInitCommand.Execute(UniRx.Unit.Default);
        BootstrapViewModel.CommandExecuted.Subscribe(observer =>
        {
            Debug.Log($"BootstapView:Init() command {observer}");
        });
    }

    private void Start()
    {
        Init(null);
    }

}

