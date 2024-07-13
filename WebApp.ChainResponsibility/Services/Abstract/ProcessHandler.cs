using WebbApp.ChainResponsibility.Services.Interface;

namespace WebbApp.ChainResponsibility.Services.Abstract;

public abstract class ProcessHandler : IProcessHandler
{
    private IProcessHandler _nextProcessHandler;

    public IProcessHandler SetNext(IProcessHandler processHandler)
    {
        _nextProcessHandler = processHandler;
        return _nextProcessHandler;
    }

    public virtual object Handle(object o)
    {
        return _nextProcessHandler?.Handle(o);
    }
}
