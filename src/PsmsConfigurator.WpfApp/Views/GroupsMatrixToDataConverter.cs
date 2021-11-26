using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Data;

namespace PsmsConfigurator.WpfApp.Views {
  public class GroupsMatrixToDataConverter : IMultiValueConverter {
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
      var dataTable = new DataTable();
      var columns = values[0] as IList<string>;
      var rows = values[1] as IList<string>;
      var loopStates = values[2] as IList<bool[]>;
      var nameColumn = new DataColumn("Название группы") {
        ReadOnly = true
      };
      dataTable.Columns.Add(nameColumn);
      if (columns != null)
        foreach (var column in columns) {
          dataTable.Columns.Add(column, typeof(bool));
        }

      var index = 0;
      if (rows == null)
        return dataTable.DefaultView;
      foreach (var row in rows) {
        if (loopStates != null) {
          var currentRowValues = loopStates[index];
          var tmp = new object[currentRowValues.Length + 1];
          tmp[0] = row;
          currentRowValues.CopyTo(tmp, 1);
          dataTable.Rows.Add(tmp);
        }

        index++;
      }

      return dataTable.DefaultView;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) =>
      throw new NotImplementedException();
  }
}