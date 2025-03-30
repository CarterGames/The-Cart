using System.Collections.Generic;
using System.Linq;
using CarterGames.Cart.Core.Management;
using CarterGames.Cart.Core.Random;

namespace CarterGames.Cart.Core.Editor
{
	public class SearchProviderRandom : SearchProvider<IRngProvider>
	{
		private static SearchProviderRandom Instance;

		protected override string ProviderTitle => "Select Random Provider";
		public override bool HasOptions => AssemblyHelper.GetClassesOfType<IRngProvider>(false)?.Count() > 0;


		public override List<SearchGroup<IRngProvider>> GetEntriesToDisplay()
		{
			var list = new List<SearchGroup<IRngProvider>>();
			var entries = new List<SearchItem<IRngProvider>>();
			var seededEntries = new List<SearchItem<IRngProvider>>();
			var options = AssemblyHelper.GetClassesOfType<IRngProvider>(false).Where(t => !ToExclude.Contains(t));
			var seededRandomOptions = AssemblyHelper.GetClassesOfType<ISeededRngProvider>(false).Where(t => !ToExclude.Contains(t)).ToList();
			
			foreach (var entry in options)
			{
				if (seededRandomOptions.Any(t => t.GetType() == entry.GetType()))
				{
					seededEntries.Add(SearchItem<IRngProvider>.Set(entry.GetType().Name.Replace("RngProvider", string.Empty), entry));
				}
				else
				{
					entries.Add(SearchItem<IRngProvider>.Set(entry.GetType().Name.Replace("RngProvider", string.Empty), entry));
				}
			}
			
			list.Add(new SearchGroup<IRngProvider>("Seeded Random Providers", seededEntries));
			list.Add(new SearchGroup<IRngProvider>(string.Empty, entries));
			
			return list;
		}
		
		
		public static SearchProviderRandom GetProvider()
		{
			if (Instance == null)
			{
				Instance = CreateInstance<SearchProviderRandom>();
			}

			return Instance;
		}
	}
}