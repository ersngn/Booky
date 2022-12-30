namespace Booky.Domain.Models.Base;

    public interface IEntity
    {
    }

    public interface IEntity<out T> : IEntity where T : IEquatable<T>
    {
        public T Id { get; }
    }
