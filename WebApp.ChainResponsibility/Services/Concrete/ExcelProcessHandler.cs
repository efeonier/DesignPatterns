using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;
using WebbApp.ChainResponsibility.Services.Abstract;

namespace WebbApp.ChainResponsibility.Services.Concrete;

public class ExcelProcessHandler<T> : ProcessHandler
{
    private DataTable GetTable(object o)
    {
        var table = new DataTable();

        var type = typeof(T);

        type.GetProperties().ToList().ForEach(x => table.Columns.Add(x.Name, x.PropertyType));

        var list = o as List<T>;
        list?.ForEach(x =>
        {
            var values = type.GetProperties().Select(s => s.GetValue(x, null)).ToArray();
            table.Rows.Add(values);
        });
        return table;
    }

    public override object Handle(object o)
    {
        var wb = new XLWorkbook();
        var ds = new DataSet();
        ds.Tables.Add(GetTable(o));
        wb.Worksheets.Add(ds);
        var excelMs = new MemoryStream();
        wb.SaveAs(excelMs);
        return base.Handle(excelMs);
    }
}
