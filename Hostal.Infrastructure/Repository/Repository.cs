using Ardalis.Specification.EntityFrameworkCore;
using Hostal.Domain.Interfaces;
using Hostal.Infrastructure.Persistence;

namespace Hostal.Infrastructure.Repository;

public class Repository<TEntity>(HostalDbContext dbContext)
    : RepositoryBase<TEntity>(dbContext), IRepository<TEntity> where TEntity : class;
