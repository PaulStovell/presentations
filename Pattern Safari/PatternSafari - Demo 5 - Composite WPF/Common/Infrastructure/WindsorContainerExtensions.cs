using System;
using Castle.Windsor;
using Castle.Core;

namespace CompositeWPFContrib.Composite.WindsorExtensions
{
    /// <summary>
    /// Extensions methods to extend and facilitate the usage of <see cref="IWindsorContainer"/>.
    /// </summary>
    public static class WindsorContainerExtensions
    {
        /// <summary>
        /// Returns whether a specified type has a type mapping registered in the container.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> to check for the type mapping.</param>
        /// <param name="type">The type to check if there is a type mapping for.</param>
        /// <returns><see langword="true"/> if there is a type mapping registered for <paramref name="type"/>.</returns>
        /// <remarks>In order to use this extension method, you first need to add the
        /// </remarks>
        public static bool IsTypeRegistered(this IWindsorContainer container, Type type)
        {
            return container.Kernel.HasComponent(type);
        }

        public static bool IsTypeRegistered<TType>(this IWindsorContainer container)
        {
            Type typeToCheck = typeof(TType);
            return IsTypeRegistered(container, typeToCheck);
        }

        /// <summary>
        /// Utility method to try to resolve a service from the container avoiding an exception if the container cannot build the type.
        /// </summary>
        /// <param name="container">The cointainer that will be used to resolve the type.</param>
        /// <typeparam name="T">The type to resolve.</typeparam>
        /// <returns>The instance of <typeparamref name="T"/> built up by the container.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static T TryResolve<T>(this IWindsorContainer container)
        {
            object result = TryResolve(container, typeof(T));
            if (result != null)
            {
                return (T)result;
            }
            return default(T);
        }

        /// <summary>
        /// Utility method to try to resolve a service from the container avoiding an exception if the container cannot build the type.
        /// </summary>
        /// <param name="container">The cointainer that will be used to resolve the type.</param>
        /// <param name="typeToResolve">The type to resolve.</param>
        /// <returns>The instance of <paramref name="typeToResolve"/> built up by the container.</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes")]
        public static object TryResolve(this IWindsorContainer container, Type typeToResolve)
        {
            object resolved;

            try
            {
                resolved = Resolve(container, typeToResolve);
            }
            catch
            {
                resolved = null;
            }

            return resolved;
        }

        /// <summary>
        /// Resolves a service from the container. If the type does not exist on the container, 
        /// first registers it with transient lifestyle.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object Resolve(this IWindsorContainer container, Type type)
        {
            if (type.IsClass && !container.Kernel.HasComponent(type))
                container.Kernel.AddComponent(type.FullName, type, LifestyleType.Transient);

            return container.Resolve(type);
        }

        /// <summary>
        /// Registers the type on the container.
        /// </summary>
        /// <typeparam name="TTypeFrom">The type of the interface.</typeparam>
        /// <typeparam name="TTypeTo">The type of the service.</typeparam>
        /// <param name="container">The container.</param>
        public static void RegisterType<TTypeFrom, TTypeTo>(this IWindsorContainer container)
        {
            RegisterType<TTypeFrom, TTypeTo>(container, true);
        }

        /// <summary>
        /// Registers the type on the container.
        /// </summary>
        /// <typeparam name="TTypeFrom">The type of interface.</typeparam>
        /// <typeparam name="TTypeTo">The type of the service.</typeparam>
        /// <param name="container">The container.</param>
        /// <param name="singleton">if set to <c>true</c> type will be registered as singleton.</param>
        public static void RegisterType<TTypeFrom, TTypeTo>(this IWindsorContainer container, bool singleton)
        {
            if (!container.Kernel.HasComponent(typeof(TTypeFrom)))
            {
                container.Kernel.AddComponent(typeof(TTypeTo).FullName, typeof(TTypeFrom), typeof(TTypeTo), singleton ? LifestyleType.Singleton : LifestyleType.Transient);
            }
        }
    }
}
