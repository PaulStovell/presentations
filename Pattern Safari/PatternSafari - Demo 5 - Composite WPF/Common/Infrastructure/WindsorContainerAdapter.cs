using System;
using Castle.Windsor;
using Microsoft.Practices.Composite;

namespace CompositeWPFContrib.Composite.WindsorExtensions
{
    /// <summary>
    /// Defines an adapter for the <see cref="IContainerFacade"/> interface
    /// to be used by the Composite Application Library.
    /// </summary>
    public class WindsorContainerAdapter : IContainerFacade
    {
        private readonly IWindsorContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindsorContainerAdapter"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public WindsorContainerAdapter(IWindsorContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Resolve an instance of the requested type from the container.
        /// </summary>
        /// <param name="type">The type of object to get from the container.</param>
        /// <returns>An instance of <paramref name="type"/>.</returns>
        public object Resolve(Type type)
        {
            return WindsorContainerExtensions.Resolve(_container, type);
        }

        /// <summary>
        /// Tries to resolve an instance of the requested type from the container.
        /// </summary>
        /// <param name="type">The type of object to get from the container.</param>
        /// <returns>
        /// An instance of <paramref name="type"/>. 
        /// If the type cannot be resolved it will return a <see langword="null"/> value.
        /// </returns>
        public object TryResolve(Type type)
        {
            return _container.TryResolve(type);
        }
    }
}
