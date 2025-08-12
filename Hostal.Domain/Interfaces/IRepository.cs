using Ardalis.Specification;

namespace Hostal.Domain.Interfaces;

/// <summary>
/// Generic interface defining a repository for a specific entity.
/// Inherits from IRepositoryBase from Ardalis.Specification for basic CRUD operations.
/// </summary>
/// <typeparam name="TEntity">Type of the entity managed by the repository.</typeparam>
public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : class;
