using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class MVC
{
    // Store
    public static Dictionary<string, Model> Models = new Dictionary<string, Model>();
    public static Dictionary<string, View> Views = new Dictionary<string, View>();
    public static Dictionary<string, Type> CommondMap = new Dictionary<string, Type>(); // Event name - Controller type

    // Register
    public static void RegisterModel(Model model) => Models[model.Name] = model;
    public static void RegisterView(View view)
    {
        // Avoid register duplicate, remove [view]
        if (Views.ContainsKey(view.Name))
            Views.Remove(view.Name);

        // Register event for [view]
        view.RegisterEvents();

        // Add [view] to [Views]
        Views[view.Name] = view;
    }
    public static void RegisterController(string eventName, Type controllerType) => CommondMap[eventName] = controllerType;

    // Get
    public static Model GetModel<T>() where T : Model
    {
        foreach (var model in Models.Values)
        {
            if (model is T)
                return model;
        }
        return null;
    }

    public static View GetView<T>() where T : View
    {
        foreach (var view in Views.Values)
        {
            if (view is T)
                return view;
        }
        return null;
    }

    // Send event
    public static void SendEvent(string eventName, object data = null)
    {
        // Controller response Event
        if (CommondMap.ContainsKey(eventName))
        {
            Type t = CommondMap[eventName];
            Controller c = Activator.CreateInstance(t) as Controller;
            // Invoke Controller's [HandleEvent] method
            c.Execute(data);
        }

        // View response Event
        foreach (var view in Views.Values)
        {
            if (view.AttentionEvents.Contains(eventName))
            {
                view.HandleEvent(eventName, data);
            }
        }

    }
}