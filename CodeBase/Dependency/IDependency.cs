using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.Dependency
{
    public interface IDependency<T>
    {
        static Func<T> _resolver;
        static Action<T> _disposer;

        public static void Register(Func<T> resolver)
        {
            _resolver = resolver;
        }

        public static void Register(Func<T> resolver, Action<T> disposer)
        {
            _resolver = resolver;
            _disposer = disposer;
        }

        public static void Register(T entity)
        {
            Register(delegate { return entity; });
        }

        public static void Register<U>() where U : T, new()
        {
            Register(delegate { return new U(); });
        }

        public static void RegisterPool<U>() where U : T, new()
        {
            var pool = new Queue<T>();

            Func<T> resolver = delegate ()
            {
                if (pool.Count > 0)
                    return pool.Dequeue();
                return new U();
            };

            Action<T> disposer = delegate (T entity)
            {
                pool.Enqueue(entity);
            };

            Register(resolver, disposer);
        }

        public static void Reset()
        {
            _resolver = null;
            _disposer = null;
        }

        public static T Resolve()
        {
            return _resolver();
        }
        
        public static T TryResolve()
        {
            return (_resolver != null) ? _resolver() : default(T);
        }

        public static bool TryResolve(out T result)
        {
            if (_resolver == null)
            {
                result = default(T);
                return false;
            }
            
            result = _resolver();
            return true;
        }

        public static void Dispose(T entity)
        {
            _disposer(entity);
        }
    }
}
