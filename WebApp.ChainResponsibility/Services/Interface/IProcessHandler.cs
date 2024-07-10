namespace WebbApp.ChainResponsibility.Services.Interface;

public interface IProcessHandler
{
    IProcessHandler SetNext(IProcessHandler processHandler);
    object Handle(object o);
}