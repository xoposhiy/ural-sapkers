using System.Collections.Generic;

namespace Visualizer
{
	public delegate void ModelUpdater(VisualizerModel model);

	public class ModelUpdatersQueue
	{
		public void Add(ModelUpdater updater)
		{
			lock (updaters)
			{
				updaters.Enqueue(updater);
			}
		}

		public void ExecuteBatch(VisualizerModel model)
		{
			lock (updaters)
			{
				for (int i = 0; updaters.Count > 0 && i < BatchSize; i++)
				{
					updaters.Dequeue()(model);
				}
			}
		}

		private readonly Queue<ModelUpdater> updaters = new Queue<ModelUpdater>();
		private const int BatchSize = 150;
	}
}