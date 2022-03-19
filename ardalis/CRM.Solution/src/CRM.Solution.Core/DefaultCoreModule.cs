using Autofac;
using CRM.Solution.Core.Interfaces;
using CRM.Solution.Core.Services;

namespace CRM.Solution.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}
