using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cloak;
using Cloak.Time;
using Grasp.Json;
using Grasp.Knowledge.Base;
using Grasp.Messaging;
using Grasp.Persistence;
using Grasp.Semantics;
using Grasp.Semantics.Discovery.Reflection;
using Grasp.Work;
using LibGit2Sharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Grasp.Git.Examples
{



	

	





	public class XXX
	{
		[Fact]
		public void YYY()
		{
			var activator = new NotionActivator(
				typeof(GitRepository).Assembly.BindDomainModel(new FullName("Grasp.Git")).BindDomainModel(),
				() => new IsolatedNotionContext());

			var jsonConverter = new GraspJsonConverter(new ExcludedFieldSet(), new JsonStateFactory(new FieldValueConverter()), activator);

			var jsonSerializer = new JsonSerializer { Formatting = Formatting.Indented };




			AmbientMessageChannel.Configure(() => new ConsoleMessageChannel { JsonConverter = jsonConverter });



			var gitRepository = new GitRepository(
				new Workspace(
					@"D:\My Documents\Projects\GitHub\Acme.Development\",
					new EventFileFormat(new EventJsonFormat(
						jsonConverter,
						jsonSerializer,
						stream => new JsonTextWriter(new StreamWriter(stream)) { Formatting = Formatting.Indented })),
					new Repository(@"D:\My Documents\Projects\GitHub\Acme.Development\.git")),
				activator);





			IAggregate article = new Article(new FullName("WorkItem1"), new FullName("Acme.Inventory"), "Acme Inventory Worksheet");

			gitRepository.SaveAggregateAsync(article).Wait();

			var loadTask = gitRepository.LoadAggregateAsync(typeof(Article), new FullName("Acme.Inventory"));
			loadTask.Wait();
			article = loadTask.Result;



			article.HandleCommand(new AddSectionCommand(new FullName("WorkItem2"), new FullName("Acme.Inventory"), new FullName("Computers")));

			gitRepository.SaveAggregateAsync(article).Wait();

			loadTask = gitRepository.LoadAggregateAsync(typeof(Article), new FullName("Acme.Inventory"));
			loadTask.Wait();
			article = loadTask.Result;



			article.HandleCommand(new AddSectionCommand(new FullName("WorkItem3"), new FullName("Acme.Inventory"), new FullName("Desks")));

			gitRepository.SaveAggregateAsync(article).Wait();

			loadTask = gitRepository.LoadAggregateAsync(typeof(Article), new FullName("Acme.Inventory"));
			loadTask.Wait();
			article = loadTask.Result;



			article.HandleCommand(new AddTagCommand(new FullName("WorkItem4"), new FullName("Acme.Inventory"), new FullName("Resources")));

			gitRepository.SaveAggregateAsync(article).Wait();



			


		}


		






		private sealed class ConsoleMessageChannel : IMessageChannel
		{
			internal GraspJsonConverter JsonConverter;

			public Task PublishAsync(Message message)
			{
				var jsonStream = new MemoryStream();
				var jsonWriter = new StreamWriter(jsonStream);

				var jsonSerializer = new JsonSerializer { Formatting = Formatting.Indented };

				jsonSerializer.Converters.Add(JsonConverter);




				

				JsonConverter.WriteJson(new JsonTextWriter(jsonWriter), message, jsonSerializer);

				jsonWriter.Flush();

				jsonStream.Seek(0, SeekOrigin.Begin);

				



				var json = new StreamReader(jsonStream).ReadToEnd();

				var text = JToken.Parse(json);


				Console.WriteLine(text);




				return Task.Delay(0);
			}
		}





		private sealed class IsolatedNotionContext : INotionContext
		{
			private readonly IDictionary<Field, object> _values = new Dictionary<Field, object>();

			public IEnumerable<Grasp.FieldBinding> GetBindings(Notion notion)
			{
				return _values.Select(pair => pair.Key.Bind(pair.Value));
			}

			public object GetValue(Notion notion, Field field)
			{
				object value;

				_values.TryGetValue(field, out value);

				return value;
			}

			public bool TryGetValue(Notion notion, Field field, out object value)
			{
				return _values.TryGetValue(field, out value);
			}

			public void SetValue(Notion notion, Field field, object value)
			{
				_values[field] = value;
			}
		}
	}






}