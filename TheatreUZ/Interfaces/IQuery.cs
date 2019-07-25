using System;

namespace TheatreUZ
{
    public interface IQuery<out TResponse>
    {

    }

    public interface IQueryHandler<in TQuery, out TResponse> where TQuery : IQuery<TResponse>
    {
        TResponse Get();
    }
    
}
