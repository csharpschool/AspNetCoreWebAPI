﻿using System.Collections.Generic;

namespace AspNetCoreWebAPI.Services
{
    public interface IGenericEFRepository
    {
        IEnumerable<TEntity> Get<TEntity>() where TEntity : class;
    }
}