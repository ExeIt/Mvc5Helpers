namespace Torchlight.Mvc5.Common.Libs.Interfaces
{
    public interface IFormHandler<T>
    {
        bool Handle(T form);
    }
}
