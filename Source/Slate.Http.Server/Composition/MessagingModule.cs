using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Core;
using Cloak.Autofac;
using Cloak.Linq;
using Grasp.Hypermedia.Server;
using Grasp.Messaging;
using Grasp.Work.Items;
using Slate.Forms;
using Slate.Services;

namespace Slate.Http.Server.Composition
{
	public class MessagingModule : BuilderModule
	{
		public MessagingModule()
		{
			RegisterType<StartWorkHandler>().InstancePerDependency();

			Register(c => new TypeDispatchChannelFactory(c.Resolve<ILifetimeScope>()).Create())
			.As<IMessageChannel>()
			.SingleInstance();

			RegisterModule<AmbientMessageChannelModule>();
		}

		private sealed class TypeDispatchChannelFactory
		{
			private readonly ILifetimeScope _rootScope;

			internal TypeDispatchChannelFactory(ILifetimeScope rootScope)
			{
				_rootScope = rootScope;
			}

			internal TypeDispatchChannel Create()
			{
				return new TypeDispatchChannel(CreateChannels().ToReadOnlyDictionary());
			}

			private IEnumerable<KeyValuePair<Type, IMessageChannel>> CreateChannels()
			{
				yield return SetHandler<StartWorkCommand, StartWorkHandler>();
				yield return SetHandler<StartFormCommand, StartFormHandler>();
			}

			private KeyValuePair<Type, IMessageChannel> Subscribe<TEvent>() where TEvent : Event
			{
				return new KeyValuePair<Type, IMessageChannel>(typeof(TEvent), new SubscriberChannel<TEvent>(_rootScope));
			}

			private KeyValuePair<Type, IMessageChannel> SetHandler<TCommand, THandler>() where TCommand : Command where THandler : IHandler<TCommand>
			{
				return new KeyValuePair<Type, IMessageChannel>(typeof(TCommand), new HandlerChannel<TCommand, THandler>(_rootScope));
			}
		}

		private sealed class SubscriberChannel<TEvent> : IMessageChannel where TEvent : Event
		{
			private readonly ILifetimeScope _rootScope;

			internal SubscriberChannel(ILifetimeScope rootScope)
			{
				_rootScope = rootScope;
			}

			public async Task PublishAsync(Message message)
			{
				var @event = message as TEvent;

				if(@event != null)
				{
					using(var publishScope = _rootScope.BeginLifetimeScope())
					{
						var subscribers = publishScope.Resolve<IEnumerable<ISubscriber<TEvent>>>();

						await Task.WhenAll(subscribers.Select(subscriber => subscriber.ObserveAsync(@event)));
					}
				}
			}
		}

		private sealed class HandlerChannel<TCommand, THandler> : IMessageChannel where TCommand : Command where THandler : IHandler<TCommand>
		{
			private readonly ILifetimeScope _rootScope;

			internal HandlerChannel(ILifetimeScope rootScope)
			{
				_rootScope = rootScope;
			}

			public async Task PublishAsync(Message message)
			{
				var command = message as TCommand;

				if(command != null)
				{
					using(var handleScope = _rootScope.BeginLifetimeScope())
					{
						var handler = handleScope.Resolve<THandler>();

						await handler.HandleAsync(command);
					}
				}
			}
		}

		private sealed class AmbientMessageChannelModule : Module
		{
			private static readonly string _itemKey = typeof(AmbientMessageChannelModule).FullName + ".Channel";

			protected override void AttachToComponentRegistration(IComponentRegistry componentRegistry, IComponentRegistration registration)
			{
				var isController = registration.Services.OfType<TypedService>().Any(typedService => typeof(ApiController).IsAssignableFrom(typedService.ServiceType));

				if(isController)
				{
					registration.Activated += OnRegistrationActivated;
				}

				base.AttachToComponentRegistration(componentRegistry, registration);
			}

			private void OnRegistrationActivated(object sender, ActivatedEventArgs<object> e)
			{
				// This relies on IMessageChannel being SingleInstance. If it had any more specific lifetime, we would have to more
				// carefully manage the instances we use to configure the ambient message channel.

				var channel = e.Context.Resolve<IMessageChannel>();

				AmbientMessageChannel.Configure(() => channel);
			}
		}
	}
}