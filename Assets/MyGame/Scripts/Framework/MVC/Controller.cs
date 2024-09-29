using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Controller
{
    // Get Model and View
    protected Model GetModel<T>() where T : Model => MVC.GetModel<T>();
    protected View GetView<T>() where T : View => MVC.GetView<T>();

    // Register
    protected void RegisterModel(Model model) => MVC.RegisterModel(model);
    protected void RegisterView(View view) => MVC.RegisterView(view);
    protected void RegisterController(string eventName, Type controllerType) => MVC.RegisterController(eventName, controllerType);

    // Execute
    public abstract void Execute(object obj);
}