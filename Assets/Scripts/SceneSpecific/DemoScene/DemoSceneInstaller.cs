using System.Collections.Generic;
using AI.PathCalculation;
using AI.Task;
using GameControl;
using UnityEngine;
using Zenject;
using System.Linq;
using Common;

public class DemoSceneInstaller : MonoInstaller<DemoSceneInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<Rigidbody>().FromComponentSibling();
        Container.Bind<TeamInfo>().FromComponentSibling();
        Container.Bind<TaskPrioritizer>().FromNewComponentSibling();
        Container.Bind<IRadar>().FromComponentSibling();
        Container.Bind<ShipMovementProperties>().FromComponentSibling();
        Container.Bind<IAITask[]>().FromMethod(injectContext=> ((MonoBehaviour)injectContext.ObjectInstance).GetComponents<IAITask>());
        Container.Bind<RVOAgent>().FromComponentSibling();
        Container.Bind<PIDController>().FromMethod(context => new PIDController());
    }
}