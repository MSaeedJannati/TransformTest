using TransformTest;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ModelsInstaller", menuName = "TransformTest/Installers/ModelsInstaller")]
public class ModelsInstaller : ScriptableObjectInstaller<ModelsInstaller>
{
    [SerializeField] AxisModel _axisModel;
    public override void InstallBindings()
    {

        InstallScriptableObjects();
    }

    void InstallScriptableObjects()
    {
        Container.Bind<AxisModel>().FromScriptableObject(_axisModel).AsSingle();
    }
}