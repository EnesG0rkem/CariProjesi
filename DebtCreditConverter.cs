using Avalonia.Data.Converters;
using CariProjesi.Data;
using CariProjesi.Models;
using CariProjesi.Services;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace CariProje;

public class DebtCreditConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        if (value is Movement movement && parameter is string col)
        {
            if (col == "Debt")
                return movement.MovementType ? "-" : movement.MovementChange.ToString("N2");
            else if (col == "Credit")
                return movement.MovementType ? movement.MovementChange.ToString("N2") : "-";
        }
        return "-";
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        => throw new NotImplementedException();
}
