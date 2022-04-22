using TransformTest.General;
using UnityEngine;
using Zenject;

namespace TransformTest.Installers
{
    public class GeneralInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindFactories();
        }

        void BindFactories()
        {
            Container.BindFactory<AxisView, InputHandler,AxisLogic, AxisLogic.Factory>();
        }
    }
}
