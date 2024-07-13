using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.CommandPattern.Commands;

public class FileCreateInvoker
{
    private readonly List<ITableActionCommand> _tableActionCommands = new();
    private ITableActionCommand _tableActionCommand;

    public void SetCommand(ITableActionCommand tableActionCommand)
    {
        _tableActionCommand = tableActionCommand;
    }

    public void AddCommand(ITableActionCommand tableActionCommand)
    {
        _tableActionCommands.Add(tableActionCommand);
    }

    public IActionResult CreateFile()
    {
        return _tableActionCommand.Execute();
    }

    public List<IActionResult> CreateFiles()
    {
        return _tableActionCommands.Select(s => s.Execute()).ToList();
    }
}
