namespace Configurateur
{
    public interface IProjection
    {
        void Apply(IEventWrapper eventWrapper);
    }
}