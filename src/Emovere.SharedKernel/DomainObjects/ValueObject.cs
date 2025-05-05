namespace Emovere.SharedKernel.DomainObjects
{
    public abstract record ValueObject
    {
        protected abstract void Validate();
    }
}