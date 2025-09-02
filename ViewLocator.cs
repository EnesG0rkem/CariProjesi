using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using CariProje.ViewModels;
using CariProje.Views.Dialogs;

namespace CariProje;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is null)
            return null;

        var name = param.GetType().Name;
        var viewTypeName = name.Replace("ViewModel", "View");

        // Explicitly handle known view mappings
        Type? type = viewTypeName switch
        {
            "MessageDialogView" => typeof(MessageDialogView),
            _ => GetViewType(viewTypeName)
        };

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = param;
            return control;
        }

        return new TextBlock { Text = $"Not Found: {name}" };
    }

    private Type? GetViewType(string viewTypeName)
    {
        var assembly = typeof(ViewLocator).Assembly;
        return assembly.GetTypes()
            .FirstOrDefault(t => t.Name == viewTypeName);
    }

    public bool Match(object? data) => data is ViewModelBase or DialogViewModel;
    
}
